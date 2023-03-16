using System;
using System.Collections.Generic;
using PylonC.NET;
using System.Threading;

namespace PylonC.NETSupportLibrary
{
    /* The ImageProvider is responsible for opening and closing a device, it takes care of the grabbing and buffer handling, 
     it notifies the user via events about state changes, and provides access to GenICam parameter nodes of the device. 
     The grabbing is done in an internal thread. After an image is grabbed the image ready event is fired by the grab 
     thread. The image can be acquired using GetCurrentImage(). After processing of the image it can be released via ReleaseImage.
     The image is then queued for the next grab.  */
    public class ImageProvider
    {
        /* Simple data class for holding image data. */
        public class Image
        {
            public Image(int newWidth, int newHeight, Byte[] newBuffer, bool color)
            {
                Width = newWidth;
                Height = newHeight;
                Buffer = newBuffer;
                Color = color;
            }

            public readonly int Width; /* The width of the image. */
            public readonly int Height; /* The height of the image. */
            public readonly Byte[] Buffer; /* The raw image data. */
            public readonly bool Color; /* If false the buffer contains a Mono8 image. Otherwise, RGBA8packed is provided. */
        }

        /* The class GrabResult is used internally to queue grab results. */
        protected class GrabResult
        {
            public Image ImageData; /* Holds the taken image. */
            public PYLON_STREAMBUFFER_HANDLE Handle; /* Holds the handle of the image registered at the stream grabber. It is used to queue the buffer associated with itself for the next grab. */
        }

        /* The members of ImageProvider: */
        protected bool m_converterOutputFormatIsColor = false;/* The output format of the format converter. */
        protected PYLON_IMAGE_FORMAT_CONVERTER_HANDLE m_hConverter; /* The format converter is used mainly for coverting color images. It is not used for Mono8 or RGBA8packed images. */
        protected Dictionary<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>> m_convertedBuffers; /* Holds handles and buffers used for converted images. It is not used for Mono8 or RGBA8packed images.*/
        public PYLON_DEVICE_HANDLE m_hDevice;           /* Handle for the pylon device. */
        protected PYLON_STREAMGRABBER_HANDLE m_hGrabber;   /* Handle for the pylon stream grabber. */
        protected PYLON_DEVICECALLBACK_HANDLE m_hRemovalCallback;    /* Required for deregistering the callback. */
        protected PYLON_WAITOBJECT_HANDLE m_hWait;         /* Handle used for waiting for a grab to be finished. */
        protected uint m_numberOfBuffersUsed = 5;          /* Number of m_buffers used in grab. */
        protected bool m_grabThreadRun = false;            /* Indicates that the grab thread is active.*/
        protected bool m_open = false;                     /* Indicates that the device is open and ready to grab.*/
        protected bool m_grabOnce = false;                 /* Use for single frame mode. */
        protected bool m_removed = false;                  /* Indicates that the device has been removed from the PC. */
        protected Thread m_grabThread;                     /* Thread for grabbing the images. */
        protected Object m_lockObject;                     /* Lock object used for thread synchronization. */
        protected Dictionary<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>> m_buffers; /* Holds handles and buffers used for grabbing. */
        protected List<GrabResult> m_grabbedBuffers; /* List of grab results already grabbed. */
        protected DeviceCallbackHandler m_callbackHandler; /* Handles callbacks from a device .*/
        protected string m_lastError = "";                 /* Holds the error information belonging to the last exception thrown. */

        /* Creates the last error text from message and detailed text. */
        private string GetLastErrorText()
        {
            string lastErrorMessage = GenApi.GetLastErrorMessage();
            string lastErrorDetail  = GenApi.GetLastErrorDetail();

            string lastErrorText = lastErrorMessage;
            if (lastErrorDetail.Length > 0)
            {
                lastErrorText += "\n\nDetails:\n";
            }
            lastErrorText += lastErrorDetail;
            return lastErrorText;
        }

        /* Sets the internal last error variable. */
        private void UpdateLastError()
        {
            m_lastError = GetLastErrorText();
        }

