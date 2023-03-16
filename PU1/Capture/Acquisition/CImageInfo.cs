using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CImageInfo : IDisposable
    {
        #region VARIABLE
        private DateTime m_OTime = DateTime.MinValue;
        private Bitmap m_OImage = null;
        #endregion


        #region PROPERTIES
        public DateTime OTime
        {
            get { return this.m_OTime; }
        }


        public Bitmap OImage
        {
            get { return this.m_OImage; }
            set { this.m_OImage = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CImageInfo(Bitmap OImage)
        {
            try
            {
                this.m_OTime = DateTime.Now;
                this.m_OImage = new Bitmap(OImage);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        //public CImageInfo(CImageInfo OImageInfo)
        //{
        //    try
        //    {
        //        this.m_OTime = OImageInfo.OTime;
        //        this.m_OImage = (Bitmap)OImageInfo.OImage.Clone();
        //    }
        //    catch (Exception ex)
        //    {
        //        CError.Throw(ex);
        //    }
        //}

        ~CImageInfo() 
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

        public void Dispose() 
        {
            if(this.m_OImage!= null) this.m_OImage.Dispose(); 
        }
        #endregion
    }
}
