using PU1;
using System.Windows.Forms;

namespace PU1
{
    public partial class frmMain : Form
    {
        #region VARIABLES
        private UcScreen m_OScreen = null;
        private UcHome m_OHome = null;
        private UcSetup m_OSetup = null;
        private UcRecipe m_ORecipe = null;
        private UcReport m_OReport = null;
        #endregion

        #region CONSTRUCTOR & DESTRUCTOR
        public frmMain()
        {
            InitializeComponent();
            this.ReadyCamera();
            //this.ReadyRecipe();
            //this.ReadyScaleInfo();
        }
        #endregion

        #region EVENT
        #region FORM EVENT
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                this.m_OHome = new UcHome();
                this.m_OSetup = new UcSetup();
                this.m_ORecipe = new UcRecipe();
                this.m_OReport = new UcReport();
                this.m_OHome.Dock = DockStyle.Fill;
                this.m_OSetup.Dock = DockStyle.Fill;
                this.m_ORecipe.Dock = DockStyle.Fill;
                this.m_OReport.Dock = DockStyle.Fill;
                this.m_OHome.ScreenFixed += new UcScreen.ScreenFixedHandler(this.OScreen_ScreenFixed);
                this.m_OSetup.ScreenFixed += new UcScreen.ScreenFixedHandler(this.OScreen_ScreenFixed);
                this.m_ORecipe.ScreenFixed += new UcScreen.ScreenFixedHandler(this.OScreen_ScreenFixed);
                this.m_OReport.ScreenFixed += new UcScreen.ScreenFixedHandler(this.OScreen_ScreenFixed);
                this.SetScreen(this.m_OHome);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //if (CAcquisitionManager.It.OHead1 != null) CAcquisitionManager.It.OHead1.Stop();
                //if (CAcquisitionManager.It.OHead2 != null) CAcquisitionManager.It.OHead2.Stop();
                //if (CAcquisitionManager.It.OHead3 != null) CAcquisitionManager.It.OHead3.Stop();
                //if (CAcquisitionManager.It.OHead4 != null) CAcquisitionManager.It.OHead4.Stop();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
        #region BUTTON EVENT
        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        private void BtnScreen_Click(object sender, EventArgs e)
        {
            try
            {
                //base.Size = new Size(2560, 1024);

                switch ((string)((Control)sender).Tag)
                {
                    case "HOME":
                        this.SetScreen(this.m_OHome);
                        break;

                    case "SETUP":
                        this.SetScreen(this.m_OSetup);
                        break;

                    case "RECIPE":
                        this.SetScreen(this.m_ORecipe);
                        break;

                    case "REPORT":
                        this.SetScreen(this.m_OReport);
                        break;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
        #region ETC EVENT
        private void Timer1000_Tick(object sender, EventArgs e)
        {
            this.LblTime1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        private void OScreen_ScreenFixed(bool BFixed)
        {
            try
            {
                this.BtnHome.BackColor = (BFixed == true) ? SystemColors.Control : Color.SteelBlue;
                this.BtnSetup.BackColor = (BFixed == true) ? SystemColors.Control : Color.SteelBlue;
                this.BtnRecipe.BackColor = (BFixed == true) ? SystemColors.Control : Color.SteelBlue;
                this.BtnReport.BackColor = (BFixed == true) ? SystemColors.Control : Color.SteelBlue;
                this.BtnExit.BackColor = (BFixed == true) ? SystemColors.Control : Color.SteelBlue;
                this.BtnHome.Enabled = !BFixed;
                this.BtnSetup.Enabled = !BFixed;
                this.BtnRecipe.Enabled = !BFixed;
                this.BtnReport.Enabled = !BFixed;
                this.BtnExit.Enabled = !BFixed;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion

        #endregion

        #region FUNCTION
        private void ReadyCamera()
        {
            try 
            {
                CTool.It.OTool.ODisplayer.OView = CAcquisitionManager.It.OHead1;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
                
        private void SetScreen(UcScreen OScreen)
        {
            try
            {
                if (this.m_OScreen == null)
                {
                    OScreen.Add();
                    this.PnlMid.Controls.Add(OScreen);
                    this.m_OScreen = OScreen;
                }
                else
                {
                    if (this.m_OScreen.GetType() != OScreen.GetType())
                    {
                        this.m_OScreen.Remove();
                        OScreen.Add();

                        this.PnlMid.Controls.Add(OScreen);
                        OScreen.BringToFront();
                        this.PnlMid.Controls.Remove(this.m_OScreen);
                        this.PnlMid.Dock =DockStyle.Fill;
                        this.PnlMid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
                        this.m_OScreen = OScreen;
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion

        private void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
    }
}