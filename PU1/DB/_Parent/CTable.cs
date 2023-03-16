using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CTable : ATable
    {
        #region VARIABLE
        private DataTable m_ODataSource = null;
        private CCsvFile m_OFile = null;
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CTable(string StrName, string StrPath)
            : base(StrName)
        {
            try
            {
                this.m_OFile = new CCsvFile(StrPath);
                this.m_OFile.BeginRead(Encoding.Default);
                List<List<string>> LstOSource = this.m_OFile.ReadAll();
                this.m_OFile.EndRead();


                this.m_ODataSource = new DataTable();
                List<string> LstOColumnName = LstOSource[ATable.TABLE_COLUMN_INDEX];
                List<string> LstODataType = LstOSource[ATable.TABLE_DATATYPE_INDEX];
                for (int _Index = 0; _Index < LstOColumnName.Count; _Index++)
                {
                    this.m_ODataSource.Columns.Add(LstOColumnName[_Index], base.GetDataType(LstODataType[_Index]));
                }


                DataRow ORow = null;
                List<string> LstOData = null;
                for (int _Index = ATable.TABLE_DATATYPE_INDEX + 1; _Index < LstOSource.Count; _Index++)
                {
                    LstOData = LstOSource[_Index];

                    ORow = this.m_ODataSource.NewRow();
                    for (int _Index2 = 0; _Index2 < this.m_ODataSource.Columns.Count; _Index2++)
                    {
                        if (String.IsNullOrEmpty(LstOData[_Index2]) == true) continue;
                        ORow[_Index2] = base.ConvertData(this.m_ODataSource.Columns[_Index2].DataType, LstOData[_Index2]);
                    }

                    this.m_ODataSource.Rows.Add(ORow);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        ~CTable()
        {
            try
            {
                this.Dispose();
            }
            catch (Exception ex)
            {
                CError.Ignore(ex);
            }
        }
        #endregion


        #region FUNCTION
        public override DataTable Select()
        {
            DataTable OResult = null;

            try
            {
                OResult = this.m_ODataSource.Copy();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return OResult;
        }


        public object Select(int I32RowIndex, string StrColumnName)
        {
            object OResult = null;

            try
            {
                if (this.m_ODataSource.Rows[I32RowIndex][StrColumnName] != DBNull.Value)
                {
                    OResult = this.m_ODataSource.Rows[I32RowIndex][StrColumnName];
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return OResult;
        }


        public int SelectRowIndex(string StrColumnName, object OComparingValue)
        {
            int I32Result = -1;

            try
            {
                if (OComparingValue != null)
                {
                    for (int _Index1 = 0; _Index1 < this.m_ODataSource.Rows.Count; _Index1++)
                    {
                        if (this.m_ODataSource.Rows[_Index1][StrColumnName] == DBNull.Value) continue;
                        if (this.m_ODataSource.Rows[_Index1][StrColumnName].Equals(OComparingValue) == false) continue;

                        I32Result = _Index1;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return I32Result;
        }


        public int SelectRowIndex(string[] ArrStrColumnName, object[] ArrOComparingValue)
        {
            int I32Result = -1;

            try
            {
                if (ArrStrColumnName.Length != ArrOComparingValue.Length)
                {
                    throw new Exception("Count Of Column and Data is not matched");
                }


                bool BEqual = true;
                for (int _Index1 = 0; _Index1 < this.m_ODataSource.Rows.Count; _Index1++)
                {
                    BEqual = true;

                    for (int _Index2 = 0; _Index2 < ArrStrColumnName.Length; _Index2++)
                    {
                        if (this.m_ODataSource.Rows[_Index1][ArrStrColumnName[_Index2]] == DBNull.Value)
                        {
                            BEqual = false;
                            break;
                        }
                        if (this.m_ODataSource.Rows[_Index1][ArrStrColumnName[_Index2]].Equals(ArrOComparingValue[_Index2]) == false)
                        {
                            BEqual = false;
                        }
                    }

                    if (BEqual == true)
                    {
                        I32Result = _Index1;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return I32Result;
        }


        public List<int> SelectRowIndexs(string StrColumnName, object OComparingValue)
        {
            List<int> LstI32Result = null;

            try
            {
                if (OComparingValue != null)
                {
                    LstI32Result = new List<int>();
                    for (int _Index1 = 0; _Index1 < this.m_ODataSource.Rows.Count; _Index1++)
                    {
                        if (this.m_ODataSource.Rows[_Index1][StrColumnName] == DBNull.Value) continue;
                        if (this.m_ODataSource.Rows[_Index1][StrColumnName].Equals(OComparingValue) == false) continue;

                        LstI32Result.Add(_Index1);
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return LstI32Result;
        }


        public List<int> SelectRowIndexs(string[] ArrStrColumnName, object[] ArrOComparingValue)
        {
            List<int> LstI32Result = null;

            try
            {
                if (ArrStrColumnName.Length != ArrOComparingValue.Length)
                {
                    throw new Exception("Count Of Column and Data is not matched");
                }


                bool BEqual = true;
                LstI32Result = new List<int>();
                for (int _Index1 = 0; _Index1 < this.m_ODataSource.Rows.Count; _Index1++)
                {
                    BEqual = true;

                    for (int _Index2 = 0; _Index2 < ArrStrColumnName.Length; _Index2++)
                    {
                        if (this.m_ODataSource.Rows[_Index1][ArrStrColumnName[_Index2]] == DBNull.Value)
                        {
                            BEqual = false;
                            break;
                        }
                        if (this.m_ODataSource.Rows[_Index1][ArrStrColumnName[_Index2]].Equals(ArrOComparingValue[_Index2]) == false)
                        {
                            BEqual = false;
                            break;
                        }
                    }

                    if (BEqual == true) LstI32Result.Add(_Index1);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return LstI32Result;
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


        public void DeleteRow(int I32RowIndex)
        {
            try
            {
                this.m_ODataSource.Rows.RemoveAt(I32RowIndex);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void DeleteRow(string StrColumnName, object OComparingValue)
        {
            try
            {
                if (OComparingValue != null)
                {
                    for (int _Index1 = this.m_ODataSource.Rows.Count - 1; _Index1 >= 0; _Index1--)
                    {
                        if (this.m_ODataSource.Rows[_Index1][StrColumnName] == DBNull.Value) continue;
                        if (this.m_ODataSource.Rows[_Index1][StrColumnName].Equals(OComparingValue) == false) continue;

                        this.m_ODataSource.Rows.RemoveAt(_Index1);
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public override void Commit()
        {
            try
            {
                List<List<string>> LstOSource = new List<List<string>>();

                List<string> LstStrColumnName = new List<string>();
                List<string> LstStrDataType = new List<string>();
                foreach (DataColumn _Item in this.m_ODataSource.Columns)
                {
                    LstStrColumnName.Add(_Item.ColumnName);
                    LstStrDataType.Add(base.GetDataTypeString(_Item.DataType));
                }
                LstOSource.Add(LstStrColumnName);
                LstOSource.Add(LstStrDataType);

                List<string> LstStrData = null;
                foreach (DataRow _Item1 in this.m_ODataSource.Rows)
                {
                    LstStrData = new List<string>();

                    for (int _Index2 = 0; _Index2 < this.m_ODataSource.Columns.Count; _Index2++)
                    {
                        if (_Item1[_Index2] == DBNull.Value) LstStrData.Add(string.Empty);
                        else LstStrData.Add(_Item1[_Index2].ToString());
                    }

                    LstOSource.Add(LstStrData);
                }


                this.m_OFile.BeginWrite(false, Encoding.Default);
                this.m_OFile.WriteAll(LstOSource);
                this.m_OFile.EndWrite();
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

                if (this.m_OFile != null)
                {
                    this.m_OFile.Dispose();
                    this.m_OFile = null;
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
