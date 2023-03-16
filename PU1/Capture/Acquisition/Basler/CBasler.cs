using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PylonC.NET;
using PylonC.NETSupportLibrary;

namespace PU1
{
    public class CBasler : ICamera, IDisposable
    {
        #region VARIABLE
        private CCameraInfo m_OCameraInfo = null;

        private ImageProvider m_OImageProvider = null;
        private NODE_HANDLE m_OWidthHandle = null;
        private NODE_HANDLE m_OHeightHandle = null;
        private NODE_HANDLE m_OCenterXHandle = null;
        private NODE_HANDLE m_OOffsetXHandle = null;
        private NODE_HANDLE m_OGainHandle = null;
        private NODE_HANDLE m_OExposureHandle = null;
        private NODE_HANDLE m_OTriggerSelectorHandle = null;
        private NODE_HANDLE m_OTriggerModeHandle = null;
        private NODE_HANDLE m_OTriggerSourceHandle = null;

        private Bitmap m_OImage = null;
        private bool m_BRun = false;
        #endregion


        #region DELEGATE & EVENT
        public event ExportedHandler Exported = null;
        #endregion


        #region PROPERTIES
        public CCameraInfo OCameraInfo
        {
            get { return this.m_OCameraInfo; }
        }


        public bool BRun
        {
            get { return this.m_BRun; }
        }


        public int I32Width
        {
            get { return (int)GenApi.IntegerGetValue(this.m_OWidthHandle); }
            set { GenApi.IntegerSetValue(this.m_OWidthHandle, value); }
        }


        public int I32WidthMin
        {
            get { return (int)GenApi.IntegerGetMin(this.m_OWidthHandle); }
        }


        public int I32WidthMax
        {
            get { return (int)GenApi.IntegerGetMax(this.m_OWidthHandle); }
        }


        public int I32Height
        {
            get { return (int)GenApi.IntegerGetValue(this.m_OHeightHandle); }
            set { GenApi.IntegerSetValue(this.m_OHeightHandle, value); }
        }


        public int I32HeightMin
        {
            get { return (int)GenApi.IntegerGetMin(this.m_OHeightHandle); }
        }


        public int I32HeightMax
        {
            get { return (int)GenApi.IntegerGetMax(this.m_OHeightHandle); }
        }


        public bool BCenterX
        {
            get { return GenApi.BooleanGetValue(this.m_OCenterXHandle); }
            set
            {
                GenApi.BooleanSetValue(this.m_OCenterXHandle, value);
                if (value == false) GenApi.IntegerSetValue(this.m_OOffsetXHandle, 0);
            }
        }


        public int I32Gain
        {
            get { return (int)GenApi.IntegerGetValue(this.m_OGainHandle); }
            set { GenApi.IntegerSetValue(this.m_OGainHandle, value); }
        }


        public int I32GainMin
        {
            get { return (int)GenApi.IntegerGetMin(this.m_OGainHandle); }
        }


        public int I32GainMax
        {
            get { return (int)GenApi.IntegerGetMax(this.m_OGainHandle); }
        }


        public int I32ExposureTime
        {
            get { return (int)GenApi.IntegerGetValue(this.m_OExposureHandle); }
            set { GenApi.IntegerSetValue(this.m_OExposureHandle, value); }
        }


        public int I32ExposureTimeMin
        {
            get { return (int)GenApi.IntegerGetMin(this.m_OExposureHandle); }
        }


        public int I32ExposureTimeMax
        {
            get { return (int)GenApi.IntegerGetMax(this.m_OExposureHandle); }
        }


        public string StrTriggerSelector
        {
            get { return GenApi.NodeToString(this.m_OTriggerSelectorHandle); }
            set { GenApi.NodeFromString(this.m_OTriggerSelectorHandle, value); }
        }


        public string StrTriggerMode
        {
            get { return GenApi.NodeToString(this.m_OTriggerModeHandle); }
            set { GenApi.NodeFromString(this.m_OTriggerModeHandle, value); }
        }


