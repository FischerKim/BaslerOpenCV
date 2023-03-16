using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PU1
{
    public class CDB : ADB
    {
        #region SIGNLE TON
        protected static CDB Src_It = null;


        public static CDB It
        {
            get
            {
                CDB OResult = null;

                try
                {
                    if (CDB.Src_It == null)
                    {
                        CDB.Src_It = new CDB();
                    }

                    OResult = CDB.Src_It;
                }
                catch (Exception ex)
                {
                    CError.Throw(ex);
                }

                return OResult;
            }
        }
        #endregion


        #region CONST
        public const string TABLE_ENVIRONMENT = "ENVIRONMENT";
        public const string ENVIRONMENT_NAME = "NAME";
        public const string ENVIRONMENT_VALUE = "VALUE";

        //public const string TABLE_RECIPE_LIST = "RECIPE_LIST";
        //public const string RECIPE_LIST_ID = "ID";
        //public const string RECIPE_LIST_NAME = "NAME";

        //public const string TABLE_RECIPE_INFO = "RECIPE_INFO";
        //public const string RECIPE_INFO_ID = "ID";
        //public const string RECIPE_INFO_RECIPE = "RECIPE";

        public const string TABLE_REPORT = "REPORT";
        public const string REPORT_DATETIME = "DATETIME";
        public const string REPORT_RECIPE = "RECIPE";
        public const string REPORT_HEAD1_RESULT = "HEAD1_RESULT";
        public const string REPORT_HEAD1_PIXEL_X = "HEAD1_PIXEL_X";
        public const string REPORT_HEAD1_PIXEL_Y = "HEAD1_PIXEL_Y";
        public const string REPORT_HEAD1_SCORE = "HEAD1_SCORE";
        public const string REPORT_HEAD1_FILE = "HEAD1_FILE";
        #endregion


        #region FUNCTION
        protected override void InitDB() { }
        #endregion
    }
}
