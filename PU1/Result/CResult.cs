using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CResult : IDisposable
    {
        #region VARIABLE
        private CImageInfo m_OImageInfo = null;

        private bool m_BInspected = false;
        private bool m_BOK = false;
        private double m_F64S = 0;
        private double m_F64X = 0;
        private double m_F64Y = 0;
        #endregion


        #region PROPERTIES


        public CImageInfo OImageInfo
        {
            get { return this.m_OImageInfo; }
        }


        public bool BInspected
        {
            get { return this.m_BInspected; }
            set { this.m_BInspected = value; }
        }


        public bool BOK
        {
            get { return this.m_BOK; }
            set { this.m_BOK = value; }
        }


        public double F64S
        {
            get { return this.m_F64S; }
            set { this.m_F64S = value; }
        }


        public double F64X
        {
            get { return this.m_F64X; }
            set { this.m_F64X = value; }
        }


        public double F64Y
        {
            get { return this.m_F64Y; }
            set { this.m_F64Y = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CResult(CImageInfo OImageInfo)
        {
            try
            {
                this.m_OImageInfo = OImageInfo;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }

        ~CResult()
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
            if(this.m_OImageInfo.OImage!=null) this.m_OImageInfo.OImage.Dispose();
        }   
        #endregion
    }
}