        public ImageProvider()
        {
            m_grabThread = new Thread(Grab);
            m_lockObject = new Object();
            m_buffers = new Dictionary<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>>();
            m_grabbedBuffers = new List<GrabResult>();
            m_hGrabber = new PYLON_STREAMGRABBER_HANDLE();
            m_hDevice = new PYLON_DEVICE_HANDLE();
            m_hRemovalCallback = new PYLON_DEVICECALLBACK_HANDLE();
            m_hConverter = new PYLON_IMAGE_FORMAT_CONVERTER_HANDLE();
            m_callbackHandler = new DeviceCallbackHandler();
            m_callbackHandler.CallbackEvent += new DeviceCallbackHandler.DeviceCallback(RemovalCallbackHandler);
        }

        public bool IsOpen
        {
            get { return m_open; }
        }

        #region COMMENT OPEN(UINT)
        public void Open(uint index)
        {
            Open(Pylon.CreateDeviceByIndex(index));
        }
        #endregion

        #region CLOSE(VOID)
        public void Close()
        {
            OnDeviceClosingEvent();

            Exception lastException = null;

            m_removed = false;

            if (m_hGrabber.IsValid)
            {
                try
                {
                   
                    Pylon.StreamGrabberClose(m_hGrabber);
                }
                catch (Exception e) { lastException = e; UpdateLastError(); }
            }

            if (m_hDevice.IsValid)
            {
                try 
                {
                    if (m_hRemovalCallback.IsValid)
                    {
                        Pylon.DeviceDeregisterRemovalCallback(m_hDevice, m_hRemovalCallback);
                    }
                }
                catch (Exception e) { lastException = e; UpdateLastError(); }

                try
                {
                    if (Pylon.DeviceIsOpen(m_hDevice))
                    {
                        Pylon.DeviceClose(m_hDevice);
                    }
                }
                catch (Exception e) { lastException = e; UpdateLastError(); }
                
                try
                {
                    Pylon.DestroyDevice(m_hDevice);
                }
                catch (Exception e) { lastException = e; UpdateLastError(); }
            }

            m_hGrabber.SetInvalid();
            m_hRemovalCallback.SetInvalid();
            m_hDevice.SetInvalid();

            OnDeviceClosedEvent();

            if (lastException != null)
            {
                throw lastException;
            }
        }
        #endregion

        public void OneShot()
        {
            if (m_open && !m_grabThread.IsAlive)
            {
                m_numberOfBuffersUsed = 1;
                m_grabOnce = true;
                m_grabThreadRun = true;

                m_grabThread = new Thread(Grab);
                m_grabThread.Start();
            }
        }
        
        public void ContinuousShot()
        {
            if (m_open && !m_grabThread.IsAlive)
            {
                m_numberOfBuffersUsed = 5;
                m_grabOnce = false;
                m_grabThreadRun = true;
                m_grabThread = new Thread(Grab);
                m_grabThread.Start();
            }
        }

        public void Stop()
        {
            if (m_open && m_grabThread.IsAlive)
            {
                m_grabThreadRun = false;
                m_grabThread.Join();
            }
        }

        public Image GetCurrentImage()
        {
            lock (m_lockObject)
            {
                if (m_grabbedBuffers.Count > 0)
                {
                    return m_grabbedBuffers[0].ImageData;
                }
            }
            return null;
        }

        public Image GetLatestImage()
        {
            lock (m_lockObject)
            {
                while (m_grabbedBuffers.Count > 1)
                {
                    ReleaseImage();
                }
                if (m_grabbedBuffers.Count > 0)
                {
                    return m_grabbedBuffers[0].ImageData;
                }
            }
            return null;
        }

        public bool ReleaseImage()
        {
            lock (m_lockObject)
            {
                if (m_grabbedBuffers.Count > 0 )
                {
                    if (m_grabThreadRun)
                    {
                        Pylon.StreamGrabberQueueBuffer(m_hGrabber, m_grabbedBuffers[0].Handle, 0);
                    }
                    m_grabbedBuffers.RemoveAt(0);
                    return true;
                }
            }
            return false;
        }

        public string GetLastErrorMessage()
        {
            if (m_lastError.Length == 0)
            {
                UpdateLastError();
            }
            string text = m_lastError;
            m_lastError = "";
            return text;
        }

