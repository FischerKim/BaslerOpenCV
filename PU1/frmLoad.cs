using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using PylonC.NET;

namespace PU1
{
    public partial class frmLoad : Form
    {
        #region VARIABLES
        private CCameraInfo m_OHead1 = null;
        #endregion

        #region CONSTRUCTOR & DESTRUCTOR
        public frmLoad()
        {
            InitializeComponent();
        }
        #endregion

        #region EVENT
        #region FORM EVENT
        private void frmLoad_Load(object sender, EventArgs e)
        {
            try
            {
                CImageSaveTool.USE_DELETE = true;
                CImageSaveTool.DELETE_TIME_UNIT = ETIME_UNIT.DAY;
                CImageSaveTool.IMAGE_MANAGE_DIR = new List<string>();
                CImageSaveTool.IMAGE_MANAGE_DIR.Add(".\\Image\\{0:yyyy}\\{0:MM}\\{0:dd}");
                CImageSaveTool.IMAGE_MANAGE_PERIOD = 7;
                CImageSaveTool.It.Load();

                if (File.Exists(".\\System.ini") == false)
                {
                    File.WriteAllText(".\\System.ini", PU1.Properties.Resources.System);
                }

                StringBuilder OHead1 = new StringBuilder();
                frmLoad.GetPrivateProfileString("HEAD1", "IP", string.Empty, OHead1, 255, ".\\System.ini");


                uint U32Count = Pylon.EnumerateDevices();
                if (U32Count != 0)
                {
                    string StrIP = string.Empty;

                    for (uint _Index = 0; _Index < U32Count; _Index++)
                    {
                        PYLON_DEVICE_INFO_HANDLE OHandle = Pylon.GetDeviceInfoHandle(_Index);
                        StrIP = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "IpAddress");

                        if (StrIP == OHead1.ToString())
                        {
                            this.m_OHead1 = new CCameraInfo();
                            this.m_OHead1.StrVender = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "VendorName");
                            this.m_OHead1.StrModel = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "ModelName");
                            this.m_OHead1.StrSerial = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "SerialNumber");
                            this.m_OHead1.StrIP = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "IpAddress");
                            this.m_OHead1.OKey = _Index;
                        }
                    }
                }

                if (this.m_OHead1 != null) this.LblHead1.Text = this.m_OHead1.StrIP;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region BUTTON EVENT
        private void BtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                    frmCameraSelector OWindow2 = new frmCameraSelector();
                    OWindow2.OHead1 = this.m_OHead1;

                    if (OWindow2.ShowDialog() == DialogResult.OK)
                    {
                        this.m_OHead1 = OWindow2.OHead1;

                        if (this.m_OHead1 != null) this.LblHead1.Text = this.m_OHead1.StrIP;
                    }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void BtnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.m_OHead1 == null)
                {
                    MessageBox.Show("Please select a camera before continuing.");
                    return;
                }

                if (this.m_OHead1 != null)
                {
                    frmLoad.WritePrivateProfileString("HEAD1", "Company", this.m_OHead1.StrVender, ".\\System.ini");
                    frmLoad.WritePrivateProfileString("HEAD1", "Product", this.m_OHead1.StrModel, ".\\System.ini");
                    frmLoad.WritePrivateProfileString("HEAD1", "Serial", this.m_OHead1.StrSerial, ".\\System.ini");
                    frmLoad.WritePrivateProfileString("HEAD1", "IP", this.m_OHead1.StrIP, ".\\System.ini");
                }
              
                CDB.It.Load();

               // CRecipeManager.It.Load();

                CAcquisitionManager.It.OHead1 = new CBasler(this.m_OHead1);
                CAcquisitionManager.It.Setup();

                this.BtnEnter.BackColor = SystemColors.Control;
                this.BtnExit.BackColor = SystemColors.Control;
                this.BtnEnter.Enabled = false;
                this.BtnExit.Enabled = false;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
        #endregion


        #region FUNCTION
        #region EXTERNAL FUNCTION
        [DllImport("kernel32")]
        public static extern bool GetPrivateProfileString(string StrAppName, string StrKey, string StrDefault, StringBuilder StrValue, int I32Size, string StrFile);


        [DllImport("kernel32")]
        public static extern bool WritePrivateProfileString(string StrAppName, string StrKey, string StrValue, string StrFile);
        #endregion

        #endregion

    }
}
