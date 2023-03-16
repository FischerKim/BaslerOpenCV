using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CImageSaveFile : IDisposable
    {
        #region VARIABLE
        private DateTime m_OTime = DateTime.MinValue;
        private TimeSpan m_ODayStartTime = TimeSpan.Zero;

        private Bitmap m_OImage = null;
        private ImageFormat m_OFormat = ImageFormat.Bmp;

        private string m_StrDirectory = string.Empty;
        private string m_StrFile = string.Empty;
        #endregion


        #region PROPERTIES
        public DateTime OTime
        {
            get { return this.m_OTime; }
        }


        public TimeSpan ODayStartTime
        {
            get { return this.m_ODayStartTime; }
            set { this.m_ODayStartTime = value; }
        }


        public Bitmap OImage
        {
            get { return this.m_OImage; }
        }


        public ImageFormat OFormat
        {
            get { return this.m_OFormat; }
            set { this.m_OFormat = value; }
        }


        public string StrDirectory
        {
            get { return this.m_StrDirectory; }
            set { this.m_StrDirectory = value; }
        }


        public string StrFile
        {
            get { return this.m_StrFile; }
            set { this.m_StrFile = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CImageSaveFile(Bitmap OImage)
        {
            try
            {
                this.m_OTime = DateTime.Now;
                this.m_OImage = OImage;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public CImageSaveFile(DateTime OTime, Bitmap OImage)
        {
            try
            {
                this.m_OTime = OTime;
                this.m_OImage = OImage;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        ~CImageSaveFile()
        {
            try
            {
                this.Dispose();
            }
            catch (Exception ex)
            {
                CError.Ignore(ex);
            }
        }
        #endregion


        #region FUNCTION
        public void Save()
        {
            try
            {
                string StrDirectory = string.Empty;
                if (this.m_OTime.TimeOfDay < this.m_ODayStartTime)
                {
                    StrDirectory = String.Format(this.m_StrDirectory, this.OTime.AddDays(-1));
                }
                else StrDirectory = String.Format(this.m_StrDirectory, this.OTime);


                if (Directory.Exists(StrDirectory) == false)
                {
                    Directory.CreateDirectory(StrDirectory);
                }

                this.m_OImage.Save(String.Format(StrDirectory + "\\" + this.m_StrFile, this.m_OTime), this.m_OFormat);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public string GetFile()
        {
            string StrResult = string.Empty;

            try
            {
                if (this.m_OTime.TimeOfDay < this.m_ODayStartTime)
                {
                    StrResult = String.Format(this.m_StrDirectory, this.OTime.AddDays(-1));
                }
                else StrResult = String.Format(this.m_StrDirectory, this.OTime);

                StrResult += String.Format("\\" + this.m_StrFile, this.m_OTime);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return StrResult;
        }


        public void Dispose()
        {
            try
            {
                if (this.m_OImage != null)
                {
                    this.m_OImage.Dispose();
                    this.m_OImage = null;
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
