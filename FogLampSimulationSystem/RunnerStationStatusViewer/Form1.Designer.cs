
using System.Drawing;
using System.Windows.Forms;
namespace RunnerStationStatusViewer
{
    partial class Form1
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
            this.binGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.legendLabel3 = new System.Windows.Forms.Label();
            this.legendPanel3 = new System.Windows.Forms.Panel();
            this.legendLabel2 = new System.Windows.Forms.Label();
            this.legendPanel2 = new System.Windows.Forms.Panel();
            this.legendLabel1 = new System.Windows.Forms.Label();
            this.legendPanel1 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.harnessPanel = new System.Windows.Forms.Panel();
            this.harnessCountLabel = new System.Windows.Forms.Label();
            this.lensPanel = new System.Windows.Forms.Panel();
            this.lensCountLabel = new System.Windows.Forms.Label();
            this.bezelPanel = new System.Windows.Forms.Panel();
            this.bezelCountLabel = new System.Windows.Forms.Label();
            this.reflectorPanel = new System.Windows.Forms.Panel();
            this.reflectorCountLabel = new System.Windows.Forms.Label();
            this.bulbPanel = new System.Windows.Forms.Panel();
            this.bulbCountLabel = new System.Windows.Forms.Label();
            this.housingPanel = new System.Windows.Forms.Panel();
            this.housingCountLabel = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.warningLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.debugModeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshWorkstationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableVoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workstationComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.harnessProgressBar = new RunnerStationStatusViewer.ColorableProgressBar();
            this.lensProgressBar = new RunnerStationStatusViewer.ColorableProgressBar();
            this.bezelProgressBar = new RunnerStationStatusViewer.ColorableProgressBar();
            this.reflectorProgressBar = new RunnerStationStatusViewer.ColorableProgressBar();
            this.bulbProgressBar = new RunnerStationStatusViewer.ColorableProgressBar();
            this.housingProgressBar = new RunnerStationStatusViewer.ColorableProgressBar();
            this.binGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.harnessPanel.SuspendLayout();
            this.lensPanel.SuspendLayout();
            this.bezelPanel.SuspendLayout();
            this.reflectorPanel.SuspendLayout();
            this.bulbPanel.SuspendLayout();
            this.housingPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // binGroupBox
            // 
            this.binGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.binGroupBox.Controls.Add(this.groupBox1);
            this.binGroupBox.Controls.Add(this.panel8);
            this.binGroupBox.Location = new System.Drawing.Point(10, 49);
            this.binGroupBox.Name = "binGroupBox";
            this.binGroupBox.Size = new System.Drawing.Size(446, 261);
            this.binGroupBox.TabIndex = 0;
            this.binGroupBox.TabStop = false;
            this.binGroupBox.Text = "Parts Bin Status";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.legendLabel3);
            this.groupBox1.Controls.Add(this.legendPanel3);
            this.groupBox1.Controls.Add(this.legendLabel2);
            this.groupBox1.Controls.Add(this.legendPanel2);
            this.groupBox1.Controls.Add(this.legendLabel1);
            this.groupBox1.Controls.Add(this.legendPanel1);
            this.groupBox1.Location = new System.Drawing.Point(319, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(121, 140);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Legend";
            // 
            // legendLabel3
            // 
            this.legendLabel3.AutoSize = true;
            this.legendLabel3.Location = new System.Drawing.Point(28, 111);
            this.legendLabel3.Name = "legendLabel3";
            this.legendLabel3.Size = new System.Drawing.Size(64, 13);
            this.legendLabel3.TabIndex = 5;
            this.legendLabel3.Text = "Last 5 Items";
            // 
            // legendPanel3
            // 
            this.legendPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.legendPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.legendPanel3.Location = new System.Drawing.Point(6, 110);
            this.legendPanel3.Name = "legendPanel3";
            this.legendPanel3.Size = new System.Drawing.Size(17, 18);
            this.legendPanel3.TabIndex = 4;
            // 
            // legendLabel2
            // 
            this.legendLabel2.AutoSize = true;
            this.legendLabel2.Location = new System.Drawing.Point(28, 82);
            this.legendLabel2.Name = "legendLabel2";
            this.legendLabel2.Size = new System.Drawing.Size(55, 13);
            this.legendLabel2.TabIndex = 4;
            this.legendLabel2.Text = "< 50% Full";
            // 
            // legendPanel2
            // 
            this.legendPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.legendPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.legendPanel2.Location = new System.Drawing.Point(6, 79);
            this.legendPanel2.Name = "legendPanel2";
            this.legendPanel2.Size = new System.Drawing.Size(17, 18);
            this.legendPanel2.TabIndex = 3;
            // 
            // legendLabel1
            // 
            this.legendLabel1.AutoSize = true;
            this.legendLabel1.Location = new System.Drawing.Point(28, 52);
            this.legendLabel1.Name = "legendLabel1";
            this.legendLabel1.Size = new System.Drawing.Size(52, 13);
            this.legendLabel1.TabIndex = 3;
            this.legendLabel1.Text = ">50% Full";
            // 
            // legendPanel1
            // 
            this.legendPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.legendPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.legendPanel1.Location = new System.Drawing.Point(6, 49);
            this.legendPanel1.Name = "legendPanel1";
            this.legendPanel1.Size = new System.Drawing.Size(17, 18);
            this.legendPanel1.TabIndex = 2;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label10);
            this.panel8.Controls.Add(this.label11);
            this.panel8.Controls.Add(this.label12);
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.label8);
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.harnessPanel);
            this.panel8.Controls.Add(this.lensPanel);
            this.panel8.Controls.Add(this.bezelPanel);
            this.panel8.Controls.Add(this.reflectorPanel);
            this.panel8.Controls.Add(this.bulbPanel);
            this.panel8.Controls.Add(this.housingPanel);
            this.panel8.Location = new System.Drawing.Point(5, 19);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(309, 234);
            this.panel8.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label10.Location = new System.Drawing.Point(235, 119);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 21);
            this.label10.TabIndex = 12;
            this.label10.Text = "Bezel";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label11.Location = new System.Drawing.Point(132, 119);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 21);
            this.label11.TabIndex = 11;
            this.label11.Text = "Bulb";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label12.Location = new System.Drawing.Point(33, 119);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 21);
            this.label12.TabIndex = 10;
            this.label12.Text = "Lens";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label9.Location = new System.Drawing.Point(225, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 21);
            this.label9.TabIndex = 9;
            this.label9.Text = "Housing";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label8.Location = new System.Drawing.Point(120, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 21);
            this.label8.TabIndex = 8;
            this.label8.Text = "Reflector";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label7.Location = new System.Drawing.Point(23, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 21);
            this.label7.TabIndex = 7;
            this.label7.Text = "Harness";
            // 
            // harnessPanel
            // 
            this.harnessPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.harnessPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.harnessPanel.Controls.Add(this.harnessProgressBar);
            this.harnessPanel.Controls.Add(this.harnessCountLabel);
            this.harnessPanel.Location = new System.Drawing.Point(9, 32);
            this.harnessPanel.Name = "harnessPanel";
            this.harnessPanel.Size = new System.Drawing.Size(86, 87);
            this.harnessPanel.TabIndex = 0;
            // 
            // harnessCountLabel
            // 
            this.harnessCountLabel.BackColor = System.Drawing.Color.Transparent;
            this.harnessCountLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.harnessCountLabel.Location = new System.Drawing.Point(-1, 54);
            this.harnessCountLabel.Name = "harnessCountLabel";
            this.harnessCountLabel.Size = new System.Drawing.Size(86, 31);
            this.harnessCountLabel.TabIndex = 1;
            this.harnessCountLabel.Text = "--";
            this.harnessCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lensPanel
            // 
            this.lensPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lensPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lensPanel.Controls.Add(this.lensProgressBar);
            this.lensPanel.Controls.Add(this.lensCountLabel);
            this.lensPanel.Location = new System.Drawing.Point(9, 140);
            this.lensPanel.Name = "lensPanel";
            this.lensPanel.Size = new System.Drawing.Size(86, 87);
            this.lensPanel.TabIndex = 2;
            // 
            // lensCountLabel
            // 
            this.lensCountLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lensCountLabel.Location = new System.Drawing.Point(-1, 54);
            this.lensCountLabel.Name = "lensCountLabel";
            this.lensCountLabel.Size = new System.Drawing.Size(86, 31);
            this.lensCountLabel.TabIndex = 1;
            this.lensCountLabel.Text = "--";
            this.lensCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bezelPanel
            // 
            this.bezelPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.bezelPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bezelPanel.Controls.Add(this.bezelProgressBar);
            this.bezelPanel.Controls.Add(this.bezelCountLabel);
            this.bezelPanel.Location = new System.Drawing.Point(213, 140);
            this.bezelPanel.Name = "bezelPanel";
            this.bezelPanel.Size = new System.Drawing.Size(86, 87);
            this.bezelPanel.TabIndex = 6;
            // 
            // bezelCountLabel
            // 
            this.bezelCountLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.bezelCountLabel.Location = new System.Drawing.Point(-1, 54);
            this.bezelCountLabel.Name = "bezelCountLabel";
            this.bezelCountLabel.Size = new System.Drawing.Size(86, 31);
            this.bezelCountLabel.TabIndex = 1;
            this.bezelCountLabel.Text = "--";
            this.bezelCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // reflectorPanel
            // 
            this.reflectorPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.reflectorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reflectorPanel.Controls.Add(this.reflectorProgressBar);
            this.reflectorPanel.Controls.Add(this.reflectorCountLabel);
            this.reflectorPanel.Location = new System.Drawing.Point(109, 32);
            this.reflectorPanel.Name = "reflectorPanel";
            this.reflectorPanel.Size = new System.Drawing.Size(86, 87);
            this.reflectorPanel.TabIndex = 3;
            // 
            // reflectorCountLabel
            // 
            this.reflectorCountLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.reflectorCountLabel.Location = new System.Drawing.Point(-1, 54);
            this.reflectorCountLabel.Name = "reflectorCountLabel";
            this.reflectorCountLabel.Size = new System.Drawing.Size(86, 31);
            this.reflectorCountLabel.TabIndex = 1;
            this.reflectorCountLabel.Text = "--";
            this.reflectorCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bulbPanel
            // 
            this.bulbPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.bulbPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bulbPanel.Controls.Add(this.bulbProgressBar);
            this.bulbPanel.Controls.Add(this.bulbCountLabel);
            this.bulbPanel.Location = new System.Drawing.Point(109, 140);
            this.bulbPanel.Name = "bulbPanel";
            this.bulbPanel.Size = new System.Drawing.Size(86, 87);
            this.bulbPanel.TabIndex = 5;
            // 
            // bulbCountLabel
            // 
            this.bulbCountLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.bulbCountLabel.Location = new System.Drawing.Point(-1, 54);
            this.bulbCountLabel.Name = "bulbCountLabel";
            this.bulbCountLabel.Size = new System.Drawing.Size(86, 31);
            this.bulbCountLabel.TabIndex = 1;
            this.bulbCountLabel.Text = "--";
            this.bulbCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // housingPanel
            // 
            this.housingPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.housingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.housingPanel.Controls.Add(this.housingProgressBar);
            this.housingPanel.Controls.Add(this.housingCountLabel);
            this.housingPanel.Location = new System.Drawing.Point(213, 32);
            this.housingPanel.Name = "housingPanel";
            this.housingPanel.Size = new System.Drawing.Size(86, 87);
            this.housingPanel.TabIndex = 4;
            // 
            // housingCountLabel
            // 
            this.housingCountLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.housingCountLabel.Location = new System.Drawing.Point(-1, 54);
            this.housingCountLabel.Name = "housingCountLabel";
            this.housingCountLabel.Size = new System.Drawing.Size(86, 32);
            this.housingCountLabel.TabIndex = 1;
            this.housingCountLabel.Text = "--";
            this.housingCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.warningLabel,
            this.debugModeLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 313);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(466, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // warningLabel
            // 
            this.warningLabel.ForeColor = System.Drawing.Color.Red;
            this.warningLabel.Name = "warningLabel";
            this.warningLabel.Size = new System.Drawing.Size(118, 17);
            this.warningLabel.Text = "toolStripStatusLabel1";
            // 
            // debugModeLabel
            // 
            this.debugModeLabel.Name = "debugModeLabel";
            this.debugModeLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(466, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshWorkstationsToolStripMenuItem,
            this.disableVoiceToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // refreshWorkstationsToolStripMenuItem
            // 
            this.refreshWorkstationsToolStripMenuItem.Name = "refreshWorkstationsToolStripMenuItem";
            this.refreshWorkstationsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.refreshWorkstationsToolStripMenuItem.Text = "Refresh Workstations";
            this.refreshWorkstationsToolStripMenuItem.Click += new System.EventHandler(this.refreshWorkstationsToolStripMenuItem_Click);
            // 
            // disableVoiceToolStripMenuItem
            // 
            this.disableVoiceToolStripMenuItem.CheckOnClick = true;
            this.disableVoiceToolStripMenuItem.Name = "disableVoiceToolStripMenuItem";
            this.disableVoiceToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.disableVoiceToolStripMenuItem.Text = "Disable Voice";
            this.disableVoiceToolStripMenuItem.Click += new System.EventHandler(this.disableVoiceToolStripMenuItem_Click);
            // 
            // workstationComboBox
            // 
            this.workstationComboBox.FormattingEnabled = true;
            this.workstationComboBox.Location = new System.Drawing.Point(10, 23);
            this.workstationComboBox.Name = "workstationComboBox";
            this.workstationComboBox.Size = new System.Drawing.Size(423, 21);
            this.workstationComboBox.TabIndex = 6;
            this.workstationComboBox.SelectedIndexChanged += new System.EventHandler(this.workstationComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Overfilled";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(6, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(17, 18);
            this.panel1.TabIndex = 6;
            // 
            // harnessProgressBar
            // 
            this.harnessProgressBar.Location = new System.Drawing.Point(-1, -1);
            this.harnessProgressBar.Name = "harnessProgressBar";
            this.harnessProgressBar.Size = new System.Drawing.Size(86, 52);
            this.harnessProgressBar.TabIndex = 2;
            // 
            // lensProgressBar
            // 
            this.lensProgressBar.Location = new System.Drawing.Point(-1, 0);
            this.lensProgressBar.Name = "lensProgressBar";
            this.lensProgressBar.Size = new System.Drawing.Size(86, 52);
            this.lensProgressBar.TabIndex = 5;
            // 
            // bezelProgressBar
            // 
            this.bezelProgressBar.Location = new System.Drawing.Point(-1, -1);
            this.bezelProgressBar.Name = "bezelProgressBar";
            this.bezelProgressBar.Size = new System.Drawing.Size(86, 52);
            this.bezelProgressBar.TabIndex = 7;
            // 
            // reflectorProgressBar
            // 
            this.reflectorProgressBar.Location = new System.Drawing.Point(-1, -1);
            this.reflectorProgressBar.Name = "reflectorProgressBar";
            this.reflectorProgressBar.Size = new System.Drawing.Size(86, 52);
            this.reflectorProgressBar.TabIndex = 3;
            // 
            // bulbProgressBar
            // 
            this.bulbProgressBar.Location = new System.Drawing.Point(-1, -1);
            this.bulbProgressBar.Name = "bulbProgressBar";
            this.bulbProgressBar.Size = new System.Drawing.Size(86, 52);
            this.bulbProgressBar.TabIndex = 6;
            // 
            // housingProgressBar
            // 
            this.housingProgressBar.Location = new System.Drawing.Point(-1, -1);
            this.housingProgressBar.Name = "housingProgressBar";
            this.housingProgressBar.Size = new System.Drawing.Size(86, 52);
            this.housingProgressBar.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 335);
            this.Controls.Add(this.workstationComboBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.binGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Runner Station Status";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.binGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.harnessPanel.ResumeLayout(false);
            this.lensPanel.ResumeLayout(false);
            this.bezelPanel.ResumeLayout(false);
            this.reflectorPanel.ResumeLayout(false);
            this.bulbPanel.ResumeLayout(false);
            this.housingPanel.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox binGroupBox;
        private Panel harnessPanel;
        private Panel bezelPanel;
        private Label bezelCountLabel;
        private Panel bulbPanel;
        private Label bulbCountLabel;
        private Panel housingPanel;
        private Label housingCountLabel;
        private Panel reflectorPanel;
        private Label reflectorCountLabel;
        private Panel lensPanel;
        private Label lensCountLabel;
        private Label harnessCountLabel;
        private Panel panel8;
        private Panel legendPanel1;
        private Label label7;
        private Label label8;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label9;
        private GroupBox groupBox1;
        private Label legendLabel1;
        private Label legendLabel3;
        private Panel legendPanel3;
        private Label legendLabel2;
        private Panel legendPanel2;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel warningLabel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem refreshWorkstationsToolStripMenuItem;
        private ComboBox workstationComboBox;
        private ToolStripMenuItem disableVoiceToolStripMenuItem;
        private ToolStripStatusLabel debugModeLabel;
        private ColorableProgressBar harnessProgressBar;
        private Label label1;
        private Panel panel1;
        private ColorableProgressBar lensProgressBar;
        private ColorableProgressBar reflectorProgressBar;
        private ColorableProgressBar housingProgressBar;
        private ColorableProgressBar bezelProgressBar;
        private ColorableProgressBar bulbProgressBar;
    }
}

