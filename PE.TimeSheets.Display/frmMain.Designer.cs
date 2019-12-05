namespace PE.TimeSheets.Display
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvTimeSheets = new System.Windows.Forms.DataGridView();
            this.lnkNew = new System.Windows.Forms.LinkLabel();
            this.lnkSubmit = new System.Windows.Forms.LinkLabel();
            this.txtDefaultHourlyRate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeSheets)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTimeSheets
            // 
            this.dgvTimeSheets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimeSheets.Location = new System.Drawing.Point(12, 53);
            this.dgvTimeSheets.Name = "dgvTimeSheets";
            this.dgvTimeSheets.Size = new System.Drawing.Size(906, 150);
            this.dgvTimeSheets.TabIndex = 0;
            this.dgvTimeSheets.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvTimeSheets_CellContentClick);
            this.dgvTimeSheets.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DgvTimeSheets_CellValidating);
            this.dgvTimeSheets.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DgvTimeSheets_DataBindingComplete);
            // 
            // lnkNew
            // 
            this.lnkNew.AutoSize = true;
            this.lnkNew.Location = new System.Drawing.Point(9, 26);
            this.lnkNew.Name = "lnkNew";
            this.lnkNew.Size = new System.Drawing.Size(41, 13);
            this.lnkNew.TabIndex = 1;
            this.lnkNew.TabStop = true;
            this.lnkNew.Text = "[ New ]";
            this.lnkNew.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkNew_LinkClicked);
            // 
            // lnkSubmit
            // 
            this.lnkSubmit.AutoSize = true;
            this.lnkSubmit.Location = new System.Drawing.Point(75, 26);
            this.lnkSubmit.Name = "lnkSubmit";
            this.lnkSubmit.Size = new System.Drawing.Size(51, 13);
            this.lnkSubmit.TabIndex = 2;
            this.lnkSubmit.TabStop = true;
            this.lnkSubmit.Text = "[ Submit ]";
            this.lnkSubmit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkSubmit_LinkClicked);
            // 
            // txtDefaultHourlyRate
            // 
            this.txtDefaultHourlyRate.Location = new System.Drawing.Point(816, 22);
            this.txtDefaultHourlyRate.Name = "txtDefaultHourlyRate";
            this.txtDefaultHourlyRate.Size = new System.Drawing.Size(100, 20);
            this.txtDefaultHourlyRate.TabIndex = 7;
            this.txtDefaultHourlyRate.Text = "250.50";
            this.txtDefaultHourlyRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDefaultHourlyRate_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(709, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Default Hourly Rate $";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 450);
            this.Controls.Add(this.txtDefaultHourlyRate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkSubmit);
            this.Controls.Add(this.lnkNew);
            this.Controls.Add(this.dgvTimeSheets);
            this.Name = "frmMain";
            this.Text = "PE - Time Sheets";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeSheets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTimeSheets;
        private System.Windows.Forms.LinkLabel lnkNew;
        private System.Windows.Forms.LinkLabel lnkSubmit;
        private System.Windows.Forms.TextBox txtDefaultHourlyRate;
        private System.Windows.Forms.Label label1;
    }
}