namespace TextClipper.Views
{
    partial class HotkeySettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotkeySettings));
            this.txtClippingHotkey = new System.Windows.Forms.TextBox();
            this.btnSaveClose = new System.Windows.Forms.Button();
            this.lblSelectionHotkey = new System.Windows.Forms.Label();
            this.pnlHowTo = new System.Windows.Forms.Panel();
            this.pnlHowToSidebar = new System.Windows.Forms.Panel();
            this.lblUsageInfo = new System.Windows.Forms.Label();
            this.lblHowToTitle = new System.Windows.Forms.Label();
            this.pnlHowTo.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtClippingHotkey
            // 
            this.txtClippingHotkey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClippingHotkey.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClippingHotkey.Location = new System.Drawing.Point(32, 49);
            this.txtClippingHotkey.Name = "txtClippingHotkey";
            this.txtClippingHotkey.Size = new System.Drawing.Size(338, 27);
            this.txtClippingHotkey.TabIndex = 0;
            this.txtClippingHotkey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSaveClose
            // 
            this.btnSaveClose.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSaveClose.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnSaveClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSaveClose.ForeColor = System.Drawing.Color.White;
            this.btnSaveClose.Location = new System.Drawing.Point(32, 188);
            this.btnSaveClose.Name = "btnSaveClose";
            this.btnSaveClose.Size = new System.Drawing.Size(338, 30);
            this.btnSaveClose.TabIndex = 4;
            this.btnSaveClose.Text = "Save && Close";
            this.btnSaveClose.UseVisualStyleBackColor = false;
            this.btnSaveClose.Click += new System.EventHandler(this.btnSaveClose_Click);
            // 
            // lblSelectionHotkey
            // 
            this.lblSelectionHotkey.AutoSize = true;
            this.lblSelectionHotkey.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSelectionHotkey.Location = new System.Drawing.Point(29, 27);
            this.lblSelectionHotkey.Name = "lblSelectionHotkey";
            this.lblSelectionHotkey.Size = new System.Drawing.Size(181, 15);
            this.lblSelectionHotkey.TabIndex = 1;
            this.lblSelectionHotkey.Text = "Change the text-clipping hotkey:";
            // 
            // pnlHowTo
            // 
            this.pnlHowTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(255)))));
            this.pnlHowTo.Controls.Add(this.pnlHowToSidebar);
            this.pnlHowTo.Controls.Add(this.lblUsageInfo);
            this.pnlHowTo.Controls.Add(this.lblHowToTitle);
            this.pnlHowTo.Location = new System.Drawing.Point(32, 82);
            this.pnlHowTo.Name = "pnlHowTo";
            this.pnlHowTo.Size = new System.Drawing.Size(338, 100);
            this.pnlHowTo.TabIndex = 9;
            // 
            // pnlHowToSidebar
            // 
            this.pnlHowToSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.pnlHowToSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlHowToSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlHowToSidebar.Name = "pnlHowToSidebar";
            this.pnlHowToSidebar.Size = new System.Drawing.Size(5, 100);
            this.pnlHowToSidebar.TabIndex = 9;
            // 
            // lblUsageInfo
            // 
            this.lblUsageInfo.AutoSize = true;
            this.lblUsageInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUsageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUsageInfo.Location = new System.Drawing.Point(18, 37);
            this.lblUsageInfo.Name = "lblUsageInfo";
            this.lblUsageInfo.Size = new System.Drawing.Size(303, 45);
            this.lblUsageInfo.TabIndex = 7;
            this.lblUsageInfo.Text = "If the textbox is not selected, click on it, then simply hit \r\nany key or key com" +
    "bination to enter the keys you prefer.\r\nOnce done, click \'Save && Close\' to save" +
    " the key(s).";
            // 
            // lblHowToTitle
            // 
            this.lblHowToTitle.AutoSize = true;
            this.lblHowToTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHowToTitle.ForeColor = System.Drawing.Color.Black;
            this.lblHowToTitle.Location = new System.Drawing.Point(17, 13);
            this.lblHowToTitle.Name = "lblHowToTitle";
            this.lblHowToTitle.Size = new System.Drawing.Size(97, 20);
            this.lblHowToTitle.TabIndex = 6;
            this.lblHowToTitle.Text = "How to use...";
            // 
            // HotkeySettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(399, 244);
            this.Controls.Add(this.pnlHowTo);
            this.Controls.Add(this.btnSaveClose);
            this.Controls.Add(this.lblSelectionHotkey);
            this.Controls.Add(this.txtClippingHotkey);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HotkeySettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hotkey Settings";
            this.Load += new System.EventHandler(this.HotkeySettings_Load);
            this.Shown += new System.EventHandler(this.HotkeySettings_Shown);
            this.pnlHowTo.ResumeLayout(false);
            this.pnlHowTo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.TextBox txtClippingHotkey;
        private System.Windows.Forms.Button btnSaveClose;
        private System.Windows.Forms.Label lblSelectionHotkey;
        private System.Windows.Forms.Panel pnlHowTo;
        private System.Windows.Forms.Panel pnlHowToSidebar;
        private System.Windows.Forms.Label lblUsageInfo;
        private System.Windows.Forms.Label lblHowToTitle;
    }
}