        public NODE_HANDLE GetNodeFromDevice(string name)
        {
            if (m_open && !m_removed)
            {
                NODEMAP_HANDLE hNodemap = Pylon.DeviceGetNodeMap(m_hDevice);
                return GenApi.NodeMapGetNode(hNodemap, name);
            }
            return new NODE_HANDLE();
        }

        #region OPEN(PYLON_DEVICE_HANDLE)
        protected void Open(PYLON_DEVICE_HANDLE device)
        {
            try
            {
                m_hDevice = device;

                Pylon.DeviceOpen(m_hDevice, Pylon.cPylonAccessModeControl | Pylon.cPylonAccessModeStream);

                m_hRemovalCallback = Pylon.DeviceRegisterRemovalCallback(m_hDevice, m_callbackHandler);

                if (Pylon.DeviceFeatureIsWritable(m_hDevice, "GevSCPSPacketSize"))
                {
                    Pylon.DeviceSetIntegerFeature(m_hDevice, "GevSCPSPacketSize", 9000);
                }

                if (Pylon.DeviceFeatureIsWritable(m_hDevice, "ChunkModeActive"))
                {
                    Pylon.DeviceSetBooleanFeature(m_hDevice, "ChunkModeActive", false);
                }

                if (Pylon.DeviceFeatureIsAvailable(m_hDevice, "EnumEntry_TriggerSelector_AcquisitionStart"))
                {
                    Pylon.DeviceFeatureFromString(m_hDevice, "TriggerSelector", "AcquisitionStart");
                    Pylon.DeviceFeatureFromString(m_hDevice, "TriggerMode", "Off");
                }

                if (Pylon.DeviceFeatureIsAvailable(m_hDevice, "EnumEntry_TriggerSelector_FrameBurstStart"))
                {
                    Pylon.DeviceFeatureFromString(m_hDevice, "TriggerSelector", "FrameBurstStart");
                    Pylon.DeviceFeatureFromString(m_hDevice, "TriggerMode", "Off");
                }

                if (Pylon.DeviceFeatureIsAvailable(m_hDevice, "EnumEntry_TriggerSelector_FrameStart"))
                {
                    Pylon.DeviceFeatureFromString(m_hDevice, "TriggerSelector", "FrameStart");
                    Pylon.DeviceFeatureFromString(m_hDevice, "TriggerMode", "Off");
                }

                if (Pylon.DeviceGetNumStreamGrabberChannels(m_hDevice) < 1)
                {
                    throw new Exception("The transport layer doesn't support image streams.");
                }

                m_hGrabber = Pylon.DeviceGetStreamGrabber(m_hDevice, 0);
                Pylon.StreamGrabberOpen(m_hGrabber);

                m_hWait = Pylon.StreamGrabberGetWaitObject(m_hGrabber);
            }
            catch
            {
                UpdateLastError();

                try
                {
                    Close();
                }
                catch
                {
                }
                throw;
            }

            OnDeviceOpenedEvent();
        }
        #endregion

        protected void SetupGrab()
        {
            lock (m_lockObject)
            {
                m_grabbedBuffers.Clear();
            }

            if (m_grabOnce)
            {
                Pylon.DeviceFeatureFromString(m_hDevice, "AcquisitionMode", "SingleFrame");
            }
            else
            {
                Pylon.DeviceFeatureFromString(m_hDevice, "AcquisitionMode", "Continuous");
            }
            foreach (KeyValuePair<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>> pair in m_buffers)
            {
                pair.Value.Dispose();
            }
            m_buffers.Clear();

            uint payloadSize = checked((uint)Pylon.DeviceGetIntegerFeature(m_hDevice, "PayloadSize"));

            Pylon.StreamGrabberSetMaxNumBuffer(m_hGrabber, m_numberOfBuffersUsed);
            Pylon.StreamGrabberSetMaxBufferSize(m_hGrabber, payloadSize);
            Pylon.StreamGrabberPrepareGrab(m_hGrabber);
            for (uint i = 0; i < m_numberOfBuffersUsed; ++i)
            {
                PylonBuffer<Byte> buffer = new PylonBuffer<byte>(payloadSize, true);
                PYLON_STREAMBUFFER_HANDLE handle = Pylon.StreamGrabberRegisterBuffer(m_hGrabber, ref buffer);
                m_buffers.Add(handle, buffer);
            }
            foreach (KeyValuePair<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>> pair in m_buffers)
            {
                Pylon.StreamGrabberQueueBuffer(m_hGrabber, pair.Key, 0);
            }

            m_hConverter.SetInvalid();
            Pylon.DeviceExecuteCommandFeature(m_hDevice, "AcquisitionStart");
        }

