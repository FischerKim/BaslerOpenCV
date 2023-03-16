using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CTrace
    {
        private static string StrNow
        {
            get
            {
                string StrResult = string.Empty;

                try
                {
                    StrResult = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff");
                }
                catch (System.Exception ex)
                {
                    CError.Throw(ex);
                }

                return StrResult;
            }
        }


        public static void Info(string StrMsg)
        {
            try
            {
                Console.WriteLine("[" + CTrace.StrNow + " :: INFORMATION] " + StrMsg);
            }
            catch (System.Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public static void Warning(string StrMsg)
        {
            try
            {
                Console.WriteLine("[" + CTrace.StrNow + " :: WARNING] " + StrMsg);
            }
            catch (System.Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public static void Error(string StrMsg)
        {
            try
            {
                Console.WriteLine("[" + CTrace.StrNow + " :: ERROR] " + StrMsg);
            }
            catch (System.Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public static void Quest(string StrMsg, DialogResult OAnswer)
        {
            try
            {
                Console.WriteLine("[" + CTrace.StrNow + " :: QUESTION] " + StrMsg + " : " + OAnswer.ToString());
            }
            catch (System.Exception ex)
            {
                CError.Throw(ex);
            }
        }
    }
}
