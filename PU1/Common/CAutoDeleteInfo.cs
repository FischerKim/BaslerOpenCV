using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CAutoDeleteInfo
    {
        #region VARIABLE
        private bool m_BEnabled = false;
        private ETIME_UNIT m_BTimeUnit = ETIME_UNIT.DAY;
        private int m_I32KeepPeriod = 30;
        #endregion


        #region PROPERTIES
        public bool BEnabled
        {
            get { return this.m_BEnabled; }
            set { this.m_BEnabled = value; }
        }


        public ETIME_UNIT ETimeUnit
        {
            get { return this.m_BTimeUnit; }
            set { this.m_BTimeUnit = value; }
        }


        public int I32KeepPeriod
        {
            get { return this.m_I32KeepPeriod; }
            set { this.m_I32KeepPeriod = value; }
        }
        #endregion
    }
}
