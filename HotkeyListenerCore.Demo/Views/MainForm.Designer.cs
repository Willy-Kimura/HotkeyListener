namespace TextClipper.Views
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnChangeClippingHotkey = new System.Windows.Forms.Button();
            this.lstClippedTexts = new System.Windows.Forms.ListBox();
            this.lblClipppingHotkeyInfo = new System.Windows.Forms.Label();
            this.lblHowToTitle = new System.Windows.Forms.Label();
            this.lblUsageInfo = new System.Windows.Forms.Label();
            this.pnlHowTo = new System.Windows.Forms.Panel();
            this.pnlHowToSidebar = new System.Windows.Forms.Panel();
            this.lblNoClippedTexts = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlHowTo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(14, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(450, 30);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Text Clipper | All clipped texts will appear here...";
            // 
            // btnChangeClippingHotkey
            // 
            this.btnChangeClippingHotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeClippingHotkey.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnChangeClippingHotkey.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnChangeClippingHotkey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeClippingHotkey.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnChangeClippingHotkey.ForeColor = System.Drawing.Color.White;
            this.btnChangeClippingHotkey.Location = new System.Drawing.Point(604, 46);
            this.btnChangeClippingHotkey.Name = "btnChangeClippingHotkey";
            this.btnChangeClippingHotkey.Size = new System.Drawing.Size(184, 30);
            this.btnChangeClippingHotkey.TabIndex = 3;
            this.btnChangeClippingHotkey.Text = "Change Clipping Hotkey";
            this.btnChangeClippingHotkey.UseVisualStyleBackColor = false;
            this.btnChangeClippingHotkey.Click += new System.EventHandler(this.btnChangeHotkey_Click);
            // 
            // lstClippedTexts
            // 
            this.lstClippedTexts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstClippedTexts.BackColor = System.Drawing.Color.White;
            this.lstClippedTexts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstClippedTexts.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstClippedTexts.FormattingEnabled = true;
            this.lstClippedTexts.ItemHeight = 17;
            this.lstClippedTexts.Location = new System.Drawing.Point(12, 206);
            this.lstClippedTexts.Name = "lstClippedTexts";
            this.lstClippedTexts.ScrollAlwaysVisible = true;
            this.lstClippedTexts.Size = new System.Drawing.Size(776, 376);
            this.lstClippedTexts.TabIndex = 4;
            this.lstClippedTexts.DoubleClick += new System.EventHandler(this.lstClippedTexts_DoubleClick);
            this.lstClippedTexts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstClippedTexts_KeyDown);
            // 
            // lblClipppingHotkeyInfo
            // 
            this.lblClipppingHotkeyInfo.AutoSize = true;
            this.lblClipppingHotkeyInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClipppingHotkeyInfo.ForeColor = System.Drawing.Color.Black;
            this.lblClipppingHotkeyInfo.Location = new System.Drawing.Point(15, 46);
            this.lblClipppingHotkeyInfo.Name = "lblClipppingHotkeyInfo";
            this.lblClipppingHotkeyInfo.Size = new System.Drawing.Size(289, 20);
            this.lblClipppingHotkeyInfo.TabIndex = 5;
            this.lblClipppingHotkeyInfo.Text = "The current clipping hotkey is \"{Hotkey}\"";
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
            // lblUsageInfo
            // 
            this.lblUsageInfo.AutoSize = true;
            this.lblUsageInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUsageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUsageInfo.Location = new System.Drawing.Point(18, 37);
            this.lblUsageInfo.Name = "lblUsageInfo";
            this.lblUsageInfo.Size = new System.Drawing.Size(610, 60);
            this.lblUsageInfo.TabIndex = 7;
            this.lblUsageInfo.Text = resources.GetString("lblUsageInfo.Text");
            // 
            // pnlHowTo
            // 
            this.pnlHowTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHowTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(255)))));
            this.pnlHowTo.Controls.Add(this.pnlHowToSidebar);
            this.pnlHowTo.Controls.Add(this.lblUsageInfo);
            this.pnlHowTo.Controls.Add(this.lblHowToTitle);
            this.pnlHowTo.Location = new System.Drawing.Point(12, 82);
            this.pnlHowTo.Name = "pnlHowTo";
            this.pnlHowTo.Size = new System.Drawing.Size(776, 113);
            this.pnlHowTo.TabIndex = 8;
            // 
            // pnlHowToSidebar
            // 
            this.pnlHowToSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.pnlHowToSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlHowToSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlHowToSidebar.Name = "pnlHowToSidebar";
            this.pnlHowToSidebar.Size = new System.Drawing.Size(5, 113);
            this.pnlHowToSidebar.TabIndex = 9;
            // 
            // lblNoClippedTexts
            // 
            this.lblNoClippedTexts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNoClippedTexts.AutoSize = true;
            this.lblNoClippedTexts.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNoClippedTexts.ForeColor = System.Drawing.Color.DarkGray;
            this.lblNoClippedTexts.Location = new System.Drawing.Point(325, 373);
            this.lblNoClippedTexts.Name = "lblNoClippedTexts";
            this.lblNoClippedTexts.Size = new System.Drawing.Size(149, 15);
            this.lblNoClippedTexts.TabIndex = 10;
            this.lblNoClippedTexts.Text = "(No clipped texts available)";
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnMinimize.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnMinimize.Location = new System.Drawing.Point(504, 46);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(94, 30);
            this.btnMinimize.TabIndex = 11;
            this.btnMinimize.Text = "Minimize";
            this.toolTip.SetToolTip(this.btnMinimize, "Minimize window");
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 602);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.lblNoClippedTexts);
            this.Controls.Add(this.pnlHowTo);
            this.Controls.Add(this.lblClipppingHotkeyInfo);
            this.Controls.Add(this.lstClippedTexts);
            this.Controls.Add(this.btnChangeClippingHotkey);
            this.Controls.Add(this.lblTitle);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TextClipper (Demo)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlHowTo.ResumeLayout(false);
            this.pnlHowTo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnChangeClippingHotkey;
        private System.Windows.Forms.ListBox lstClippedTexts;
        private System.Windows.Forms.Label lblClipppingHotkeyInfo;
        private System.Windows.Forms.Label lblHowToTitle;
        private System.Windows.Forms.Label lblUsageInfo;
        private System.Windows.Forms.Panel pnlHowTo;
        private System.Windows.Forms.Panel pnlHowToSidebar;
        private System.Windows.Forms.Label lblNoClippedTexts;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