        public string StrTriggerSource
        {
            get { return GenApi.NodeToString(this.m_OTriggerSourceHandle); }
            set { GenApi.NodeFromString(this.m_OTriggerSourceHandle, value); }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CBasler()
        {
            try
            {
                PYLON_DEVICE_INFO_HANDLE OHandle = Pylon.GetDeviceInfoHandle(0);

                this.m_OCameraInfo = new CCameraInfo();
                this.m_OCameraInfo.StrVender = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "VendorName");
                this.m_OCameraInfo.StrModel = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "ModelName");
                this.m_OCameraInfo.StrSerial = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "SerialNumber");
                this.m_OCameraInfo.StrIP = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "IpAddress");
                this.m_OCameraInfo.OKey = (uint)0;

                this.m_OImageProvider = new ImageProvider();
                this.m_OImageProvider.Open(0);
                this.m_OImageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(this.OImageProvider_DeviceRemovedEvent);
                this.m_OImageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(this.OImageProvider_ImageReadyEvent);
                this.m_OWidthHandle = this.m_OImageProvider.GetNodeFromDevice("Width");
                this.m_OHeightHandle = this.m_OImageProvider.GetNodeFromDevice("Height");
                this.m_OCenterXHandle = this.m_OImageProvider.GetNodeFromDevice("CenterX");
                this.m_OOffsetXHandle = this.m_OImageProvider.GetNodeFromDevice("OffsetX");
                this.m_OGainHandle = this.m_OImageProvider.GetNodeFromDevice("GainRaw");
                this.m_OExposureHandle = this.m_OImageProvider.GetNodeFromDevice("ExposureTimeRaw");
                this.m_OTriggerSelectorHandle = this.m_OImageProvider.GetNodeFromDevice("TriggerSelector");
                this.m_OTriggerModeHandle = this.m_OImageProvider.GetNodeFromDevice("TriggerMode");
                this.m_OTriggerSourceHandle = this.m_OImageProvider.GetNodeFromDevice("TriggerSource");
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public CBasler(string StrIP)
        {
            try
            {
                this.m_OCameraInfo = this.GetCameraInfo(StrIP);
                if (this.m_OCameraInfo == null) throw new Exception("카메라(" + StrIP + ")에 연결할 수 없습니다.");

                this.m_OImageProvider = new ImageProvider();
                this.m_OImageProvider.Open((uint)this.m_OCameraInfo.OKey);
                this.m_OImageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OImageProvider_DeviceRemovedEvent);
                this.m_OImageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OImageProvider_ImageReadyEvent);
                this.m_OWidthHandle = this.m_OImageProvider.GetNodeFromDevice("Width");
                this.m_OHeightHandle = this.m_OImageProvider.GetNodeFromDevice("Height");
                this.m_OCenterXHandle = this.m_OImageProvider.GetNodeFromDevice("CenterX");
                this.m_OOffsetXHandle = this.m_OImageProvider.GetNodeFromDevice("OffsetX");
                this.m_OGainHandle = this.m_OImageProvider.GetNodeFromDevice("GainRaw");
                this.m_OExposureHandle = this.m_OImageProvider.GetNodeFromDevice("ExposureTimeRaw");
                this.m_OTriggerSelectorHandle = this.m_OImageProvider.GetNodeFromDevice("TriggerSelector");
                this.m_OTriggerModeHandle = this.m_OImageProvider.GetNodeFromDevice("TriggerMode");
                this.m_OTriggerSourceHandle = this.m_OImageProvider.GetNodeFromDevice("TriggerSource");
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public CBasler(CCameraInfo OCameraInfo)
        {
            try
            {
                this.m_OCameraInfo = OCameraInfo;

                this.m_OImageProvider = new ImageProvider();
                this.m_OImageProvider.Open((uint)this.m_OCameraInfo.OKey);
                this.m_OImageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OImageProvider_DeviceRemovedEvent);
                this.m_OImageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OImageProvider_ImageReadyEvent);
                this.m_OWidthHandle = this.m_OImageProvider.GetNodeFromDevice("Width");
                this.m_OHeightHandle = this.m_OImageProvider.GetNodeFromDevice("Height");
                this.m_OCenterXHandle = this.m_OImageProvider.GetNodeFromDevice("CenterX");
                this.m_OOffsetXHandle = this.m_OImageProvider.GetNodeFromDevice("OffsetX");
                this.m_OGainHandle = this.m_OImageProvider.GetNodeFromDevice("GainRaw");
                this.m_OExposureHandle = this.m_OImageProvider.GetNodeFromDevice("ExposureTimeRaw");
                this.m_OTriggerSelectorHandle = this.m_OImageProvider.GetNodeFromDevice("TriggerSelector");
                this.m_OTriggerModeHandle = this.m_OImageProvider.GetNodeFromDevice("TriggerMode");
                this.m_OTriggerSourceHandle = this.m_OImageProvider.GetNodeFromDevice("TriggerSource");
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        ~CBasler()
        {
            try
            {
                this.Dispose();
            }
            catch (Exception ex)
            {
              //  CError.Ignore(ex);
            }
        }
        #endregion


