using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public abstract class ADB : IDisposable
    {
        #region CONST & STATIC
        private static string DEFINED_TABLE_PATH = Environment.CurrentDirectory + @"\DB\TableDefinder.csv";

        private const string DEFINED_TABLE_NAME = "TABLE_DEFINDER";
        private const string DEFINED_TABLE_COLUMN_NAME = "NAME";
        private const string DEFINED_TABLE_COLUMN_TYPE = "TYPE";
        private const string DEFINED_TABLE_COLUMN_PATH = "PATH";
        private const string DEFINED_TABLE_COLUMN_ROOT = "ROOT";

        private const string TABLE_TYPE_STATIC = "STATIC";
        private const string TABLE_TYPE_YEARLY = "YEARLY";
        private const string TABLE_TYPE_MONTHLY = "MONTHLY";
        private const string TABLE_TYPE_DAILY = "DAILY";
        private const string TABLE_TYPE_SPECIAL = "SPECIAL";
        #endregion


        #region VARIABLE
        private List<CTable> m_LstOTable = null;

        private List<IDynamicTable> m_LstODynamicTable = null;
        private IDynamicTable m_OTable = null;
        private DateTime m_OBeforeDeleteTime = DateTime.MinValue;

        private Thread m_OWorker = null;
        private bool m_BDoWork = false;
        private object m_OInterrupt = null;
        #endregion


        #region INDEXER
        public CTable this[string StrName]
        {
            get
            {
                CTable OResult = null;

                try
                {
                    foreach (CTable _Item in this.m_LstOTable)
                    {
                        if (_Item.StrName != StrName) continue;

                        OResult = _Item;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    CError.Throw(ex);
                }

                return OResult;
            }
        }
        #endregion


        #region PROPERTIES
        protected List<CTable> LstOTable
        {
            get { return this.m_LstOTable; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public ADB()
        {
            try
            {
                this.m_LstOTable = new List<CTable>();
                this.m_LstODynamicTable = new List<IDynamicTable>();
                this.m_OInterrupt = new object();

                this.BeginWork();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region FUNCTION
        public virtual bool Load()
        {
            bool BResult = false;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                do
                {
                    try
                    {
                        CTable OTableDefinder = new CTable(ADB.DEFINED_TABLE_NAME, ADB.DEFINED_TABLE_PATH);

                        DataTable OTableInfo = OTableDefinder.Select();
                        string StrName = string.Empty;
                        string StrKind = string.Empty;
                        string StrPath = string.Empty;
                        string StrRoot = string.Empty;

                        foreach (DataRow _Item in OTableInfo.Rows)
                        {
                            StrName = (string)_Item[ADB.DEFINED_TABLE_COLUMN_NAME];
                            StrKind = (string)_Item[ADB.DEFINED_TABLE_COLUMN_TYPE];

                            switch (StrKind)
                            {
                                case TABLE_TYPE_STATIC:
                                    StrPath = (string)_Item[ADB.DEFINED_TABLE_COLUMN_PATH];
                                    this.m_LstOTable.Add(new CTable(StrName, StrPath));
                                    break;

                                case TABLE_TYPE_MONTHLY:
                                    StrPath = (string)_Item[ADB.DEFINED_TABLE_COLUMN_PATH];
                                    StrRoot = (string)_Item[ADB.DEFINED_TABLE_COLUMN_ROOT];
                                    this.m_LstODynamicTable.Add(new CMonthlyTable(StrName, StrPath, StrRoot));
                                    break;

                                case TABLE_TYPE_DAILY:
                                    StrPath = (string)_Item[ADB.DEFINED_TABLE_COLUMN_PATH];
                                    StrRoot = (string)_Item[ADB.DEFINED_TABLE_COLUMN_ROOT];
                                    this.m_LstODynamicTable.Add(new CDailyTable(StrName, StrPath, StrRoot));
                                    break;
                            }
                        }

                        OTableDefinder.Dispose();
                        OTableDefinder = null;

                        BResult = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (this.IsRetry(ex) == true)
                        {
                            this.Dispose();
                        }
                        else break;
                    }
                }
                while (true);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return BResult;
        }


        public void SetAutoDelete(string StrTable, CAutoDeleteInfo OAutoDeleteInfo)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                foreach (IDynamicTable _Item in this.m_LstODynamicTable)
                {
                    if (_Item.StrName != StrTable) continue;

                    _Item.OAutoDeleteInfo = OAutoDeleteInfo;
                    break;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        public void SetDayStartTime(string StrTable, TimeSpan ODayStartTime)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                foreach (IDynamicTable _Item in this.m_LstODynamicTable)
                {
                    if (_Item.StrName != StrTable) continue;

                    _Item.ODayStartTime = ODayStartTime;
                    break;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        private bool IsRetry(Exception OException)
        {
            bool BResult = false;

            try
            {
                if ((OException.GetType() == typeof(IOException)) && (((IOException)OException).GetType() != typeof(FileNotFoundException)))
                {
                    string StrMsg = "Please close the data file before continuing." + "\n"
                                  + "Do you want to continue loading data?" + "\n"
                                  + "(Please first close the data file)";

                    BResult = (CMsgBox.YesNo(StrMsg) == DialogResult.Yes);
                }
                else
                {
                    if (Directory.Exists(Environment.CurrentDirectory + "\\DBBackup") == true)
                    {
                        string StrMsg = "Failed to Load data" + "\n"
                                      + "Do you want to recover from old data?";

                        BResult = (CMsgBox.YesNo(StrMsg) == DialogResult.Yes);

                        if (BResult == true)
                        {
                            if (Directory.Exists(Environment.CurrentDirectory + "\\DB") == true)
                            {
                                Directory.Delete(Environment.CurrentDirectory + "\\DB", true);
                            }
                            Directory.Move(Environment.CurrentDirectory + "\\DBBackup", Environment.CurrentDirectory + "\\DB");
                        }
                    }
                    else
                    {
                        string StrMsg = "Failed to Load data";

                        BResult = (CMsgBox.YesNo(StrMsg) == DialogResult.Yes);

                        //if (BResult == true)
                        //{
                        //    if (Directory.Exists(Environment.CurrentDirectory + "\\DB") == true)
                        //    {
                        //        Directory.Delete(Environment.CurrentDirectory + "\\DB", true);
                        //    }
                        //    Directory.CreateDirectory(Environment.CurrentDirectory + "\\DB");

                        //    this.InitDB();
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return BResult;
        }


        private void BeginWork()
        {
            try
            {
                if (this.m_OWorker == null)
                {
                    this.m_BDoWork = true;

                    this.m_OWorker = new Thread(this.Work);
                    this.m_OWorker.IsBackground = true;
                    this.m_OWorker.Start();
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void Work()
        {
            try
            {
                while (this.m_BDoWork == true)
                {
                    try
                    {
                        int I32Index = 0;
                        while (this.GetDynamicTable(I32Index, out this.m_OTable))
                        {
                            this.m_OTable.Commit();
                            I32Index++;
                        }


                        if (this.m_OBeforeDeleteTime.Date != DateTime.Now.Date)
                        {
                            this.m_OBeforeDeleteTime = DateTime.Now;

                            I32Index = 0;
                            while (this.GetDynamicTable(I32Index, out this.m_OTable))
                            {
                                this.m_OTable.AutoDelete();
                                I32Index++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        CError.Ignore(ex);
                    }

                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                CError.Ignore(ex);
            }
        }


        private void EndWork()
        {
            try
            {
                if (this.m_OWorker != null)
                {
                    this.m_BDoWork = false;

                    this.m_OWorker.Join();
                    this.m_OWorker = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private bool GetDynamicTable(int I32Index, out IDynamicTable OTable)
        {
            bool BResult = false;
            OTable = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                if (this.m_LstODynamicTable.Count > I32Index)
                {
                    OTable = this.m_LstODynamicTable[I32Index];
                    BResult = true;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return BResult;
        }


        public IDynamicTable GetDynamicTable(string StrName)
        {
            IDynamicTable OResult = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                foreach (IDynamicTable _Item in this.m_LstODynamicTable)
                {
                    if (_Item.StrName != StrName) continue;

                    OResult = _Item;
                    break;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return OResult;
        }


        public virtual void Dispose()
        {
            try
            {
                this.EndWork();

                if (this.m_LstOTable != null)
                {
                    for (int _Index = this.m_LstOTable.Count - 1; _Index >= 0; _Index--)
                    {
                        this.m_LstOTable[_Index].Dispose();
                    }

                    this.m_LstOTable.Clear();
                    this.m_LstOTable = null;
                }

                if (this.m_LstODynamicTable != null)
                {
                    for (int _Index = this.m_LstODynamicTable.Count - 1; _Index >= 0; _Index--)
                    {
                        this.m_LstODynamicTable[_Index].Dispose();
                    }

                    this.m_LstODynamicTable.Clear();
                    this.m_LstODynamicTable = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        protected abstract void InitDB();
        #endregion
    }
}
