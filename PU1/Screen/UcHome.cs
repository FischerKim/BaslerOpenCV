using PostMultipart;
using PU1;
using PU1.Common.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PostMultipart.HttpHelper;

namespace PU1
{
    public partial class UcHome : UcScreen
    {

        #region VARIABLE
        private bool m_BPreventEvent = false;
        int I32FramePerSec = 0;
        int I32InspectionCountPerSec = 0;
        int I32Count = 0;
        #endregion

        #region CONSTRUCTOR & DESTRUCTOR
        public UcHome()
        {
            InitializeComponent();
            try
            {
                this.m_BPreventEvent = true;
                this.BtnStart.BackColor = Color.RoyalBlue;
                this.BtnStart.Enabled = true;
                this.BtnStop.BackColor = Color.SteelBlue;
                this.BtnStop.Enabled = false;

                this.TrkCameraGain.Minimum = CAcquisitionManager.It.OHead1.I32GainMin;
                this.TrkCameraGain.Maximum = CAcquisitionManager.It.OHead1.I32GainMax;
                this.NudCameraGain.Minimum = this.TrkCameraGain.Minimum;
                this.NudCameraGain.Maximum = this.TrkCameraGain.Maximum;

                this.TrkCameraGain.Value = CAcquisitionManager.It.OHead1.I32Gain;
                this.NudCameraGain.Value = this.TrkCameraGain.Value;

                this.TrkCameraExposureTime.Minimum = CAcquisitionManager.It.OHead1.I32ExposureTimeMin;
                this.TrkCameraExposureTime.Maximum = CAcquisitionManager.It.OHead1.I32ExposureTimeMax;
                this.NudCameraExposureTime.Minimum = this.TrkCameraExposureTime.Minimum;
                this.NudCameraExposureTime.Maximum = this.TrkCameraExposureTime.Maximum;
                this.TrkCameraExposureTime.Value = CAcquisitionManager.It.OHead1.I32ExposureTime;
                this.NudCameraExposureTime.Value = this.TrkCameraExposureTime.Value;

                this.ConnectToInferenceServer();

                this.Timer1000.Start();

            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally 
            {
                this.m_BPreventEvent = false;
            }
        }
        #endregion
        #region EVENT
        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (e == EventArgs.Empty) return;

            try
            {
                this.m_BPreventEvent = true;
                //apply recipe 
                CAcquisitionManager.It.OHead1.Start();
                CTool.It.OTool.ODisplayer.BClean = false;
                //CTool.It.
                this.BtnStart.BackColor = Color.SteelBlue;
                this.BtnStart.Enabled = false;
                this.BtnStop.BackColor = Color.RoyalBlue;
                this.BtnStop.Enabled = true;

                base.OnScreenFixed(true);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                this.m_BPreventEvent = false;
            }

        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (e == EventArgs.Empty) return;

            try
            {
                this.m_BPreventEvent = true;
                //apply recipe 
                CAcquisitionManager.It.OHead1.Stop();
                CTool.It.OTool.ODisplayer.BClean = true;
                //CTool.It.
                this.LblOK.BackColor = Color.White;
                this.BtnStart.BackColor = Color.RoyalBlue;
                this.BtnStart.Enabled = true;
                this.BtnStop.BackColor = Color.SteelBlue;
                this.BtnStop.Enabled = false;
                this.InspectSwitch.Checked = false;
                this.ChkSave.Checked = false;
                base.OnScreenFixed(false);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                this.m_BPreventEvent = false;
            }
        }


