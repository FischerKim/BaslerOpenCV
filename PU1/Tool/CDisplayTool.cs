using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CDisplayTool : IDisposable
    {
        #region VARIABLE
        private string m_StrDisplayName = string.Empty;
        private IImageExporter m_OView = null;
        private List<CImageInfo> m_LstOImageInfo = null;
        private List<long> m_LstI64Tick = null;
        private int m_I32NumImageCountPerSec = 0;
        //private CImageInfo m_OCurrentImage = null;
        private int m_I32ViewerResetCount = 0;
        private object m_OInterrupt = null;

        private Thread m_OWorker = null;
        private bool m_BDoWork = false;

        private ImageExportedHandler m_OImageExported = null;
        private CameraDisconnectedHandler m_OCameraDisconnected = null;
        private bool m_BClean = false;
       // int I32Count = 0;
        #endregion


        #region DELEGATE & EVENT
        public delegate void ImageExportedHandler(Bitmap OImage);
        public delegate void CameraDisconnectedHandler(string StrDisplayName);
        #endregion

        #region PROPERTIES

        public bool BClean
        {
            set { this.m_BClean = value; }        
        }
        public string StrDisplayName
        { 
            get { return m_StrDisplayName; }
            set { m_StrDisplayName = value; }
        }

        public IImageExporter OView
        {
            get { return m_OView; }
            set 
            {
                try
                {
                    if (this.m_OView != null)
                    {
                        this.m_OView.Exported -= new ExportedHandler(OView_Exported);
                    }
                    this.m_OView = value;
                    if (this.m_OView != null)
                    {
                        this.m_OView.Exported += new ExportedHandler(OView_Exported);
                    }
                }
                catch (Exception ex)
                {
                    CError.Throw(ex);
                }
            }
        }

        //public CImageInfo OCurrentImage
        //{
        //    get { return m_OCurrentImage; }
        //}

        public ImageExportedHandler OImageExported
        {
            set { this.m_OImageExported = value; }
        }

        public CameraDisconnectedHandler OCameraDisconnected
        {
            set { this.m_OCameraDisconnected = value; }
        }
        #endregion

        #region CONSTRUCTOR & DESTRUCTOR
        public CDisplayTool()
        {
            try
            {
                this.m_LstOImageInfo = new List<CImageInfo>();
                this.m_LstI64Tick = new List<long>();
                this.m_OInterrupt = new object();

                this.BeginWork();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        ~CDisplayTool()
        {
            try
            {
                this.Dispose();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion

        #region EVENT
        private void OView_Exported(object OSource)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                CImageInfo OImageInfo = new CImageInfo((Bitmap)OSource);
                this.m_LstOImageInfo.Add(OImageInfo); 
               // this.m_LstI64Tick.Add(OImageInfo.OTime.Ticks);
                //  this.m_OCurrentImage = OImageInfo;
               
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }
        }
        #region FUNCTION
        private void BeginWork()
        {
            try
            {
                if (this.m_OWorker == null)
                {
                    this.m_BDoWork = true;

                    this.m_OWorker = new Thread(this.Work);
                    this.m_OWorker.IsBackground = true;
                    this.m_OWorker.Start();
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void Work()
        {
            try
            {
                while (this.m_BDoWork == true)
                {
                    try
                    {
                        CImageInfo OImageInfo = this.GetImageInfoToDisplay();

                        if (OImageInfo != null && OImageInfo.OImage != null)
                        {
                            this.OnImageExported((Bitmap)OImageInfo.OImage.Clone());
                        }
                        //this.I32Count++;
                        //if (I32Count >= 4)
                        //{
                        //    I32Count = 0;
                        //    GC.Collect();
                        //}


                        //if (this.CheckCameraConnection() == false)
                        //{
                        //    if (this.m_I32ViewerResetCount < 3)
                        //    {
                        //        this.m_I32NumImageCountPerSec = 0;
                        //        this.m_I32ViewerResetCount++;

                        //        this.m_OView.Stop();
                        //        this.m_OView.Start();
                        //    }
                        //    else this.OnCameraDisconnected();
                        //}


                    }
                    catch (Exception ex)
                    {
                        CError.Ignore(ex);
                    }

                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                CError.Ignore(ex);
            }
        }


        private void EndWork()
        {
            try
            {
                if (this.m_OWorker != null)
                {
                    this.m_BDoWork = false;

                    this.m_OWorker.Join();
                    this.m_OWorker = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }

        private void Clean()
        {
            try
            {
                //반대로 돌지 않으면 리스트 0이 1로 대체될듯
                for (int i = this.m_LstOImageInfo.Count - 1; i >= 0; i--)
                {
                var diff = (DateTime.Now - this.m_LstOImageInfo[i].OTime).TotalSeconds;
                if (diff >= 1)
                {
                    this.m_LstOImageInfo[i].Dispose();
                    this.m_LstOImageInfo.RemoveAt(i);
                }
            }     //}
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }



        private CImageInfo GetImageInfoToDisplay()
        {
            CImageInfo OResult = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                if (this.m_LstOImageInfo.Count >= 2)
                {
                    for (int i = 0; i < this.m_LstOImageInfo.Count - 2; i++)
                    {
                        this.m_LstOImageInfo[i].Dispose();
                    }
                    this.m_LstOImageInfo.RemoveRange(0, this.m_LstOImageInfo.Count - 2);

                    //if more than 1 seconds elapsed, remove it and return null
                    if(this.m_BClean)  this.Clean();

                    if (this.m_LstOImageInfo.Count >= 1)
                    {
                        OResult = this.m_LstOImageInfo[this.m_LstOImageInfo.Count - 1]; //위에서 remove 했으니까 이게 최신 이미지.
                                                   //this.m_LstOImageInfo[0].Dispose();
                                                   //this.m_LstOImageInfo.RemoveAt(0);
                    }

                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return OResult;
        }


        public CImageInfo GetImageInfoToAnalysis()
        {
            CImageInfo OResult = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                if (this.m_LstOImageInfo.Count > 0)
                {
                    OResult = this.m_LstOImageInfo[this.m_LstOImageInfo.Count - 1];

                    //dispose는 GetImageInfoToDisplay에서 담당하도록 해야할듯
                      // this.m_LstOImageInfo[this.m_LstOImageInfo.Count - 1].Dispose();
                      // this.m_LstOImageInfo.RemoveAt(this.m_LstOImageInfo.Count - 1);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return OResult;
        }


        public void CheckFrameCount()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                int I32FrameCount = this.m_LstI64Tick.Count;
                this.m_LstI64Tick.Clear();

                if (this.m_OView != null)
                {
                    if (this.m_OView.BRun == true)
                    {
                        if (I32FrameCount == 0)
                        {
                            this.m_I32NumImageCountPerSec += 1;
                        }
                        else
                        {
                            this.m_I32NumImageCountPerSec = 0;
                            this.m_I32ViewerResetCount = 0;
                        }
                    }
                    else this.m_I32NumImageCountPerSec = 0;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        private bool CheckCameraConnection()
        {
            bool BResult = true;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                if (this.m_I32NumImageCountPerSec == 3)
                {
                    this.m_I32NumImageCountPerSec = 0;
                    BResult = false;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return BResult;
        }


        private void OnImageExported(Bitmap OImage)
        {
            try
            {
                if (this.m_OImageExported != null)
                {
                    this.m_OImageExported(OImage);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void OnCameraDisconnected()
        {
            try
            {
                if (this.m_OCameraDisconnected != null)
                {
                    this.m_OCameraDisconnected(this.m_StrDisplayName);
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
                this.EndWork();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
        #endregion



    }
}
