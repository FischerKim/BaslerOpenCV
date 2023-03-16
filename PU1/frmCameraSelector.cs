using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using PylonC.NET;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PU1
{
    public partial class frmCameraSelector : Form
    {
        #region VARIABLE
        private CCameraInfo m_OHead1;
        #endregion

        #region PROPERTIES
        public CCameraInfo OHead1
        {
            get { return this.m_OHead1; }
            set { this.m_OHead1 = value; }
        }
        #endregion

        #region CONSTRUCTOR & DESTRUCTOR
        public frmCameraSelector()
        {
            InitializeComponent();
        }
        #endregion

        #region EVENT
        private void frmCameraSelector_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable ODataSource = new DataTable();
                ODataSource.Columns.Add("COMPANY", typeof(string));
                ODataSource.Columns.Add("MODEL", typeof(string));
                ODataSource.Columns.Add("IP", typeof(string));
                ODataSource.Columns.Add("SERIAL", typeof(string));
                ODataSource.Columns.Add("KEY", typeof(uint));

                uint U32Count = Pylon.EnumerateDevices();
                for (uint _Index = 0; _Index < U32Count; _Index++)
                {
                    PYLON_DEVICE_INFO_HANDLE OHandle = Pylon.GetDeviceInfoHandle(_Index);

                    DataRow ORow = ODataSource.NewRow();
                    ORow["COMPANY"] = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "VendorName");
                    ORow["MODEL"] = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "ModelName");
                    ORow["IP"] = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "IpAddress");
                    ORow["SERIAL"] = Pylon.DeviceInfoGetPropertyValueByName(OHandle, "SerialNumber");
                    ORow["KEY"] = _Index;
                    ODataSource.Rows.Add(ORow);
                }

                this.DgvList.DataSource = ODataSource;


                if (this.m_OHead1 != null) this.LblHead1.Text = this.m_OHead1.StrIP;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        private void BtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.DgvList.CurrentRow == null) return;

                string StrKind = (string)((Button)sender).Tag;
                string StrIP = (string)(((DataRowView)(this.DgvList.CurrentRow.DataBoundItem)).Row)["IP"];

                switch (StrKind)
                {
                    case "HEAD1":
                        this.LblHead1.Text = StrIP;
                        break;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(this.LblHead1.Text) == true) this.m_OHead1 = null;


                DataTable ODataSource = (DataTable)this.DgvList.DataSource;

                foreach (DataRow _Item in ODataSource.Rows)
                {
                    if ((string)_Item["IP"] == this.LblHead1.Text)
                    {
                        if (this.m_OHead1 == null) this.m_OHead1 = new CCameraInfo();
                        this.m_OHead1.StrVender = (string)_Item["COMPANY"];
                        this.m_OHead1.StrModel = (string)_Item["MODEL"];
                        this.m_OHead1.StrIP = (string)_Item["IP"];
                        this.m_OHead1.StrSerial = (string)_Item["SERIAL"];
                        this.m_OHead1.OKey = (uint)_Item["KEY"];
                    }
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }


        private void BtnCancel_Click(object sender, EventArgs e)
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
    }
}
