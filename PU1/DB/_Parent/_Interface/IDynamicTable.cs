using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public interface IDynamicTable : IDisposable
    {
        #region PROPERTIES
        string StrName { get; }
        TimeSpan ODayStartTime { get; set; }
        CAutoDeleteInfo OAutoDeleteInfo { get; set; }
        #endregion


        #region FUNCTION
        DataTable Select(DateTime OTime);
        DataTable Select(DateTime OTime, bool BNeedCriticalSection);
        int InsertRow();
        void Update(int I32RowIndex, string StrColumnName, object Data);
        void Commit();
        void AutoDelete();
        void BeginSyncData();
        void EndSyncData();
        void BeginSyncIO();
        void EndSyncIO();
        #endregion
    }
}
