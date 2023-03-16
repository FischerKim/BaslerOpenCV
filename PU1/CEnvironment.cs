using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PU1
{
    public class CEnvironment
    {
        #region SINGLE TON
        private static CEnvironment Src_It = null;


        public static CEnvironment It
        {
            get
            {
                CEnvironment OResult = null;

                try
                {
                    if (CEnvironment.Src_It == null)
                    {
                        CEnvironment.Src_It = new CEnvironment();
                    }

                    OResult = CEnvironment.Src_It;
                }
                catch (Exception ex)
                {
                    CError.Throw(ex);
                }

                return OResult;
            }
        }
        #endregion


        #region CONST
        public const double LEN_PER_PIXEL = 0.00375D;
        

        private const string ENABLED1 = "ENABLED1";
        private const string ENABLEMARK = "ENABLEMARK";

        private const string MAX_TRY = "MAXTRY";
        private const string CAM_HEAD1_GAIN = "CAM_HEAD1_GAIN";
        private const string CAM_HEAD1_EXPOTIME = "CAM_HEAD1_EXPOTIME";

        private const string CAMERA_ACQUISITION_MODE = "CAMERA_ACQUISITION_MODE";
        private const string CAMERA_FRAME_MODE = "CAMERA_FRAME_MODE";

        #endregion


        #region PROPERTIES

        public string StrEnabled1
        {
            get { return Convert.ToString(this.GetData(CEnvironment.ENABLED1)); }
            set { this.SetData(CEnvironment.ENABLED1, value); }
        }

        public int I32MaxTry
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.MAX_TRY)); }
            set { this.SetData(CEnvironment.MAX_TRY, value); }
        }
        //

        public int I32CamHead1Gain
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.CAM_HEAD1_GAIN)); }
            set { this.SetData(CEnvironment.CAM_HEAD1_GAIN, value); }
        }


        public int I32CamHead1ExpoTime
        {
            get { return Convert.ToInt32(this.GetData(CEnvironment.CAM_HEAD1_EXPOTIME)); }
            set { this.SetData(CEnvironment.CAM_HEAD1_EXPOTIME, value); }
        }


        public string StrCameraAcquisitionMode
        {
            get { return this.GetData(CEnvironment.CAMERA_ACQUISITION_MODE); }
            set { this.SetData(CEnvironment.CAMERA_ACQUISITION_MODE, value); }
        }


        public string StrCameraFrameMode
        {
            get { return this.GetData(CEnvironment.CAMERA_FRAME_MODE); }
            set { this.SetData(CEnvironment.CAMERA_FRAME_MODE, value); }
        }

        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        protected CEnvironment()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region FUNCTION
        private string GetData(string StrName)
        {
            string StrResult = string.Empty;

            try
            {
                int I32RowIndex = CDB.It[CDB.TABLE_ENVIRONMENT].SelectRowIndex(CDB.ENVIRONMENT_NAME, StrName);
                object OValue = CDB.It[CDB.TABLE_ENVIRONMENT].Select(I32RowIndex, CDB.ENVIRONMENT_VALUE);

                if (OValue != null) StrResult = (string)OValue;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return StrResult;
        }


        private void SetData(string StrName, object OValue)
        {
            try
            {
                int I32RowIndex = CDB.It[CDB.TABLE_ENVIRONMENT].SelectRowIndex(CDB.ENVIRONMENT_NAME, StrName);
                CDB.It[CDB.TABLE_ENVIRONMENT].Update(I32RowIndex, CDB.ENVIRONMENT_VALUE, OValue.ToString());
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void Commit()
        {
            try
            {
                CDB.It[CDB.TABLE_ENVIRONMENT].Commit();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
    }

}
