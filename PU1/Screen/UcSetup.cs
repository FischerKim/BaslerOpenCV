using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PU1
{
    public partial class UcSetup : UcScreen
    {
        #region VARIABLES
        private bool m_BPreventEvent = false;
        #endregion

        #region CONSTRUCTOR & DESTRUCTOR
        public UcSetup()
        {
            InitializeComponent();

            try {

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

                this.CmbAcquisition.Text = this.GetTriggerMode(CEnvironment.It.StrCameraAcquisitionMode);
                this.CmbFrameMode.Text = this.GetTriggerMode(CEnvironment.It.StrCameraFrameMode);
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        #endregion

        #region EVENT
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
        private void CmbCameraTriggerMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_BPreventEvent == true) return;

            try
            {
                string StrTag = (string)((Control)sender).Tag;

                if (StrTag == "ACQUISITION")
                {
                    if (this.CmbAcquisition.Text == "On")
                    {
                        CAcquisitionManager.It.OHead1.StrTriggerSelector = "AcquisitionStart";
                        CAcquisitionManager.It.OHead1.StrTriggerMode = ETriggerMode.On.ToString();

                        CEnvironment.It.StrCameraAcquisitionMode = ETriggerMode.On.ToString();
                        CEnvironment.It.Commit();
                    }
                    else
                    {
                        CAcquisitionManager.It.OHead1.StrTriggerSelector = "AcquisitionStart";
                        CAcquisitionManager.It.OHead1.StrTriggerMode = ETriggerMode.Off.ToString();

                        CEnvironment.It.StrCameraAcquisitionMode = ETriggerMode.Off.ToString();
                        CEnvironment.It.Commit();
                    }
                }
                else if (StrTag == "FRAME")
                {
                    if (this.CmbFrameMode.Text == "On")
                    {
                        CAcquisitionManager.It.OHead1.StrTriggerSelector = "FrameStart";
                        CAcquisitionManager.It.OHead1.StrTriggerMode = ETriggerMode.On.ToString();

                        CEnvironment.It.StrCameraFrameMode = ETriggerMode.On.ToString();
                        CEnvironment.It.Commit();
                    }
                    else
                    {
                        CAcquisitionManager.It.OHead1.StrTriggerSelector = "FrameStart";
                        CAcquisitionManager.It.OHead1.StrTriggerMode = ETriggerMode.Off.ToString();

                        CEnvironment.It.StrCameraFrameMode = ETriggerMode.Off.ToString();
                        CEnvironment.It.Commit();
                    }
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
                this.m_BPreventEvent = true;


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
            finally
            {
                this.m_BPreventEvent = false;
            }
        }
        #endregion

        #region FUNCTION
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
        #endregion
    }

    public enum ETriggerMode : byte
    {
        On = 0x00,
        Off
    }
}
