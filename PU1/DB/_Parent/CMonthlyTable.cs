using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CMonthlyTable : ATable, IDynamicTable
    {
        #region CONST
        private static string STANDARD_DIR = "{0:yyyy}";
        private static string STANDARD_PATH = "{0:yyyy}\\{0:yyyy-MM}.csv";
        #endregion


        #region VARIABLE
        private readonly string m_StrRoot = string.Empty;

        private DataTable m_ODataSource = null;
        private object m_ODataInterrupt = null;

        private CTable m_OList = null;
        private CTable m_OSchema = null;
        private object m_OIOInterrupt = null;
        #endregion


        #region PROPERTIES
        public TimeSpan ODayStartTime
        {
            get { return TimeSpan.MinValue; }
            set { }
        }


        public CAutoDeleteInfo OAutoDeleteInfo
        {
            get { return null; }
            set { }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CMonthlyTable(string StrName, string StrTableListPath, string StrRoot)
            : base(StrName)
        {
            try
            {
                this.m_StrRoot = StrRoot;

                this.m_OList = new CTable(StrName + "List", StrTableListPath);
                this.m_OSchema = new CTable(StrName + "Schema", StrRoot + "\\Schema.csv");
                this.m_OIOInterrupt = new object();

                this.m_ODataSource = this.m_OSchema.Select().Clone();
                this.m_ODataInterrupt = new object();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region FUNCTION
        public override DataTable Select()
        {
            throw new NotImplementedException();
        }


        public DataTable Select(DateTime OTime)
        {
            DataTable OResult = null;

            try
            {
                Monitor.Enter(this.m_OIOInterrupt);


                int I32RowIndex = this.m_OList.SelectRowIndex("DATE", OTime.ToString("yyyy.MM"));

                if (I32RowIndex == -1)
                {
                    OResult = this.m_OSchema.Select();
                }
                else
                {
                    CTable OTable = new CTable(OTime.ToString("yyyy.MM"), this.m_OList.Select(I32RowIndex, "FILE").ToString());
                    OResult = OTable.Select();
                    OTable.Dispose();
                    OTable = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OIOInterrupt);
            }

            return OResult;
        }


        public DataTable Select(DateTime OTime, bool BNeedCriticalSection)
        {
            DataTable OResult = null;

            try
            {
                if (BNeedCriticalSection == true) Monitor.Enter(this.m_OIOInterrupt);


                int I32RowIndex = this.m_OList.SelectRowIndex("DATE", OTime.ToString("yyyy.MM"));

                if (I32RowIndex == -1)
                {
                    OResult = this.m_OSchema.Select();
                }
                else
                {
                    CTable OTable = new CTable(OTime.ToString("yyyy.MM"), this.m_OList.Select(I32RowIndex, "FILE").ToString());
                    OResult = OTable.Select();
                    OTable.Dispose();
                    OTable = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                if (BNeedCriticalSection == true) Monitor.Exit(this.m_OIOInterrupt);
            }

            return OResult;
        }


        public int InsertRow()
        {
            int I32Result = 0;

            try
            {
                this.m_ODataSource.Rows.Add(this.m_ODataSource.NewRow());
                I32Result = this.m_ODataSource.Rows.Count - 1;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return I32Result;
        }


        public void Update(int I32RowIndex, string StrColumnName, object OData)
        {
            try
            {
                if (this.m_ODataSource.Columns[StrColumnName].DataType.Equals(OData.GetType()) == false)
                {
                    throw new Exception("Data type of table and recieved data is not matched");
                }
                this.m_ODataSource.Rows[I32RowIndex][StrColumnName] = OData;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public override void Commit()
        {
            bool BMonitorEntered = false;

            try
            {
                BMonitorEntered = Monitor.TryEnter(this.m_OIOInterrupt, 50);
                if (BMonitorEntered == false) return;

                DataTable ODataSource = this.CopyDataSouce();
                if (ODataSource == null) return;


                Dictionary<string, List<List<string>>> OCollection = new Dictionary<string, List<List<string>>>();
                foreach (DataRow _Item1 in ODataSource.Rows)
                {
                    string StrDate = ((string)_Item1["DATETIME"]).Substring(0, "yyyy.MM".Length);
                    bool BExists = false;

                    foreach (string _Item2 in OCollection.Keys)
                    {
                        if (_Item2 != StrDate) continue;

                        BExists = true;
                        break;
                    }

                    if (BExists == false) OCollection.Add(StrDate, new List<List<string>>());
                }


                foreach (DataRow _Item1 in ODataSource.Rows)
                {
                    List<string> LstStrData = new List<string>();

                    for (int _Index2 = 0; _Index2 < ODataSource.Columns.Count; _Index2++)
                    {
                        if (_Item1[_Index2] == DBNull.Value) LstStrData.Add(string.Empty);
                        else LstStrData.Add(_Item1[_Index2].ToString());
                    }

                    OCollection[((string)_Item1["DATETIME"]).Substring(0, "yyyy.MM".Length)].Add(LstStrData);
                }


                foreach (string _Item1 in OCollection.Keys)
                {
                    string StrPath = string.Empty;
                    int I32RowIndex = this.m_OList.SelectRowIndex("DATE", _Item1);

                    if (I32RowIndex == -1) //파일 없음
                    {
                        Directory.CreateDirectory(string.Format(this.m_StrRoot + "\\" + STANDARD_DIR, Convert.ToDateTime(_Item1)));
                        StrPath = string.Format(this.m_StrRoot + "\\" + STANDARD_PATH, Convert.ToDateTime(_Item1));

                        I32RowIndex = this.m_OList.InsertRow();
                        this.m_OList.Update(I32RowIndex, "DATE", _Item1);
                        this.m_OList.Update(I32RowIndex, "FILE", StrPath);
                        this.m_OList.Commit();

                        OCollection[_Item1].Insert(0, new List<string>());
                        OCollection[_Item1].Insert(1, new List<string>());
                        foreach (DataColumn _Item2 in ODataSource.Columns)
                        {
                            OCollection[_Item1][0].Add(_Item2.ColumnName);
                            OCollection[_Item1][1].Add(this.GetDataTypeString(_Item2.DataType));
                        }
                    }
                    else //기존 파일 있음
                    {
                        StrPath = (string)this.m_OList.Select(I32RowIndex, "FILE");

                        if (System.IO.File.Exists(StrPath) == false)
                        {
                            OCollection[_Item1].Insert(0, new List<string>());
                            OCollection[_Item1].Insert(1, new List<string>());
                            foreach (DataColumn _Item2 in ODataSource.Columns)
                            {
                                OCollection[_Item1][0].Add(_Item2.ColumnName);
                                OCollection[_Item1][1].Add(this.GetDataTypeString(_Item2.DataType));
                            }
                        }
                    }

                    CCsvFile OFile = new CCsvFile(StrPath);
                    OFile.BeginWrite(true, Encoding.Default);
                    OFile.WriteAll(OCollection[_Item1]);
                    OFile.EndWrite();
                    OFile.Dispose();
                    OFile = null;
                }


                if (OCollection != null)
                {
                    OCollection.Clear();
                    OCollection = null;
                }
                if (ODataSource != null)
                {
                    ODataSource.Dispose();
                    ODataSource = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                if (BMonitorEntered == true) Monitor.Exit(this.m_OIOInterrupt);
            }
        }


        public void AutoDelete()
        {

        }


        private DataTable CopyDataSouce()
        {
            DataTable OResult = null;

            try
            {
                Monitor.Enter(this.m_ODataInterrupt);

                if (this.m_ODataSource.Rows.Count != 0)
                {
                    OResult = this.m_ODataSource.Copy();
                    this.m_ODataSource.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_ODataInterrupt);
            }

            return OResult;
        }


        public void BeginSyncData()
        {
            try
            {
                Monitor.Enter(this.m_ODataInterrupt);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void EndSyncData()
        {
            try
            {
                Monitor.Exit(this.m_ODataInterrupt);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void BeginSyncIO()
        {
            try
            {
                Monitor.Enter(this.m_OIOInterrupt);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void EndSyncIO()
        {
            try
            {
                Monitor.Exit(this.m_OIOInterrupt);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public override void Dispose()
        {
            try
            {
                if (this.m_ODataSource != null)
                {
                    this.m_ODataSource.Dispose();
                    this.m_ODataSource = null;
                }
                if (this.m_OSchema != null)
                {
                    this.m_OSchema.Dispose();
                    this.m_OSchema = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
    }
}
