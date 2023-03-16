using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CCsvFile : IDisposable
    {
        #region VARIABLE
        private string m_StrFile = string.Empty;
        private StreamReader m_OReader = null;
        private StreamWriter m_OWriter = null;
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CCsvFile(string StrFile)
        {
            try
            {
                this.m_StrFile = StrFile;
            }
            catch (System.Exception ex)
            {
                CError.Throw(ex);
            }
        }


        ~CCsvFile()
        {
            try
            {
                this.Dispose();
            }
            catch (System.Exception ex)
            {
                CError.Ignore(ex);
            }
        }
        #endregion


        #region FUNCTION
        public void Create()
        {
            try
            {
                System.IO.File.Create(this.m_StrFile);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public bool Exists()
        {
            bool BResult = false;

            try
            {
                BResult = System.IO.File.Exists(this.m_StrFile);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return BResult;
        }


        public void BeginRead(Encoding EEncoding)
        {
            try
            {
                if (this.m_OReader == null)
                {
                    this.m_OReader = new StreamReader(this.m_StrFile, EEncoding);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public List<string> ReadLine()
        {
            List<string> LstStrResult = null;

            try
            {
                string StrText = this.m_OReader.ReadLine();
                LstStrResult = this.GetFields(StrText);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return LstStrResult;
        }


        public List<List<string>> ReadAll()
        {
            List<List<string>> LstOResult = null;

            try
            {
                string StrText = string.Empty;
                LstOResult = new List<List<string>>();

                while ((StrText = this.m_OReader.ReadLine()) != null)
                {
                    LstOResult.Add(this.GetFields(StrText));
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return LstOResult;
        }


        public void EndRead()
        {
            try
            {
                if (this.m_OReader != null)
                {
                    this.m_OReader.Close();
                    this.m_OReader.Dispose();
                    this.m_OReader = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void BeginWrite(bool BIsAppend, Encoding EEncoding)
        {
            try
            {
                if (this.m_OWriter == null)
                {
                    this.m_OWriter = new StreamWriter(this.m_StrFile, BIsAppend, EEncoding);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void WriteLine(List<string> LstStrField)
        {
            try
            {
                string StrLine = this.GetLine(LstStrField);
                this.m_OWriter.WriteLine(StrLine);
                this.m_OWriter.Flush();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void WriteAll(List<List<string>> LstOField)
        {
            try
            {
                string StrLine = null;
                foreach (List<string> _Item in LstOField)
                {
                    StrLine = this.GetLine(_Item);
                    this.m_OWriter.WriteLine(StrLine);
                }
                this.m_OWriter.Flush();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public void EndWrite()
        {
            try
            {
                if (this.m_OWriter != null)
                {
                    this.m_OWriter.Close();
                    this.m_OWriter.Dispose();
                    this.m_OWriter = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private List<string> GetFields(string StrLine)
        {
            List<string> LstStrResult = null;

            try
            {
                LstStrResult = new List<string>();

                bool BIsCompleted = true;
                string StrTemp1 = string.Empty;
                string StrTemp2 = string.Empty;
                string[] ArrStrField = StrLine.Split(',');

                for (int _Index = 0; _Index < ArrStrField.Length; _Index++)
                {
                    if (BIsCompleted == true) StrTemp1 = ArrStrField[_Index];
                    else StrTemp1 += "," + ArrStrField[_Index];

                    StrTemp2 = StrTemp1.Replace("\"\"", "");
                    if (StrTemp2.Contains("\"") == true)
                    {
                        if ((StrTemp2.Length == 1) ||
                           ((StrTemp2.StartsWith("\"") == false) || (StrTemp2.EndsWith("\"") == false)))
                        {
                            BIsCompleted = false;
                            continue;
                        }
                    }

                    if ((StrTemp1.Length > 1) &&
                       ((StrTemp1.StartsWith("\"") == true) && (StrTemp1.EndsWith("\"") == true)))
                    {
                        StrTemp1 = StrTemp1.Substring(1, StrTemp1.Length - 2);
                    }

                    LstStrResult.Add(StrTemp1.Replace("\"\"", "\""));
                    BIsCompleted = true;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return LstStrResult;
        }


        private string GetLine(List<string> LstStrField)
        {
            string StrResult = null;

            try
            {
                for (int _Index = 0; _Index < LstStrField.Count; _Index++)
                {
                    if (LstStrField[_Index].Contains("\"") == true)
                    {
                        LstStrField[_Index] = LstStrField[_Index].Replace("\"", "\"\"");
                    }
                    if ((LstStrField[_Index].Contains("\"") == true) ||
                       (LstStrField[_Index].Contains(",") == true))
                    {
                        LstStrField[_Index] = "\"" + LstStrField[_Index] + "\"";
                    }
                }

                StrResult = string.Empty;
                for (int _Index = 0; _Index < LstStrField.Count - 1; _Index++)
                {
                    StrResult += LstStrField[_Index] + ",";
                }
                StrResult += LstStrField[LstStrField.Count - 1];
            }
            catch (System.Exception ex)
            {
                CError.Throw(ex);
            }

            return StrResult;
        }


        public void Dispose()
        {
            try
            {
                this.EndRead();
                this.EndWrite();
            }
            catch (System.Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
    }
}
