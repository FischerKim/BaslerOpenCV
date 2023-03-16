using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CImageSaveTool : IDisposable
    {
        #region SINGLE TON
        private static CImageSaveTool Src_It = null;

        public static CImageSaveTool It
        {
            get
            {
                CImageSaveTool OResult = null;

                try
                {
                    if (CImageSaveTool.Src_It == null)
                    {
                        CImageSaveTool.Src_It = new CImageSaveTool();
                    }

                    OResult = CImageSaveTool.Src_It;
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
        /// <summary>
        /// 이미지 삭제 여부
        /// </summary>
        public static bool USE_DELETE = false;


        /// <summary>
        /// 이미지 저장 경로 (이미지 자동 삭제 기능과 연관)
        /// </summary>
        public static List<string> IMAGE_MANAGE_DIR = null;


        /// <summary>
        /// 이미지를 삭제하는 기간단위
        /// </summary>
        public static ETIME_UNIT DELETE_TIME_UNIT = ETIME_UNIT.MONTH;


        /// <summary>
        /// 이미지 보관 기간 (이미지 자동 삭제 기능과 연관)
        /// </summary>
        public static int IMAGE_MANAGE_PERIOD = 0;
        #endregion


        #region VARIABLE
        private DateTime m_OTime = DateTime.MinValue;

        private List<CImageSaveFile> m_OBuffer = null;
        private string m_StrDirectory = string.Empty;
        private object m_OInterrupt = null;

        private Thread m_OWorker = null;
        private bool m_BDoWork = false;
        #endregion


        #region PROPERTIES
        public int I32Count
        {
            get
            {
                int I32Result = 0;

                try
                {
                    Monitor.Enter(this.m_OInterrupt);

                    I32Result = this.m_OBuffer.Count;
                }
                catch (Exception ex)
                {
                    CError.Throw(ex);
                }
                finally
                {
                    Monitor.Exit(this.m_OInterrupt);
                }

                return I32Result;
            }
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CImageSaveTool()
        {
            try
            {
                this.m_OTime = DateTime.Now;
                this.m_OBuffer = new List<CImageSaveFile>();
                this.m_OInterrupt = new object();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        ~CImageSaveTool()
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
                    CImageSaveTool.Delete();
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
                        CImageSaveFile OFile = this.Get();
                        if (OFile != null && OFile.OImage != null)
                        {
                            OFile.Save();
                            OFile.Dispose();
                            OFile = null;
                        }
                        this.Remove();

                        if ((USE_DELETE == true) && (this.m_OTime.Date != DateTime.Now.Date))
                        {
                            this.m_OTime = DateTime.Now;
                            CImageSaveTool.Delete();
                        }
                    }
                    catch (Exception ex)
                    {
                        CError.Ignore(ex);
                    }

                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
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


        private CImageSaveFile Get()
        {
            CImageSaveFile OResult = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                if (this.m_OBuffer.Count > 0)
                {
                    OResult = this.m_OBuffer[0];
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

        private CImageSaveFile Remove()
        {
            CImageSaveFile OResult = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                if (this.m_OBuffer.Count > 0)
                {
                    if(this.m_OBuffer[0].OImage!=null) this.m_OBuffer[0].OImage.Dispose();
                    this.m_OBuffer.RemoveAt(0);
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

        public void Set(CImageSaveFile OImageFileInfo)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);
                if (OImageFileInfo.OImage != null)
                {
                    this.m_OBuffer.Add(OImageFileInfo);
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


        public void Set(List<CImageSaveFile> LstOImageFileInfo)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                this.m_OBuffer.AddRange(LstOImageFileInfo);
                LstOImageFileInfo.Clear();
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


        public void Dispose()
        {
            try
            {
                this.EndWork();

                if (this.m_OBuffer != null)
                {
                    this.m_OBuffer.Clear();
                    this.m_OBuffer = null;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region STATIC FUNCTION
        public static void Delete()
        {
            try
            {
                DateTime ODateTime = DateTime.Now;
                string StrDirectory = string.Empty;

                if (DELETE_TIME_UNIT == ETIME_UNIT.MONTH)
                {
                    for (DateTime _Item1 = ODateTime; _Item1 >= ODateTime.AddMonths(-12); _Item1 = _Item1.AddMonths(-1))
                    {
                        if (_Item1 < ODateTime.AddMonths((IMAGE_MANAGE_PERIOD) * -1))
                        {
                            if (_Item1.Month != 12)
                            {
                                foreach (string _Item2 in IMAGE_MANAGE_DIR)
                                {
                                    StrDirectory = String.Format(_Item2, _Item1);
                                    if (Directory.Exists(StrDirectory) == true) Directory.Delete(StrDirectory, true);
                                }
                            }
                            else
                            {
                                foreach (string _Item2 in IMAGE_MANAGE_DIR)
                                {
                                    StrDirectory = String.Format(_Item2, _Item1);
                                    StrDirectory = Directory.GetParent(StrDirectory).FullName;
                                    if (Directory.Exists(StrDirectory) == true) Directory.Delete(StrDirectory, true);
                                }
                            }
                        }
                    }
                }
                else if (DELETE_TIME_UNIT == ETIME_UNIT.DAY)
                {
                    for (DateTime _Item1 = ODateTime; _Item1 >= ODateTime.AddDays(-365); _Item1 = _Item1.AddDays(-1))
                    {
                        if (_Item1 < ODateTime.AddDays((IMAGE_MANAGE_PERIOD) * -1))
                        {
                            if (_Item1.Day != DateTime.DaysInMonth(_Item1.Year, _Item1.Month))
                            {
                                foreach (string _Item2 in IMAGE_MANAGE_DIR)
                                {
                                    StrDirectory = String.Format(_Item2, _Item1);
                                    if (Directory.Exists(StrDirectory) == true) Directory.Delete(StrDirectory, true);
                                }
                            }
                            else
                            {
                                if (_Item1.Month != 12)
                                {
                                    foreach (string _Item2 in IMAGE_MANAGE_DIR)
                                    {
                                        StrDirectory = String.Format(_Item2, _Item1);
                                        StrDirectory = Directory.GetParent(StrDirectory).FullName;
                                        if (Directory.Exists(StrDirectory) == true) Directory.Delete(StrDirectory, true);
                                    }
                                }
                                else
                                {
                                    foreach (string _Item2 in IMAGE_MANAGE_DIR)
                                    {
                                        StrDirectory = String.Format(_Item2, _Item1);
                                        StrDirectory = Directory.GetParent(StrDirectory).FullName;
                                        StrDirectory = Directory.GetParent(StrDirectory).FullName;
                                        if (Directory.Exists(StrDirectory) == true) Directory.Delete(StrDirectory, true);
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
