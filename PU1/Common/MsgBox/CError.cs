using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace PU1
{
    public class CError
    {
        #region CONST
        private const int ERROR_METHOD_FRAME = 2;
        #endregion


        #region PROPERTIES
        private static string StrErrorMethod
        {
            get
            {
                string StrResult = string.Empty;

                try
                {
                    StackTrace OStack = new StackTrace();

                    MethodBase OErrorMethod = OStack.GetFrame(CError.ERROR_METHOD_FRAME).GetMethod();
                    StrResult = OErrorMethod.DeclaringType.Name + "." + OErrorMethod.Name;
                }
                catch (Exception ex)
                {
                    CError.Ignore(ex);
                }

                return StrResult;
            }
        }
        #endregion


        #region FUNCTION
        public static void Throw(Exception OException)
        {
            CTrace.Error(CError.StrErrorMethod + " : " + OException.Message);
            throw OException;
        }


        public static void Show(Exception OException)
        {
            CTrace.Error(CError.StrErrorMethod + " : " + OException.Message);

            if (OException.GetType().Equals(typeof(CWarningException)) == true)
            {
                CMsgBox.Warning(OException.Message);
            }
            else CMsgBox.Error(CError.StrErrorMethod + " : " + OException.Message);
        }


        public static void Ignore(Exception OException)
        {
            CTrace.Error(CError.StrErrorMethod + " : " + OException.Message);
        }
        #endregion
    }
}
