namespace PU1
{
    partial class UcHome
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PnlDisplay1 = new System.Windows.Forms.PictureBox();
            this.BtnStop = new System.Windows.Forms.Button();
            this.BtnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.NudCameraExposureTime = new System.Windows.Forms.NumericUpDown();
            this.NudCameraGain = new System.Windows.Forms.NumericUpDown();
            this.TrkCameraExposureTime = new System.Windows.Forms.TrackBar();
            this.TrkCameraGain = new System.Windows.Forms.TrackBar();
            this.LblResult = new System.Windows.Forms.Label();
            this.LblOK = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LblX = new System.Windows.Forms.Label();
            this.LblY = new System.Windows.Forms.Label();
            this.InspectSwitch = new System.Windows.Forms.CheckBox();
            this.Timer1000 = new System.Windows.Forms.Timer(this.components);
            this.LblInspectPerSec = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.LblFramePersec = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.LblLicense = new System.Windows.Forms.Label();
            this.ChkSave = new System.Windows.Forms.CheckBox();
            this.lbl5 = new System.Windows.Forms.Label();
            this.LblScore = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PnlDisplay1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudCameraExposureTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudCameraGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrkCameraExposureTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrkCameraGain)).BeginInit();
            this.SuspendLayout();
            // 
            // PnlDisplay1
            // 
            this.PnlDisplay1.BackColor = System.Drawing.Color.MediumBlue;
            this.PnlDisplay1.Location = new System.Drawing.Point(0, 40);
            this.PnlDisplay1.Name = "PnlDisplay1";
            this.PnlDisplay1.Size = new System.Drawing.Size(658, 474);
            this.PnlDisplay1.TabIndex = 0;
            this.PnlDisplay1.TabStop = false;
            // 
            // BtnStop
            // 
            this.BtnStop.BackColor = System.Drawing.Color.RoyalBlue;
            this.BtnStop.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnStop.ForeColor = System.Drawing.Color.White;
            this.BtnStop.Location = new System.Drawing.Point(538, 520);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(120, 35);
            this.BtnStop.TabIndex = 13;
            this.BtnStop.Tag = "";
            this.BtnStop.Text = "Stop";
            this.BtnStop.UseVisualStyleBackColor = false;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // BtnStart
            // 
            this.BtnStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnStart.BackColor = System.Drawing.Color.RoyalBlue;
            this.BtnStart.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnStart.ForeColor = System.Drawing.Color.White;
            this.BtnStart.Location = new System.Drawing.Point(416, 520);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(120, 35);
            this.BtnStart.TabIndex = 12;
            this.BtnStart.Tag = "";
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = false;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DimGray;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(658, 40);
            this.label1.TabIndex = 14;
            this.label1.Text = "Camera";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DimGray;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(659, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 40);
            this.label2.TabIndex = 26;
            this.label2.Text = "Exposure Time";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.DimGray;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(659, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 40);
            this.label5.TabIndex = 25;
            this.label5.Text = "Gain";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NudCameraExposureTime
            // 
            this.NudCameraExposureTime.Location = new System.Drawing.Point(1158, 60);
            this.NudCameraExposureTime.Name = "NudCameraExposureTime";
            this.NudCameraExposureTime.Size = new System.Drawing.Size(120, 27);
            this.NudCameraExposureTime.TabIndex = 23;
            this.NudCameraExposureTime.Tag = "EXPOSURE TIME";
            this.NudCameraExposureTime.ValueChanged += new System.EventHandler(this.NumericValueChanged);
            // 
            // NudCameraGain
            // 
            this.NudCameraGain.Location = new System.Drawing.Point(1158, 8);
            this.NudCameraGain.Name = "NudCameraGain";
            this.NudCameraGain.Size = new System.Drawing.Size(120, 27);
            this.NudCameraGain.TabIndex = 22;
            this.NudCameraGain.Tag = "GAIN";
            this.NudCameraGain.ValueChanged += new System.EventHandler(this.NumericValueChanged);
            // 
            // TrkCameraExposureTime
            // 
            this.TrkCameraExposureTime.Location = new System.Drawing.Point(836, 53);
            this.TrkCameraExposureTime.Maximum = 100000;
            this.TrkCameraExposureTime.Name = "TrkCameraExposureTime";
            this.TrkCameraExposureTime.Size = new System.Drawing.Size(316, 56);
            this.TrkCameraExposureTime.TabIndex = 21;
            this.TrkCameraExposureTime.Tag = "EXPOSURE TIME";
            this.TrkCameraExposureTime.ValueChanged += new System.EventHandler(this.TrkCameraOption_ValueChanged);
            // 
            // TrkCameraGain
            // 
            this.TrkCameraGain.Location = new System.Drawing.Point(836, 1);
            this.TrkCameraGain.Maximum = 1000;
            this.TrkCameraGain.Name = "TrkCameraGain";
            this.TrkCameraGain.Size = new System.Drawing.Size(316, 56);
            this.TrkCameraGain.TabIndex = 20;
            this.TrkCameraGain.Tag = "GAIN";
            this.TrkCameraGain.ValueChanged += new System.EventHandler(this.TrkCameraOption_ValueChanged);
            // 
            // LblResult
            // 
            this.LblResult.BackColor = System.Drawing.Color.Purple;
            this.LblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblResult.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblResult.ForeColor = System.Drawing.Color.White;
            this.LblResult.Location = new System.Drawing.Point(659, 164);
            this.LblResult.Name = "LblResult";
            this.LblResult.Size = new System.Drawing.Size(525, 40);
            this.LblResult.TabIndex = 28;
            this.LblResult.Text = "Result";
            this.LblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblOK
            // 
            this.LblOK.BackColor = System.Drawing.Color.White;
            this.LblOK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblOK.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblOK.ForeColor = System.Drawing.Color.Black;
            this.LblOK.Location = new System.Drawing.Point(759, 205);
            this.LblOK.Name = "LblOK";
            this.LblOK.Size = new System.Drawing.Size(162, 40);
            this.LblOK.TabIndex = 29;
            this.LblOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Purple;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(659, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 40);
            this.label4.TabIndex = 30;
            this.label4.Text = "OK";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Purple;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(922, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 40);
            this.label6.TabIndex = 31;
            this.label6.Text = "X";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Purple;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(922, 245);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 40);
            this.label7.TabIndex = 32;
            this.label7.Text = "Y";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblX
            // 
            this.LblX.BackColor = System.Drawing.Color.White;
            this.LblX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblX.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblX.ForeColor = System.Drawing.Color.Black;
            this.LblX.Location = new System.Drawing.Point(1022, 205);
            this.LblX.Name = "LblX";
            this.LblX.Size = new System.Drawing.Size(162, 40);
            this.LblX.TabIndex = 33;
            this.LblX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblY
            // 
            this.LblY.BackColor = System.Drawing.Color.White;
            this.LblY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblY.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblY.ForeColor = System.Drawing.Color.Black;
            this.LblY.Location = new System.Drawing.Point(1022, 245);
            this.LblY.Name = "LblY";
            this.LblY.Size = new System.Drawing.Size(162, 40);
            this.LblY.TabIndex = 34;
            this.LblY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InspectSwitch
            // 
            this.InspectSwitch.AutoSize = true;
            this.InspectSwitch.BackColor = System.Drawing.Color.Purple;
            this.InspectSwitch.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.InspectSwitch.ForeColor = System.Drawing.Color.White;
            this.InspectSwitch.Location = new System.Drawing.Point(12, 520);
            this.InspectSwitch.Name = "InspectSwitch";
            this.InspectSwitch.Size = new System.Drawing.Size(114, 35);
            this.InspectSwitch.TabIndex = 35;
            this.InspectSwitch.Text = "Inspect";
            this.InspectSwitch.UseVisualStyleBackColor = false;
            this.InspectSwitch.CheckedChanged += new System.EventHandler(this.InspectSwitch_CheckedChanged);
            // 
            // Timer1000
            // 
            this.Timer1000.Interval = 1000;
            this.Timer1000.Tick += new System.EventHandler(this.Timer1000_Tick);
            // 
            // LblInspectPerSec
            // 
            this.LblInspectPerSec.BackColor = System.Drawing.Color.White;
            this.LblInspectPerSec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblInspectPerSec.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblInspectPerSec.ForeColor = System.Drawing.Color.Black;
            this.LblInspectPerSec.Location = new System.Drawing.Point(822, 123);
            this.LblInspectPerSec.Name = "LblInspectPerSec";
            this.LblInspectPerSec.Size = new System.Drawing.Size(162, 40);
            this.LblInspectPerSec.TabIndex = 39;
            this.LblInspectPerSec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.DimGray;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(659, 123);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(162, 40);
            this.label8.TabIndex = 38;
            this.label8.Text = "초당 검사 수";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.DimGray;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(659, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(162, 40);
            this.label9.TabIndex = 37;
            this.label9.Text = "초당 Frame";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblFramePersec
            // 
            this.LblFramePersec.BackColor = System.Drawing.Color.White;
            this.LblFramePersec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblFramePersec.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblFramePersec.ForeColor = System.Drawing.Color.Black;
            this.LblFramePersec.Location = new System.Drawing.Point(822, 82);
            this.LblFramePersec.Name = "LblFramePersec";
            this.LblFramePersec.Size = new System.Drawing.Size(162, 40);
            this.LblFramePersec.TabIndex = 36;
            this.LblFramePersec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Orange;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(659, 287);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(325, 40);
            this.label3.TabIndex = 40;
            this.label3.Text = "Inference Server";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Orange;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(659, 328);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(162, 40);
            this.label10.TabIndex = 42;
            this.label10.Text = "License";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblLicense
            // 
            this.LblLicense.BackColor = System.Drawing.Color.White;
            this.LblLicense.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblLicense.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblLicense.ForeColor = System.Drawing.Color.Black;
            this.LblLicense.Location = new System.Drawing.Point(822, 328);
            this.LblLicense.Name = "LblLicense";
            this.LblLicense.Size = new System.Drawing.Size(162, 40);
            this.LblLicense.TabIndex = 41;
            this.LblLicense.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChkSave
            // 
            this.ChkSave.AutoSize = true;
            this.ChkSave.BackColor = System.Drawing.Color.SeaGreen;
            this.ChkSave.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ChkSave.ForeColor = System.Drawing.Color.White;
            this.ChkSave.Location = new System.Drawing.Point(105, 520);
            this.ChkSave.Name = "ChkSave";
            this.ChkSave.Size = new System.Drawing.Size(87, 35);
            this.ChkSave.TabIndex = 43;
            this.ChkSave.Text = "Save";
            this.ChkSave.UseVisualStyleBackColor = false;
            this.ChkSave.CheckedChanged += new System.EventHandler(this.ChkSave_CheckedChanged);
            // 
            // lbl5
            // 
            this.lbl5.BackColor = System.Drawing.Color.Purple;
            this.lbl5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl5.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbl5.ForeColor = System.Drawing.Color.White;
            this.lbl5.Location = new System.Drawing.Point(659, 246);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(100, 40);
            this.lbl5.TabIndex = 45;
            this.lbl5.Text = "SCORE";
            this.lbl5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblScore
            // 
            this.LblScore.BackColor = System.Drawing.Color.White;
            this.LblScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblScore.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblScore.ForeColor = System.Drawing.Color.Black;
            this.LblScore.Location = new System.Drawing.Point(759, 246);
            this.LblScore.Name = "LblScore";
            this.LblScore.Size = new System.Drawing.Size(162, 40);
            this.LblScore.TabIndex = 44;
            this.LblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UcHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl5);
            this.Controls.Add(this.LblScore);
            this.Controls.Add(this.ChkSave);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.LblLicense);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LblInspectPerSec);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.LblFramePersec);
            this.Controls.Add(this.InspectSwitch);
            this.Controls.Add(this.LblY);
            this.Controls.Add(this.LblX);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LblOK);
            this.Controls.Add(this.LblResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.NudCameraExposureTime);
            this.Controls.Add(this.NudCameraGain);
            this.Controls.Add(this.TrkCameraExposureTime);
            this.Controls.Add(this.TrkCameraGain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.PnlDisplay1);
            this.Name = "UcHome";
            ((System.ComponentModel.ISupportInitialize)(this.PnlDisplay1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudCameraExposureTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudCameraGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrkCameraExposureTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrkCameraGain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox PnlDisplay1;
        private Button BtnStop;
        private Button BtnStart;
        private Label label1;
        private Label label2;
        private Label label5;
        private NumericUpDown NudCameraExposureTime;
        private NumericUpDown NudCameraGain;
        private TrackBar TrkCameraExposureTime;
        private TrackBar TrkCameraGain;
        private Label LblResult;
        private Label LblOK;
        private Label label4;
        private Label label6;
        private Label label7;
        private Label LblX;
        private Label LblY;
        private CheckBox InspectSwitch;
        private System.Windows.Forms.Timer Timer1000;
        private Label LblInspectPerSec;
        private Label label8;
        private Label label9;
        private Label LblFramePersec;
        private Label label3;
        private Label label10;
        private Label LblLicense;
        private CheckBox ChkSave;
        private Label lbl5;
        private Label LblScore;
    }
}
