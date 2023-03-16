using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PU1
{
    public class CTool : IDisposable
    {
        private static CTool Src_It = null;

        public static CTool It
        { 
            get
            {
                CTool OResult = null;
                try
                {
                    if (CTool.Src_It == null)
                    {
                        CTool.Src_It = new CTool();
                    }
                    OResult = CTool.Src_It;
                }
                catch(Exception ex)
                {
                    CError.Throw(ex);
                }
                return OResult;
            }
        }

        #region VARIABLE
        CInspectionTool m_OTool = null;
        #endregion

        #region PROPERTIES
        public CInspectionTool OTool
        {
            get { return this.m_OTool; }
        }
        #endregion


        protected CTool() 
        {
            try
            {
                this.m_OTool = new CInspectionTool();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }

        ~CTool()
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

        public void Dispose()
        {
            try
            {
                if (this.m_OTool != null)
                {
                    this.m_OTool.Dispose();
                    this.m_OTool = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
    }
}
