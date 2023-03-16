namespace PU1
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PnlTop = new System.Windows.Forms.Panel();
            this.BtnMinimize = new System.Windows.Forms.Button();
            this.LblTime1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PnlMid = new System.Windows.Forms.Panel();
            this.PnlBottom = new System.Windows.Forms.Panel();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnReport = new System.Windows.Forms.Button();
            this.BtnRecipe = new System.Windows.Forms.Button();
            this.BtnSetup = new System.Windows.Forms.Button();
            this.BtnHome = new System.Windows.Forms.Button();
            this.Timer1000 = new System.Windows.Forms.Timer(this.components);
            this.PnlTop.SuspendLayout();
            this.PnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlTop
            // 
            this.PnlTop.Controls.Add(this.BtnMinimize);
            this.PnlTop.Controls.Add(this.LblTime1);
            this.PnlTop.Controls.Add(this.label1);
            this.PnlTop.Controls.Add(this.label4);
            this.PnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlTop.Location = new System.Drawing.Point(0, 0);
            this.PnlTop.Name = "PnlTop";
            this.PnlTop.Size = new System.Drawing.Size(1284, 40);
            this.PnlTop.TabIndex = 0;
            // 
            // BtnMinimize
            // 
            this.BtnMinimize.BackColor = System.Drawing.Color.RoyalBlue;
            this.BtnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMinimize.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnMinimize.ForeColor = System.Drawing.Color.White;
            this.BtnMinimize.Location = new System.Drawing.Point(1245, 0);
            this.BtnMinimize.Name = "BtnMinimize";
            this.BtnMinimize.Size = new System.Drawing.Size(39, 40);
            this.BtnMinimize.TabIndex = 6;
            this.BtnMinimize.Tag = "";
            this.BtnMinimize.Text = "_";
            this.BtnMinimize.UseVisualStyleBackColor = false;
            this.BtnMinimize.Click += new System.EventHandler(this.BtnMinimize_Click);
            // 
            // LblTime1
            // 
            this.LblTime1.BackColor = System.Drawing.Color.IndianRed;
            this.LblTime1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTime1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblTime1.ForeColor = System.Drawing.Color.White;
            this.LblTime1.Location = new System.Drawing.Point(1015, 0);
            this.LblTime1.Name = "LblTime1";
            this.LblTime1.Size = new System.Drawing.Size(230, 40);
            this.LblTime1.TabIndex = 9;
            this.LblTime1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DimGray;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(141, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(874, 40);
            this.label1.TabIndex = 8;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Navy;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 40);
            this.label4.TabIndex = 7;
            this.label4.Text = "Advantech";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PnlMid
            // 
            this.PnlMid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.PnlMid.Location = new System.Drawing.Point(0, 40);
            this.PnlMid.Name = "PnlMid";
            this.PnlMid.Size = new System.Drawing.Size(1284, 680);
            this.PnlMid.TabIndex = 2;
            // 
            // PnlBottom
            // 
            this.PnlBottom.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PnlBottom.Controls.Add(this.BtnExit);
            this.PnlBottom.Controls.Add(this.BtnReport);
            this.PnlBottom.Controls.Add(this.BtnRecipe);
            this.PnlBottom.Controls.Add(this.BtnSetup);
            this.PnlBottom.Controls.Add(this.BtnHome);
            this.PnlBottom.Location = new System.Drawing.Point(0, 721);
            this.PnlBottom.Name = "PnlBottom";
            this.PnlBottom.Size = new System.Drawing.Size(1284, 40);
            this.PnlBottom.TabIndex = 3;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.RoyalBlue;
            this.BtnExit.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnExit.ForeColor = System.Drawing.Color.White;
            this.BtnExit.Location = new System.Drawing.Point(1161, 3);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(120, 35);
            this.BtnExit.TabIndex = 15;
            this.BtnExit.Tag = "HEAD1";
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnReport
            // 
            this.BtnReport.BackColor = System.Drawing.Color.RoyalBlue;
            this.BtnReport.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnReport.ForeColor = System.Drawing.Color.White;
            this.BtnReport.Location = new System.Drawing.Point(369, 3);
            this.BtnReport.Name = "BtnReport";
            this.BtnReport.Size = new System.Drawing.Size(120, 35);
            this.BtnReport.TabIndex = 14;
            this.BtnReport.Tag = "REPORT";
            this.BtnReport.Text = "Report";
            this.BtnReport.UseVisualStyleBackColor = false;
            this.BtnReport.Click += new System.EventHandler(this.BtnScreen_Click);
            // 
            // BtnRecipe
            // 
            this.BtnRecipe.BackColor = System.Drawing.Color.RoyalBlue;
            this.BtnRecipe.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnRecipe.ForeColor = System.Drawing.Color.White;
            this.BtnRecipe.Location = new System.Drawing.Point(247, 3);
            this.BtnRecipe.Name = "BtnRecipe";
            this.BtnRecipe.Size = new System.Drawing.Size(120, 35);
            this.BtnRecipe.TabIndex = 13;
            this.BtnRecipe.Tag = "RECIPE";
            this.BtnRecipe.Text = "Recipe";
            this.BtnRecipe.UseVisualStyleBackColor = false;
            this.BtnRecipe.Click += new System.EventHandler(this.BtnScreen_Click);
            // 
            // BtnSetup
            // 
            this.BtnSetup.BackColor = System.Drawing.Color.RoyalBlue;
            this.BtnSetup.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnSetup.ForeColor = System.Drawing.Color.White;
            this.BtnSetup.Location = new System.Drawing.Point(125, 3);
            this.BtnSetup.Name = "BtnSetup";
            this.BtnSetup.Size = new System.Drawing.Size(120, 35);
            this.BtnSetup.TabIndex = 12;
            this.BtnSetup.Tag = "SETUP";
            this.BtnSetup.Text = "Setup";
            this.BtnSetup.UseVisualStyleBackColor = false;
            this.BtnSetup.Click += new System.EventHandler(this.BtnScreen_Click);
            // 
            // BtnHome
            // 
            this.BtnHome.BackColor = System.Drawing.Color.RoyalBlue;
            this.BtnHome.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnHome.ForeColor = System.Drawing.Color.White;
            this.BtnHome.Location = new System.Drawing.Point(3, 3);
            this.BtnHome.Name = "BtnHome";
            this.BtnHome.Size = new System.Drawing.Size(120, 35);
            this.BtnHome.TabIndex = 11;
            this.BtnHome.Tag = "HOME";
            this.BtnHome.Text = "Home";
            this.BtnHome.UseVisualStyleBackColor = false;
            this.BtnHome.Click += new System.EventHandler(this.BtnScreen_Click);
            // 
            // Timer1000
            // 
            this.Timer1000.Enabled = true;
            this.Timer1000.Interval = 1000;
            this.Timer1000.Tick += new System.EventHandler(this.Timer1000_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 761);
            this.Controls.Add(this.PnlBottom);
            this.Controls.Add(this.PnlMid);
            this.Controls.Add(this.PnlTop);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.PnlTop.ResumeLayout(false);
            this.PnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel PnlTop;
        private Panel PnlMid;
        private Panel PnlBottom;
        private Label LblTime1;
        private Label label1;
        private Label label4;
        private Button BtnMinimize;
        private System.Windows.Forms.Timer Timer1000;
        private Button BtnReport;
        private Button BtnRecipe;
        private Button BtnSetup;
        private Button BtnHome;
        private Button BtnExit;
    }
}