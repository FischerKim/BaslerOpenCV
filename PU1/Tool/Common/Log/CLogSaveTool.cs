using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CLogSaveTool : IDisposable
    {
        #region SINGLE TON
        private static CLogSaveTool Src_It = null;

        public static CLogSaveTool It
        {
            get
            {
                CLogSaveTool OResult = null;

                try
                {
                    if (CLogSaveTool.Src_It == null)
                    {
                        CLogSaveTool.Src_It = new CLogSaveTool();
                    }

                    OResult = CLogSaveTool.Src_It;
                }
                catch (Exception ex)
                {
                    CError.Throw(ex);
                }

                return OResult;
            }
        }
        #endregion

        #region STATIC VARIABLE
        public static bool USE_DELETE = false;
        public static List<string> LOG_MANAGE_PATH = null;
        public static ETIME_UNIT DELETE_TIME_UNIT = ETIME_UNIT.MONTH;
        public static int LOG_MANAGE_PERIOD = 0;
        #endregion


        #region VARIABLE
        private DateTime m_OTime = DateTime.MinValue;

        private List<CLogSaveFile> m_LstOFile = null;
        private object m_OInterrupt = null;

        private Thread m_OWorker = null;
        private bool m_BDoWork = false;
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        protected CLogSaveTool()
        {
            try
            {
                this.m_OTime = DateTime.Now;
                this.m_LstOFile = new List<CLogSaveFile>();
                this.m_OInterrupt = new object();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        ~CLogSaveTool()
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
        public void Load()
        {
            try
            {
                if (USE_DELETE == true)
                {
                    CLogSaveTool.Delete();
                }

                this.BeginWork();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

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
                        CLogSaveFile OFile = null;

                        while ((OFile = this.GetFile(I32Index)) != null)
                        {
                            I32Index += 1;
                            OFile.Save();
                        }

                        if ((USE_DELETE == true) && (this.m_OTime.Date != DateTime.Now.Date))
                        {
                            this.m_OTime = DateTime.Now;
                            CLogSaveTool.Delete();
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
                CError.Ignore(ex);
            }
        }


        public void SetFile(CLogSaveFile OTool)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                this.m_LstOFile.Add(OTool);
            }
            catch (Exception ex)
            {
                CError.Ignore(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        private CLogSaveFile GetFile(int I32Index)
        {
            CLogSaveFile OResult = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                if ((I32Index >= 0) && (I32Index < this.m_LstOFile.Count))
                {
                    OResult = this.m_LstOFile[I32Index];
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


        public void Dispose()
        {
            try
            {
                this.EndWork();
            }
            catch (Exception ex)
            {
                CError.Ignore(ex);
            }
        }
        #endregion

        #region STATIC FUNCTION
        public static void Delete()
        {
            try
            {
                DateTime ODateTime = DateTime.Now;
                string StrPath = string.Empty;

                if (DELETE_TIME_UNIT == ETIME_UNIT.MONTH)
                {
                    for (DateTime _Item1 = ODateTime; _Item1 >= ODateTime.AddMonths(-12); _Item1 = _Item1.AddMonths(-1))
                    {
                        if (_Item1 < ODateTime.AddMonths((LOG_MANAGE_PERIOD) * -1))
                        {
                            if (_Item1.Month != 12)
                            {
                                foreach (string _Item2 in LOG_MANAGE_PATH)
                                {
                                    StrPath = String.Format(_Item2, _Item1);
                                    if (Directory.Exists(StrPath) == true) Directory.Delete(StrPath);
                                }
                            }
                            else
                            {
                                foreach (string _Item2 in LOG_MANAGE_PATH)
                                {
                                    StrPath = String.Format(_Item2, _Item1);
                                    StrPath = Directory.GetParent(StrPath).FullName;
                                    if (Directory.Exists(StrPath) == true) Directory.Delete(StrPath, true);
                                }
                            }
                        }
                    }
                }
                else if (DELETE_TIME_UNIT == ETIME_UNIT.DAY)
                {
                    for (DateTime _Item1 = ODateTime; _Item1 >= ODateTime.AddDays(-365); _Item1 = _Item1.AddDays(-1))
                    {
                        if (_Item1 < ODateTime.AddDays((LOG_MANAGE_PERIOD) * -1))
                        {
                            if (_Item1.Day != DateTime.DaysInMonth(_Item1.Year, _Item1.Month))
                            {
                                foreach (string _Item2 in LOG_MANAGE_PATH)
                                {
                                    StrPath = String.Format(_Item2, _Item1);
                                    if (File.Exists(StrPath) == true) File.Delete(StrPath);
                                }
                            }
                            else
                            {
                                if (_Item1.Month != 12)
                                {
                                    foreach (string _Item2 in LOG_MANAGE_PATH)
                                    {
                                        StrPath = String.Format(_Item2, _Item1);
                                        StrPath = (new FileInfo(StrPath)).DirectoryName;
                                        if (Directory.Exists(StrPath) == true) Directory.Delete(StrPath, true);
                                    }
                                }
                                else
                                {
                                    foreach (string _Item2 in LOG_MANAGE_PATH)
                                    {
                                        StrPath = String.Format(_Item2, _Item1);
                                        StrPath = (new FileInfo(StrPath)).DirectoryName;
                                        StrPath = Directory.GetParent(StrPath).FullName;
                                        if (Directory.Exists(StrPath) == true) Directory.Delete(StrPath, true);
                                    }
                                }
                            }
                        }
                    }
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
