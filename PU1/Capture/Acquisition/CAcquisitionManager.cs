using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PU1
{
    public class CAcquisitionManager : IDisposable
    {
        #region SINGLETON
        private static CAcquisitionManager Src_It = null;
        #endregion

        public static CAcquisitionManager It
        {
            get
            {
                CAcquisitionManager OResult = null;
                try
                {
                    if (CAcquisitionManager.Src_It == null)
                    {
                        CAcquisitionManager.Src_It = new CAcquisitionManager();
                    }
                    OResult = CAcquisitionManager.Src_It;
                }
                catch (Exception ex)
                {
                    CError.Throw(ex);
                }
                return OResult;
            }
        }

        private CBasler m_OHead1 = null;

        public CBasler OHead1
        { 
            get { return m_OHead1; }
            set { m_OHead1 = value; }
        }

        ~CAcquisitionManager()
        {
            try
            {
                this.Dispose();
            }
            catch (Exception ex)
            {
                //CError.Throw(ex);
            }
        }



        #region FUNCTION
        public void Setup()
        {
            try
            {
                int I32Head1Gain = CEnvironment.It.I32CamHead1Gain;
                int I32Head1ExpoTime = CEnvironment.It.I32CamHead1ExpoTime;
                if ((I32Head1Gain >= this.m_OHead1.I32GainMin) && (I32Head1Gain <= this.m_OHead1.I32GainMax)) this.m_OHead1.I32Gain = I32Head1Gain;
                if ((I32Head1ExpoTime >= this.m_OHead1.I32ExposureTimeMin) && (I32Head1ExpoTime <= this.m_OHead1.I32ExposureTimeMax)) this.m_OHead1.I32ExposureTime = I32Head1ExpoTime;
                ////AcquisitionStart
                //this.m_OHead1.StrTriggerSelector = "AcquisitionStart";
                //this.m_OHead1.StrTriggerMode = CEnvironment.It.StrCameraAcquisitionMode;
                ////FrameStart
                //this.m_OHead1.StrTriggerSelector = "FrameStart";
                //this.m_OHead1.StrTriggerMode = CEnvironment.It.StrCameraFrameMode;

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
                if(this.m_OHead1 != null) 
                {
                    this.m_OHead1.Dispose();
                    this.m_OHead1 = null;
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
