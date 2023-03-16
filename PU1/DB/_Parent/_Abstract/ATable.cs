using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public abstract class ATable : IDisposable
    {
        #region CONST
        protected const int TABLE_COLUMN_INDEX = 0;
        protected const int TABLE_DATATYPE_INDEX = 1;
        #endregion


        #region VARIABLE
        private string m_StrName = string.Empty;
        #endregion


        #region PROPERTIES
        public string StrName
        {
            get { return this.m_StrName; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public ATable(string StrName)
        {
            try
            {
                this.m_StrName = StrName;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region FUNCTION
        protected Type GetDataType(string StrKind)
        {
            Type OResult = null;

            try
            {
                if (StrKind == "BOOL") OResult = typeof(bool);
                else if (StrKind == "BYTE") OResult = typeof(byte);
                else if (StrKind == "USHORT") OResult = typeof(ushort);
                else if (StrKind == "UINT") OResult = typeof(uint);
                else if (StrKind == "ULONG") OResult = typeof(ulong);
                else if (StrKind == "SBYTE") OResult = typeof(sbyte);
                else if (StrKind == "SHORT") OResult = typeof(short);
                else if (StrKind == "INT") OResult = typeof(int);
                else if (StrKind == "LONG") OResult = typeof(long);
                else if (StrKind == "FLOAT") OResult = typeof(float);
                else if (StrKind == "DOUBLE") OResult = typeof(double);
                else if (StrKind == "DECIMAL") OResult = typeof(decimal);
                else if (StrKind == "CHAR") OResult = typeof(char);
                else if (StrKind == "STRING") OResult = typeof(string);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return OResult;
        }


        protected string GetDataTypeString(Type OKind)
        {
            string StrResult = string.Empty;

            try
            {
                if (OKind == typeof(bool)) StrResult = "BOOL";
                else if (OKind == typeof(byte)) StrResult = "BYTE";
                else if (OKind == typeof(ushort)) StrResult = "USHORT";
                else if (OKind == typeof(uint)) StrResult = "UINT";
                else if (OKind == typeof(ulong)) StrResult = "ULONG";
                else if (OKind == typeof(sbyte)) StrResult = "SBYTE";
                else if (OKind == typeof(short)) StrResult = "SHORT";
                else if (OKind == typeof(int)) StrResult = "INT";
                else if (OKind == typeof(long)) StrResult = "LONG";
                else if (OKind == typeof(float)) StrResult = "FLOAT";
                else if (OKind == typeof(double)) StrResult = "DOUBLE";
                else if (OKind == typeof(decimal)) StrResult = "DECIMAL";
                else if (OKind == typeof(char)) StrResult = "CHAR";
                else if (OKind == typeof(string)) StrResult = "STRING";
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return StrResult;
        }


        protected object ConvertData(Type OKind, string StrData)
        {
            object OResult = null;

            try
            {
                if (OKind == typeof(bool)) OResult = Convert.ToBoolean(StrData);
                else if (OKind == typeof(byte)) OResult = Convert.ToByte(StrData);
                else if (OKind == typeof(ushort)) OResult = Convert.ToUInt16(StrData);
                else if (OKind == typeof(uint)) OResult = Convert.ToUInt32(StrData);
                else if (OKind == typeof(ulong)) OResult = Convert.ToUInt64(StrData);
                else if (OKind == typeof(sbyte)) OResult = Convert.ToSByte(StrData);
                else if (OKind == typeof(short)) OResult = Convert.ToInt16(StrData);
                else if (OKind == typeof(int)) OResult = Convert.ToInt32(StrData);
                else if (OKind == typeof(long)) OResult = Convert.ToInt64(StrData);
                else if (OKind == typeof(float)) OResult = Convert.ToSingle(StrData);
                else if (OKind == typeof(double)) OResult = Convert.ToDouble(StrData);
                else if (OKind == typeof(decimal)) OResult = Convert.ToDecimal(StrData);
                else if (OKind == typeof(char)) OResult = Convert.ToChar(StrData);
                else if (OKind == typeof(string)) OResult = Convert.ToString(StrData);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return OResult;
        }


        public abstract DataTable Select();


        public abstract void Commit();


        public abstract void Dispose();
        #endregion
    }
}