        protected void Grab()
        {
            OnGrabbingStartedEvent();
            try
            {
                SetupGrab();

                while (m_grabThreadRun)
                {
                    if (!Pylon.WaitObjectWait(m_hWait, 1000))
                    {
                        lock (m_lockObject)
                        {
                            if (m_grabbedBuffers.Count != m_numberOfBuffersUsed)
                            {
                                //throw new Exception("A grab timeout occurred.");
                            }
                            continue;
                        }
                    }

                    PylonGrabResult_t grabResult;
                    if (!Pylon.StreamGrabberRetrieveResult(m_hGrabber, out grabResult)) continue;

                    if (grabResult.Status == EPylonGrabStatus.Grabbed)
                    {
                        EnqueueTakenImage(grabResult);

                        OnImageReadyEvent();

                        if (m_grabOnce)
                        {
                            m_grabThreadRun = false;
                            break;
                        }
                    }
                    else if (grabResult.Status == EPylonGrabStatus.Failed) continue;
                    //{
                    //    throw new Exception(string.Format("A grab failure occurred. See the method ImageProvider::Grab for more information. The error code is {0:X08}.", grabResult.ErrorCode));
                    //}
                }

                CleanUpGrab();
            }
            catch (Exception e)
            {
                m_grabThreadRun = false;

                string lastErrorMessage = GetLastErrorText();

                try
                {
                    CleanUpGrab();
                }
                catch
                {
                    /* Another exception cannot be handled. */
                }

                OnGrabbingStoppedEvent();

                if (!m_removed)
                {
                    OnGrabErrorEvent(e, lastErrorMessage);
                }
                return;
            }

            OnGrabbingStoppedEvent();
        }

        protected void EnqueueTakenImage(PylonGrabResult_t grabResult)
        {
            PylonBuffer<Byte> buffer;

            if (!m_buffers.TryGetValue(grabResult.hBuffer, out buffer))
            {
                throw new Exception("Failed to find the buffer associated with the handle returned in grab result.");
            }

            GrabResult newGrabResultInternal = new GrabResult();
            newGrabResultInternal.Handle = grabResult.hBuffer; /* Add the handle to requeue the buffer in the stream grabber queue. */

            if (grabResult.PixelType == EPylonPixelType.PixelType_Mono8 || grabResult.PixelType == EPylonPixelType.PixelType_RGBA8packed)
            {
                newGrabResultInternal.ImageData = new Image(grabResult.SizeX, grabResult.SizeY, buffer.Array, grabResult.PixelType == EPylonPixelType.PixelType_RGBA8packed);
            }
            else
            {
                if (!m_hConverter.IsValid)
                {
                    m_convertedBuffers = new Dictionary<PYLON_STREAMBUFFER_HANDLE,PylonBuffer<byte>>();
                    m_hConverter = Pylon.ImageFormatConverterCreate();
                    m_converterOutputFormatIsColor = !Pylon.IsMono(grabResult.PixelType) || Pylon.IsBayer(grabResult.PixelType);
                }
                PylonBuffer<Byte> convertedBuffer = null;
                bool bufferListed = m_convertedBuffers.TryGetValue(grabResult.hBuffer, out convertedBuffer);
                Pylon.ImageFormatConverterSetOutputPixelFormat(m_hConverter, m_converterOutputFormatIsColor ? EPylonPixelType.PixelType_BGRA8packed : EPylonPixelType.PixelType_Mono8);
                Pylon.ImageFormatConverterConvert(m_hConverter, ref convertedBuffer, buffer, grabResult.PixelType, (uint)grabResult.SizeX, (uint)grabResult.SizeY, (uint)grabResult.PaddingX, EPylonImageOrientation.ImageOrientation_TopDown);
                if (!bufferListed)
                {
                    m_convertedBuffers.Add(grabResult.hBuffer, convertedBuffer);
                }

                newGrabResultInternal.ImageData = new Image(grabResult.SizeX, grabResult.SizeY, convertedBuffer.Array, m_converterOutputFormatIsColor);
            }
            lock (m_lockObject)
            {
                m_grabbedBuffers.Add(newGrabResultInternal);
            }
        }

