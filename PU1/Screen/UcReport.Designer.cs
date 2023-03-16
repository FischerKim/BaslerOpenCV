namespace PU1
{
    partial class UcReport
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
            this.label1 = new System.Windows.Forms.Label();
            this.PnlDisplay1 = new System.Windows.Forms.PictureBox();
            this.LblY = new System.Windows.Forms.Label();
            this.LblX = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LblOK = new System.Windows.Forms.Label();
            this.LblResult = new System.Windows.Forms.Label();
            this.DgvReport = new System.Windows.Forms.DataGridView();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.LblScore = new System.Windows.Forms.Label();
            this.DateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Recipe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.PnlDisplay1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvReport)).BeginInit();
            this.SuspendLayout();
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
            this.label1.TabIndex = 16;
            this.label1.Text = "Report";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PnlDisplay1
            // 
            this.PnlDisplay1.BackColor = System.Drawing.Color.MediumBlue;
            this.PnlDisplay1.Location = new System.Drawing.Point(0, 40);
            this.PnlDisplay1.Name = "PnlDisplay1";
            this.PnlDisplay1.Size = new System.Drawing.Size(658, 474);
            this.PnlDisplay1.TabIndex = 15;
            this.PnlDisplay1.TabStop = false;
            // 
            // LblY
            // 
            this.LblY.BackColor = System.Drawing.Color.White;
            this.LblY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblY.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblY.ForeColor = System.Drawing.Color.Black;
            this.LblY.Location = new System.Drawing.Point(490, 596);
            this.LblY.Name = "LblY";
            this.LblY.Size = new System.Drawing.Size(162, 40);
            this.LblY.TabIndex = 41;
            this.LblY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblX
            // 
            this.LblX.BackColor = System.Drawing.Color.White;
            this.LblX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblX.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblX.ForeColor = System.Drawing.Color.Black;
            this.LblX.Location = new System.Drawing.Point(490, 556);
            this.LblX.Name = "LblX";
            this.LblX.Size = new System.Drawing.Size(162, 40);
            this.LblX.TabIndex = 40;
            this.LblX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(327, 597);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(162, 40);
            this.label7.TabIndex = 39;
            this.label7.Text = "Y";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(327, 556);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(162, 40);
            this.label6.TabIndex = 38;
            this.label6.Text = "X";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(1, 556);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 40);
            this.label4.TabIndex = 37;
            this.label4.Text = "OK";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblOK
            // 
            this.LblOK.BackColor = System.Drawing.Color.White;
            this.LblOK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblOK.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblOK.ForeColor = System.Drawing.Color.Black;
            this.LblOK.Location = new System.Drawing.Point(164, 556);
            this.LblOK.Name = "LblOK";
            this.LblOK.Size = new System.Drawing.Size(162, 40);
            this.LblOK.TabIndex = 36;
            this.LblOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblResult
            // 
            this.LblResult.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.LblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblResult.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblResult.ForeColor = System.Drawing.Color.White;
            this.LblResult.Location = new System.Drawing.Point(1, 515);
            this.LblResult.Name = "LblResult";
            this.LblResult.Size = new System.Drawing.Size(651, 40);
            this.LblResult.TabIndex = 35;
            this.LblResult.Text = "Result";
            this.LblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DgvReport
            // 
            this.DgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DateTime,
            this.Recipe,
            this.Result,
            this.Score,
            this.X,
            this.Y});
            this.DgvReport.Location = new System.Drawing.Point(658, 40);
            this.DgvReport.Name = "DgvReport";
            this.DgvReport.RowTemplate.Height = 25;
            this.DgvReport.Size = new System.Drawing.Size(625, 637);
            this.DgvReport.TabIndex = 42;
            this.DgvReport.SelectionChanged += new System.EventHandler(this.DgvReport_SelectionChanged);
            // 
            // BtnSearch
            // 
            this.BtnSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnSearch.BackColor = System.Drawing.Color.Chocolate;
            this.BtnSearch.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnSearch.ForeColor = System.Drawing.Color.White;
            this.BtnSearch.Location = new System.Drawing.Point(1161, 3);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(120, 35);
            this.BtnSearch.TabIndex = 43;
            this.BtnSearch.Tag = "";
            this.BtnSearch.Text = "Search";
            this.BtnSearch.UseVisualStyleBackColor = false;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePicker1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePicker1.Location = new System.Drawing.Point(664, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(491, 32);
            this.dateTimePicker1.TabIndex = 44;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(1, 597);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 40);
            this.label5.TabIndex = 46;
            this.label5.Text = "SCORE";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblScore
            // 
            this.LblScore.BackColor = System.Drawing.Color.White;
            this.LblScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblScore.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblScore.ForeColor = System.Drawing.Color.Black;
            this.LblScore.Location = new System.Drawing.Point(164, 597);
            this.LblScore.Name = "LblScore";
            this.LblScore.Size = new System.Drawing.Size(162, 40);
            this.LblScore.TabIndex = 45;
            this.LblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DateTime
            // 
            this.DateTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DateTime.DataPropertyName = "DATETIME";
            this.DateTime.FillWeight = 20F;
            this.DateTime.HeaderText = "Date";
            this.DateTime.Name = "DateTime";
            // 
            // Recipe
            // 
            this.Recipe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Recipe.DataPropertyName = "RECIPE";
            this.Recipe.FillWeight = 10F;
            this.Recipe.HeaderText = "Recipe";
            this.Recipe.Name = "Recipe";
            // 
            // Result
            // 
            this.Result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Result.DataPropertyName = "HEAD1_RESULT";
            this.Result.FillWeight = 15F;
            this.Result.HeaderText = "Result";
            this.Result.Name = "Result";
            // 
            // Score
            // 
            this.Score.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Score.DataPropertyName = "HEAD1_SCORE";
            this.Score.FillWeight = 15F;
            this.Score.HeaderText = "Score";
            this.Score.Name = "Score";
            // 
            // X
            // 
            this.X.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.X.DataPropertyName = "HEAD1_PIXEL_X";
            this.X.FillWeight = 20F;
            this.X.HeaderText = "X";
            this.X.Name = "X";
            // 
            // Y
            // 
            this.Y.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Y.DataPropertyName = "HEAD1_PIXEL_Y";
            this.Y.FillWeight = 20F;
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            // 
            // UcReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LblScore);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.DgvReport);
            this.Controls.Add(this.LblY);
            this.Controls.Add(this.LblX);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LblOK);
            this.Controls.Add(this.LblResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PnlDisplay1);
            this.Name = "UcReport";
            ((System.ComponentModel.ISupportInitialize)(this.PnlDisplay1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Label label1;
        private PictureBox PnlDisplay1;
        private Label LblY;
        private Label LblX;
        private Label label7;
        private Label label6;
        private Label label4;
        private Label LblOK;
        private Label LblResult;
        private DataGridView DgvReport;
        private Button BtnSearch;
        private DateTimePicker dateTimePicker1;
        private Label label5;
        private Label LblScore;
        private DataGridViewTextBoxColumn DateTime;
        private DataGridViewTextBoxColumn Recipe;
        private DataGridViewTextBoxColumn Result;
        private DataGridViewTextBoxColumn Score;
        private DataGridViewTextBoxColumn X;
        private DataGridViewTextBoxColumn Y;
    }
}
