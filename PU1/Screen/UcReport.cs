using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PU1
{
    public partial class UcReport : UcScreen
    {
        #region VARIABLE
        private bool m_BPreventEvent = false;
        #endregion

        #region CONSTRUCTOR & DESTRUCTOR
        public UcReport()
        {
            InitializeComponent();
            this.DgvReport.AutoGenerateColumns = false;
        }
        #endregion

        #region EVENT
        #region BUTTON EVENT
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable ODataSource = null;
                if (this.DgvReport.DataSource != null)
                {
                    ODataSource = (DataTable)this.DgvReport.DataSource;
                }

                IDynamicTable OTable = CDB.It.GetDynamicTable(CDB.TABLE_REPORT);
                this.DgvReport.DataSource = OTable.Select(this.dateTimePicker1.Value, true);

                if (ODataSource != null)
                {
                    ODataSource.Dispose();
                    ODataSource = null;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        #endregion

        #endregion

        private void DgvReport_SelectionChanged(object sender, EventArgs e)
        {
            if (this.m_BPreventEvent == true) return;

            try
            {
                if(this.PnlDisplay1.Image != null) this.PnlDisplay1.Image.Dispose();
                if (this.DgvReport.CurrentRow.DataBoundItem != null)
                {
                    DataRow ORow = ((DataRowView)this.DgvReport.CurrentRow.DataBoundItem).Row;
                    this.LblOK.Text = ORow[CDB.REPORT_HEAD1_RESULT].ToString();
                    this.LblScore.Text = ORow[CDB.REPORT_HEAD1_SCORE].ToString();
                    this.LblX.Text = ORow[CDB.REPORT_HEAD1_PIXEL_X].ToString();
                    this.LblY.Text = ORow[CDB.REPORT_HEAD1_PIXEL_Y].ToString();
                    this.PnlDisplay1.Image = this.GetImage(ORow[CDB.REPORT_HEAD1_FILE].ToString());
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }


        private Bitmap GetImage(string StrFile)
        {
            Bitmap OResult = null;
            try
            {
                if (String.IsNullOrEmpty(StrFile) == false)
                {
                    if (File.Exists(StrFile) == true)
                    {
                        OResult = new Bitmap(StrFile);
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

            return OResult;
        }

    }
}