        protected void CleanUpGrab()
        {
            Pylon.DeviceExecuteCommandFeature(m_hDevice, "AcquisitionStop");

            if (m_hConverter.IsValid)
            {

                Pylon.ImageFormatConverterDestroy(m_hConverter);

                m_hConverter.SetInvalid();

                foreach (KeyValuePair<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>> pair in m_convertedBuffers)
                {
                    pair.Value.Dispose();
                }
                m_convertedBuffers = null;
            }

            Pylon.StreamGrabberCancelGrab(m_hGrabber);


            {
                bool isReady;
                do
                {
                    PylonGrabResult_t grabResult;
                    isReady = Pylon.StreamGrabberRetrieveResult(m_hGrabber, out grabResult);

                } while (isReady);
            }


            foreach (KeyValuePair<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>> pair in m_buffers)
            {
                Pylon.StreamGrabberDeregisterBuffer(m_hGrabber, pair.Key);
            }

            foreach (KeyValuePair<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>> pair in m_buffers)
            {
                pair.Value.Dispose();
            }
            m_buffers.Clear();

            Pylon.StreamGrabberFinishGrab(m_hGrabber);
        }


        protected void RemovalCallbackHandler(PYLON_DEVICE_HANDLE hDevice)
        {
            OnDeviceRemovedEvent();
        }


        public delegate void DeviceOpenedEventHandler();
        public event DeviceOpenedEventHandler DeviceOpenedEvent;

        public delegate void DeviceClosingEventHandler();
        public event DeviceClosingEventHandler DeviceClosingEvent;

        public delegate void DeviceClosedEventHandler();
        public event DeviceClosedEventHandler DeviceClosedEvent;

        public delegate void GrabbingStartedEventHandler();
        public event GrabbingStartedEventHandler GrabbingStartedEvent;

        public delegate void ImageReadyEventHandler();
        public event ImageReadyEventHandler ImageReadyEvent;

        public delegate void GrabbingStoppedEventHandler();
        public event GrabbingStoppedEventHandler GrabbingStoppedEvent;

        public delegate void GrabErrorEventHandler(Exception grabException, string additionalErrorMessage);
        public event GrabErrorEventHandler GrabErrorEvent;

        public delegate void DeviceRemovedEventHandler();
        public event DeviceRemovedEventHandler DeviceRemovedEvent;


        protected void OnDeviceOpenedEvent()
        {
            m_open = true;
            if (DeviceOpenedEvent != null)
            {
                DeviceOpenedEvent();
            }
        }


        protected void OnDeviceClosingEvent()
        {
            m_open = false;
            if (DeviceClosingEvent != null)
            {
                DeviceClosingEvent();
            }
        }


        protected void OnDeviceClosedEvent()
        {
            m_open = false;
            if (DeviceClosedEvent != null)
            {
                DeviceClosedEvent();
            }
        }


        protected void OnGrabbingStartedEvent()
        {
            if (GrabbingStartedEvent != null)
            {
                GrabbingStartedEvent();
            }
        }


        protected void OnImageReadyEvent()
        {
            if (ImageReadyEvent != null)
            {
                ImageReadyEvent();
            }
        }


        protected void OnGrabbingStoppedEvent()
        {
            if (GrabbingStoppedEvent != null)
            {
                GrabbingStoppedEvent();
            }
        }


        protected void OnGrabErrorEvent(Exception grabException, string additionalErrorMessage)
        {
            if (GrabErrorEvent != null)
            {
                GrabErrorEvent(grabException, additionalErrorMessage);
            }
        }


        protected void OnDeviceRemovedEvent()
        {
            m_removed = true;
            m_grabThreadRun = false;
            if (DeviceRemovedEvent != null)
            {
                DeviceRemovedEvent();
            }
        }
    }
}
