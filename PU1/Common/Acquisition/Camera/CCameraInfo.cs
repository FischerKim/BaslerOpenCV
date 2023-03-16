using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CCameraInfo
    {
        #region VARIABLE
        private string m_StrVendor = string.Empty;
        private string m_StrModel = string.Empty;
        private string m_StrSerial = string.Empty;
        private string m_StrIP = string.Empty;
        private object m_OKey = null;
        #endregion


        #region PROPERTIES
        public string StrVender
        {
            get { return this.m_StrVendor; }
            set { this.m_StrVendor = value; }
        }


        public string StrModel
        {
            get { return this.m_StrModel; }
            set { this.m_StrModel = value; }
        }


        public string StrSerial
        {
            get { return this.m_StrSerial; }
            set { this.m_StrSerial = value; }
        }


        public string StrIP
        {
            get { return this.m_StrIP; }
            set { this.m_StrIP = value; }
        }


        public object OKey
        {
            get { return this.m_OKey; }
            set { this.m_OKey = value; }
        }
        #endregion
    }
}