        private void OHead1_OImageExported(Bitmap OImage)
        {
            try
            {
                lock (OImage)
                {
                    this.I32FramePerSec++;
                    this.PnlDisplay1.Invoke(new Action(() =>
                    {

                        this.PnlDisplay1.Image = (Bitmap)OImage.Clone();//(Bitmap)OImageInfo.OImage.Clone();
                        
                    }));
                    //OImage.Dispose();
                    this.I32Count++;
                    if (I32Count >= 4)
                    {
                        I32Count = 0;
                        GC.Collect();
                    }

                    
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }

        private void Inspected(CResult OResult)
        {
            try
            {
                if (base.InvokeRequired == true)
                {
                    base.BeginInvoke(new CInspectionTool.ResultExportedHandler(this.Inspected), OResult);
                }
                else
                {
                    lock (OResult)
                    {
                        this.I32InspectionCountPerSec++;
                        this.LblOK.Text = OResult.BOK.ToString();
                        if (OResult.BOK == true)
                        {
                            this.LblOK.BackColor = Color.ForestGreen;
                        }
                        else
                        {
                            this.LblOK.BackColor = Color.DarkRed;
                        }


                        this.LblScore.Text = OResult.F64S.ToString();
                        this.LblX.Text = OResult.F64X.ToString();
                        this.LblY.Text = OResult.F64Y.ToString();
                        if (this.ChkSave.Checked) this.SaveToFile(OResult);
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        public override void Add()
        {
            try
            {
                CTool.It.OTool.ODisplayer.OImageExported = this.OHead1_OImageExported;
                CTool.It.OTool.OResultExported = this.Inspected;
            
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        public override void Remove()
        {
            try
            {
                CTool.It.OTool.ODisplayer.OImageExported = null;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        private void TrkCameraOption_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_BPreventEvent == true) return;

            try
            {
                string StrTag = (string)((Control)sender).Tag;
                int I32Value = Convert.ToInt32(((TrackBar)sender).Value);

                if (StrTag == "GAIN")
                {
                    CAcquisitionManager.It.OHead1.I32Gain = I32Value;
                    this.NudCameraGain.Value = I32Value;

                    CEnvironment.It.I32CamHead1Gain = I32Value;
                    CEnvironment.It.Commit();
                }
                else if (StrTag == "EXPOSURE TIME")
                {
                    CAcquisitionManager.It.OHead1.I32ExposureTime = I32Value;
                    this.NudCameraExposureTime.Value = I32Value;

                    CEnvironment.It.I32CamHead1ExpoTime = I32Value;
                    CEnvironment.It.Commit();
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
      

        private void NumericValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.m_BPreventEvent == true) return;


                string StrTag = (string)((Control)sender).Tag;
                int I32Value = 0;

               if (StrTag == "GAIN")
                {
                    I32Value = Convert.ToInt32(this.NudCameraGain.Value);

                    CAcquisitionManager.It.OHead1.I32Gain = I32Value;
                    this.TrkCameraGain.Value = I32Value;

                    CEnvironment.It.I32CamHead1Gain = I32Value;
                    CEnvironment.It.Commit();
                }
                else if (StrTag == "EXPOSURE TIME")
                {
                    I32Value = Convert.ToInt32(this.NudCameraExposureTime.Value);

                    CAcquisitionManager.It.OHead1.I32ExposureTime = I32Value;
                    this.TrkCameraExposureTime.Value = I32Value;

                    CEnvironment.It.I32CamHead1ExpoTime = I32Value;
                    CEnvironment.It.Commit();
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        private void InspectSwitch_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CTool.It.OTool.RequestMark(this.InspectSwitch.Checked);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        private void ChkSave_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CTool.It.OTool.BSave = this.ChkSave.Checked;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        private void Timer1000_Tick(object sender, EventArgs e)
        {
            try
            {
                this.LblFramePersec.Text = this.I32FramePerSec.ToString();
                this.LblInspectPerSec.Text = this.I32InspectionCountPerSec.ToString();
                this.I32FramePerSec = 0;
                this.I32InspectionCountPerSec = 0;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion

        #region FUNCTION
        private async void BtnRebootMIC_Click(object sender, EventArgs e)
        {
            try
            {
                this.LblLicense.BackColor = Color.DarkRed;
                this.LblLicense.Text = "Rebooting";
                var response = await CTool.It.OTool.ORestful.Reboot();
                
                this.ConnectToInferenceServer();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }

        private async void ConnectToInferenceServer()
        {
            try 
            { 
                var response = await CTool.It.OTool.ORestful.GetLicenseInfo();
                this.LblLicense.BackColor = Color.Green;
                this.LblLicense.Text = response.Content.ToString();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }

        private string GetTriggerMode(string StrTriggerMode)
        {
            string StrResult = string.Empty;

            try
            {
                if (StrTriggerMode == ETriggerMode.On.ToString()) StrResult = "On";
                else StrResult = "Off";
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            return StrResult;
        }

        private void SaveToFile(CResult OResult)
        {
            try
            {
                DateTime OTime = DateTime.Now;
                string StrHead1 = string.Empty;

                if (OResult.OImageInfo != null)
                {
                    //Bitmap OSaveImage = (Bitmap)OResult.OImageInfo.OImage.Clone();
                    CImageSaveFile OHead1File = new CImageSaveFile(OTime,(Bitmap)OResult.OImageInfo.OImage.Clone());
                    //OHead1File.StrDirectory = ".\\Image\\{0:yyyy}\\{0:MM}\\{0:dd}\\{0:HH.mm.ss fff}";
                    OHead1File.StrDirectory = ".\\Image\\{0:yyyy}\\{0:MM}\\{0:dd}";
                    OHead1File.StrFile = "{0:HH.mm.ss fff}.jpg";
                    OHead1File.OFormat = ImageFormat.Jpeg;

                    StrHead1 = OHead1File.GetFile();
                    CImageSaveTool.It.Set(OHead1File);
                }

                IDynamicTable OTable = CDB.It.GetDynamicTable(CDB.TABLE_REPORT);
                if (OTable == null) return;
                try
                {
                    OTable.BeginSyncData();

                    int I32RowIndex = OTable.InsertRow();
                    OTable.Update(I32RowIndex, CDB.REPORT_DATETIME, OTime.ToString("yyyy.MM.dd HH:mm:ss fff"));
                    //OTable.Update(I32RowIndex, CDB.REPORT_RECIPE, this.m_ORecipe.StrName);

                    OTable.Update(I32RowIndex, CDB.REPORT_HEAD1_RESULT, OResult.BOK);
                    OTable.Update(I32RowIndex, CDB.REPORT_HEAD1_FILE, StrHead1);

                    //if ((OResult.BInspected == true) && (OResult.BOK == true))
                    //{
                        OTable.Update(I32RowIndex, CDB.REPORT_HEAD1_PIXEL_X, OResult.F64X);
                        OTable.Update(I32RowIndex, CDB.REPORT_HEAD1_PIXEL_Y, OResult.F64Y);
                        OTable.Update(I32RowIndex, CDB.REPORT_HEAD1_SCORE, OResult.F64S);
                    //}
                }
                catch (Exception ex)
                {
                    CError.Throw(ex);
                }
                finally
                {
                    OTable.EndSyncData();
                    OResult.Dispose();
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
