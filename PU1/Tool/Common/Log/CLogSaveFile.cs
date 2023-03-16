using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CLogSaveFile
    {
        #region CONST
        private const string DIRECTORY = "{0:yyyy}\\{0:MM}";
        private const string FILE = "{0:yyyy-MM-dd}.txt";

        #endregion


        #region VARIABLE
        private string m_StrDirectory = null;
        private List<CLog> m_LstOLog = null;
        private object m_OInterrupt = null;

        private TimeSpan m_ODayStartTime = TimeSpan.Zero;
        #endregion


        #region PROPERTIES
        public string StrDirectory
        {
            get { return this.m_StrDirectory; }
            set { this.m_StrDirectory = value; }
        }


        public TimeSpan ODayStartTime
        {
            get { return this.m_ODayStartTime; }
            set { this.m_ODayStartTime = value; }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CLogSaveFile(string StrDirectory)
        {
            try
            {
                this.m_StrDirectory = StrDirectory;
                this.m_LstOLog = new List<CLog>();
                this.m_OInterrupt = new object();

                CLogSaveTool.It.SetFile(this);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region FUNCTION
        public void SetLog(string StrText)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                CLog OItem = new CLog(StrText);
                this.m_LstOLog.Add(OItem);
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


        public void SetLog(DateTime ODateTime, string StrText)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                CLog OItem = new CLog(ODateTime, StrText);
                this.m_LstOLog.Add(OItem);
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


        public void SetLog(CLog OItem)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                this.m_LstOLog.Add(OItem);
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


        private CLog GetLog()
        {
            CLog OResult = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                if (this.m_LstOLog.Count > 0)
                {
                    OResult = this.m_LstOLog[0];
                    this.m_LstOLog.RemoveAt(0);
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


        public void Save()
        {
            try
            {
                StreamWriter OWriter = null;
                string StrFile = string.Empty;
                string StrDirectory = string.Empty;

                CLog OItem = null;
                DateTime ODateTime = DateTime.MinValue;

                while ((OItem = this.GetLog()) != null)
                {
                    if (OWriter == null)
                    {
                        if (this.m_ODayStartTime > OItem.OTime.TimeOfDay) ODateTime = OItem.OTime.AddDays(-1);
                        else ODateTime = OItem.OTime;
                        StrDirectory = this.m_StrDirectory + "\\" + String.Format(DIRECTORY, ODateTime);
                        StrFile = StrDirectory + "\\" + String.Format(FILE, ODateTime);

                        if (Directory.Exists(StrDirectory) == false)
                        {
                            Directory.CreateDirectory(StrDirectory);
                        }
                        OWriter = new StreamWriter(StrFile, true, Encoding.Default);
                    }
                    else
                    {
                        if ((ODateTime.Date != OItem.OTime.Date) && (this.m_ODayStartTime <= OItem.OTime.TimeOfDay))
                        {
                            if (OWriter != null)
                            {
                                OWriter.Flush();
                                OWriter.Close();
                                OWriter.Dispose();
                            }


                            ODateTime = OItem.OTime;
                            StrDirectory = this.m_StrDirectory + "\\" + String.Format(DIRECTORY, ODateTime);
                            StrFile = StrDirectory + "\\" + String.Format(FILE, ODateTime);

                            if (Directory.Exists(StrDirectory) == false)
                            {
                                Directory.CreateDirectory(StrDirectory);
                            }
                            OWriter = new StreamWriter(StrFile, true, Encoding.Default);
                        }
                    }

                    OWriter.WriteLine(String.Format(CLog.LOG + OItem.StrText, OItem.OTime));
                }

                if (OWriter != null)
                {
                    OWriter.Flush();
                    OWriter.Close();
                    OWriter.Dispose();
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