        #region EVENT
        private CCameraInfo GetCameraInfo(string StrIP)
        {
            CCameraInfo OResult = null;

            try
            {
                uint U32Count = Pylon.EnumerateDevices();
                for (uint _Index = 0; _Index < U32Count; _Index++)
                {
                    PYLON_DEVICE_INFO_HANDLE OHandle = Pylon.GetDeviceInfoHandle(_Index);
                    if (StrIP == Pylon.DeviceInfoGetPropertyValueByName(OHandle, "IpAddress"))
                    {
                        OResult = new CCameraInfo();
                        OResult.StrVender = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "VendorName");
                        OResult.StrModel = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "ModelName");
                        OResult.StrSerial = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "SerialNumber");
                        OResult.StrIP = StrIP;
                        OResult.OKey = _Index;
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return OResult;
        }


        private void OImageProvider_DeviceRemovedEvent()
        {
            try
            {
                this.m_OImageProvider.Stop();
                this.m_OImageProvider.Close();
            }
            catch (Exception ex)
            {
              //CError.Ignore(ex);
            }
        }


        private void OImageProvider_ImageReadyEvent()
        {
            try
            {
                ImageProvider.Image OImage = this.m_OImageProvider.GetLatestImage();

                if (OImage != null)
                {
                    if (this.m_OImage != null)
                    {
                        this.m_OImage.Dispose();
                        this.m_OImage = null;
                    }

                    BitmapFactory.CreateBitmap(out this.m_OImage, OImage.Width, OImage.Height, OImage.Color);
                    BitmapFactory.UpdateBitmap(this.m_OImage, OImage.Buffer, OImage.Width, OImage.Height, OImage.Color);
                    this.OnExported(this.m_OImage);

                    this.m_OImageProvider.ReleaseImage();
                }
            }
            catch (Exception ex)
            {
              //  CError.Ignore(ex);
            }
        }
        #endregion


        #region FUNCTION
        public void OneShot()
        {
            try
            {
                this.m_OImageProvider.OneShot();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void Start()
        {
            try
            {
                this.m_OImageProvider.ContinuousShot();
                this.m_BRun = true;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void Stop()
        {
            try
            {
                this.m_OImageProvider.Stop();
                this.m_BRun = false;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OnExported(object OImage)
        {
            try
            {
                if (this.Exported != null)
                {
                    this.Exported(OImage);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void Dispose()
        {
            try
            {
                if (this.m_OImageProvider != null)
                {
                    this.m_OImageProvider.Close();
                    this.m_OImageProvider = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
    }
}
