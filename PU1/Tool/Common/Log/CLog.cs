using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CLog
    {
        #region CONST
        public const string LOG = "[{0:yyyy-MM-dd HH.mm.ss fff}]";
        #endregion


        #region VARIABLE
        private DateTime m_OTime = DateTime.MinValue;
        private string m_StrText = string.Empty;
        #endregion


        #region PROPERTIES
        public DateTime OTime
        {
            get { return this.m_OTime; }
            set { this.m_OTime = value; }
        }


        public string StrText
        {
            get { return this.m_StrText; }
            set { this.m_StrText = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CLog(string StrText)
        {
            try
            {
                this.m_OTime = DateTime.Now;
                this.m_StrText = StrText;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public CLog(DateTime OTime, string StrText)
        {
            try
            {
                this.m_OTime = OTime;
                this.m_StrText = StrText;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region FUNCTION
        public string GetLog()
        {
            string StrResult = string.Empty;

            try
            {
                StrResult = string.Format(LOG + this.m_StrText, this.m_OTime);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return StrResult;
        }
        #endregion
    }
}
