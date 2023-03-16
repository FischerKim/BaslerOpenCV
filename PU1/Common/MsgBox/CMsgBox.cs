using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CMsgBox
    {
        public static void Info(string StrMsg)
        {
            try
            {
                MessageBox.Show(StrMsg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CTrace.Info(StrMsg);
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
                MessageBox.Show(StrMsg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CTrace.Warning(StrMsg);
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
                MessageBox.Show(StrMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CTrace.Error(StrMsg);
            }
            catch (System.Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public static DialogResult OKCancel(string StrMsg)
        {
            DialogResult OResult = DialogResult.Cancel;

            try
            {
                OResult = MessageBox.Show(StrMsg, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                CTrace.Quest(StrMsg, OResult);
            }
            catch (System.Exception ex)
            {
                CError.Throw(ex);
            }

            return OResult;
        }


        public static DialogResult YesNo(string StrMsg)
        {
            DialogResult OResult = DialogResult.No;

            try
            {
                OResult = MessageBox.Show(StrMsg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                CTrace.Quest(StrMsg, OResult);
            }
            catch (System.Exception ex)
            {
                CError.Throw(ex);
            }

            return OResult;
        }
    }
}
