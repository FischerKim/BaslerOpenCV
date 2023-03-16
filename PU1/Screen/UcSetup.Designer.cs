namespace PU1
{
    partial class UcSetup
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
            this.TrkCameraGain = new System.Windows.Forms.TrackBar();
            this.TrkCameraExposureTime = new System.Windows.Forms.TrackBar();
            this.NudCameraGain = new System.Windows.Forms.NumericUpDown();
            this.NudCameraExposureTime = new System.Windows.Forms.NumericUpDown();
            this.CmbAcquisition = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CmbFrameMode = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.TrkCameraGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrkCameraExposureTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudCameraGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudCameraExposureTime)).BeginInit();
            this.SuspendLayout();
            // 
            // TrkCameraGain
            // 
            this.TrkCameraGain.Location = new System.Drawing.Point(194, 19);
            this.TrkCameraGain.Name = "TrkCameraGain";
            this.TrkCameraGain.Size = new System.Drawing.Size(316, 45);
            this.TrkCameraGain.TabIndex = 0;
            this.TrkCameraGain.ValueChanged += new System.EventHandler(this.TrkCameraOption_ValueChanged);
            // 
            // TrkCameraExposureTime
            // 
            this.TrkCameraExposureTime.Location = new System.Drawing.Point(194, 71);
            this.TrkCameraExposureTime.Name = "TrkCameraExposureTime";
            this.TrkCameraExposureTime.Size = new System.Drawing.Size(316, 45);
            this.TrkCameraExposureTime.TabIndex = 1;
            this.TrkCameraExposureTime.ValueChanged += new System.EventHandler(this.TrkCameraOption_ValueChanged);
            // 
            // NudCameraGain
            // 
            this.NudCameraGain.Location = new System.Drawing.Point(516, 19);
            this.NudCameraGain.Name = "NudCameraGain";
            this.NudCameraGain.Size = new System.Drawing.Size(120, 23);
            this.NudCameraGain.TabIndex = 2;
            // 
            // NudCameraExposureTime
            // 
            this.NudCameraExposureTime.Location = new System.Drawing.Point(516, 71);
            this.NudCameraExposureTime.Name = "NudCameraExposureTime";
            this.NudCameraExposureTime.Size = new System.Drawing.Size(120, 23);
            this.NudCameraExposureTime.TabIndex = 3;
            // 
            // CmbAcquisition
            // 
            this.CmbAcquisition.FormattingEnabled = true;
            this.CmbAcquisition.Items.AddRange(new object[] {
            "On",
            "Off"});
            this.CmbAcquisition.Location = new System.Drawing.Point(194, 110);
            this.CmbAcquisition.Name = "CmbAcquisition";
            this.CmbAcquisition.Size = new System.Drawing.Size(121, 23);
            this.CmbAcquisition.TabIndex = 4;
            this.CmbAcquisition.Tag = "ACQUISITION";
            this.CmbAcquisition.SelectedIndexChanged += new System.EventHandler(this.CmbCameraTriggerMode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DimGray;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 40);
            this.label1.TabIndex = 15;
            this.label1.Text = "Gain";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DimGray;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 40);
            this.label2.TabIndex = 16;
            this.label2.Text = "Exposure Time";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.DimGray;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(17, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 40);
            this.label3.TabIndex = 17;
            this.label3.Text = "Acq. Mode";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.DimGray;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(17, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 40);
            this.label4.TabIndex = 19;
            this.label4.Text = "Frame Mode";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CmbFrameMode
            // 
            this.CmbFrameMode.FormattingEnabled = true;
            this.CmbFrameMode.Items.AddRange(new object[] {
            "On",
            "Off"});
            this.CmbFrameMode.Location = new System.Drawing.Point(194, 151);
            this.CmbFrameMode.Name = "CmbFrameMode";
            this.CmbFrameMode.Size = new System.Drawing.Size(121, 23);
            this.CmbFrameMode.TabIndex = 18;
            this.CmbFrameMode.Tag = "FRAME";
            this.CmbFrameMode.SelectedIndexChanged += new System.EventHandler(this.CmbCameraTriggerMode_SelectedIndexChanged);
            // 
            // UcSetup
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CmbFrameMode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CmbAcquisition);
            this.Controls.Add(this.NudCameraExposureTime);
            this.Controls.Add(this.NudCameraGain);
            this.Controls.Add(this.TrkCameraExposureTime);
            this.Controls.Add(this.TrkCameraGain);
            this.Name = "UcSetup";
            ((System.ComponentModel.ISupportInitialize)(this.TrkCameraGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrkCameraExposureTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudCameraGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudCameraExposureTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TrackBar TrkCameraGain;
        private TrackBar TrkCameraExposureTime;
        private NumericUpDown NudCameraGain;
        private NumericUpDown NudCameraExposureTime;
        private ComboBox CmbAcquisition;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox CmbFrameMode;
    }
}
