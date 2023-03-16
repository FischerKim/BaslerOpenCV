using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing.Imaging;
using System.Data;
using PostMultipart;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PU1
{
    public class CInspectionTool :IDisposable
    {
        #region VARIABLE
        private CResult OResult = null; 
        private CDisplayTool m_ODisplayer = null; 
        private bool m_BInspectMark = false;
        private CResult m_OResult = null;

        private object m_OInterrupt = null;
        private Thread m_OWorker = null;
        private bool m_BDoWork = false;

        private ResultExportedHandler m_OResultExported = null;
        private CRestfulAPI m_ORestful = null;
        private bool m_BSave = false;
        private int m_OI32Count = 0;
        #endregion

        #region DELEGATE & EVENT
        public delegate void ResultExportedHandler(CResult OResult);
        #endregion


        #region PROPERTIES
        public bool BSave
        {
            set { this.m_BSave = value; }
        }

        public CDisplayTool ODisplayer
        {
            get { return this.m_ODisplayer; }
        }

        public ResultExportedHandler OResultExported
        {
            set { this.m_OResultExported = value;}
        }

        public CRestfulAPI ORestful
        {
            get { return this.m_ORestful; }
        }

        #endregion

        #region CONSTRUCTOR & DESTRUCTOR
        public CInspectionTool()
        {
            try
            {
                this.m_ORestful = new CRestfulAPI();
                this.m_ODisplayer = new CDisplayTool();
                this.m_OInterrupt = new object();

                this.BeginWork();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        ~CInspectionTool()
        {
            try
            {
                this.Dispose();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion

        #region FUNCTION
        private void BeginWork()
        {
            try
            {
                if (this.m_OWorker == null)
                {
                    this.m_BDoWork = true;

                    this.m_OWorker = new Thread(this.Work);
                    this.m_OWorker.Priority = ThreadPriority.Highest;
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
                        Monitor.Enter(this.m_OInterrupt);

                        if (this.m_BInspectMark == true)
                        {
                            CImageInfo OImageInfo = this.m_ODisplayer.GetImageInfoToAnalysis();

                            if (OImageInfo != null)
                            {
                              //  this.m_BInspectMark = false;
                                this.m_OResult = this.DoInspectMark(OImageInfo);
                                this.Inspected(this.m_OResult);
                                //if (this.m_BSave) this.SaveToFile(this.m_OResult);
                                OImageInfo.Dispose();
                               
                            }

                        }
                       
                        GC.Collect();
                    }
                    catch (Exception ex)
                    {
                        CError.Ignore(ex);
                    }
                    finally
                    {
                        Monitor.Exit(this.m_OInterrupt);
                    }

                    Thread.Sleep(100);
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


        public void RequestMark(bool BChecked)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                this.m_BInspectMark = BChecked;
                this.m_OResult = null;
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


        private CResult DoInspectMark(CImageInfo OImageInfo)
        {
            CResult OResult = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);
                CImageInfo ImageCopy = new CImageInfo((Bitmap)OImageInfo.OImage.Clone());
                var OResponse = Task.Run(async () => await (this.m_ORestful.PostImage(ImageCopy.OImage)));
                HttpHelper.Result OContents = OResponse.Result;
               
               
                var jsonObject = JsonConvert.DeserializeObject<dynamic>(OContents.Content);
                if (jsonObject != null)
                {
                    OResult = new CResult(ImageCopy);
                    OResult.BInspected = true;
                    // accessing the values in the JSON object
                    var JSON_code = jsonObject.code; // returns 200
                    var JSON_status = jsonObject.status; // returns "OK"
                    var JSON_result = jsonObject.result; // returns "empty data"
                    var JSON_description = jsonObject.description; // returns "Successful response"

                    if (JSON_description == "Successful response") 
                        OResult.BInspected = true;
                    else 
                        OResult.BInspected = false;

                    JArray scoresArray = (JArray)JSON_result.SelectToken("scores");

                    foreach (double F64Score in scoresArray)
                    {
                        OResult.F64S = F64Score * 100;
                    }

                    JArray predictedArray = (JArray)JSON_result.SelectToken("predicted");
                    foreach (string StrOK in predictedArray)
                    {
                        if (StrOK == "OK")
                            OResult.BOK = true;
                        else
                            OResult.BOK = false;
                    }
                   
                }
                //    }
                //}
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


       


        public CResult GetMarkResult()
        {
            CResult OResult = null;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                OResult = this.m_OResult;
                // this.m_OMarkResult = null;
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

        private void Inspected(CResult OResult)
        {
            try
            {
                if (this.m_OResultExported != null)
                {
                    this.m_OResultExported(OResult);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

        }


        public void Dispose()
        {
            try
            {
                this.EndWork();

                if (this.m_ODisplayer != null)
                {
                    this.m_ODisplayer.Dispose();
                    this.m_ODisplayer = null;
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
