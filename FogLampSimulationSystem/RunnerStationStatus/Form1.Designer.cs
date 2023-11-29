namespace RunnerStationStatus
{
    partial class Form1
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
            binGroupBox = new GroupBox();
            groupBox1 = new GroupBox();
            legendLabel3 = new Label();
            legendPanel3 = new Panel();
            legendLabel2 = new Label();
            legendPanel2 = new Panel();
            legendLabel1 = new Label();
            legendPanel1 = new Panel();
            panel8 = new Panel();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            harnessPanel = new Panel();
            harnessCountLabel = new Label();
            lensPanel = new Panel();
            lensCountLabel = new Label();
            bezelPanel = new Panel();
            bezelCountLabel = new Label();
            reflectorPanel = new Panel();
            reflectorCountLabel = new Label();
            bulbPanel = new Panel();
            bulbCountLabel = new Label();
            housingPanel = new Panel();
            housingCountLabel = new Label();
            statusStrip1 = new StatusStrip();
            warningLabel = new ToolStripStatusLabel();
            debugModeLabel = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            refreshWorkstationsToolStripMenuItem = new ToolStripMenuItem();
            enableDebugModeToolStripMenuItem = new ToolStripMenuItem();
            workstationComboBox = new ComboBox();
            binGroupBox.SuspendLayout();
            groupBox1.SuspendLayout();
            panel8.SuspendLayout();
            harnessPanel.SuspendLayout();
            lensPanel.SuspendLayout();
            bezelPanel.SuspendLayout();
            reflectorPanel.SuspendLayout();
            bulbPanel.SuspendLayout();
            housingPanel.SuspendLayout();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // binGroupBox
            // 
            binGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            binGroupBox.Controls.Add(groupBox1);
            binGroupBox.Controls.Add(panel8);
            binGroupBox.Location = new Point(12, 56);
            binGroupBox.Name = "binGroupBox";
            binGroupBox.Size = new Size(493, 302);
            binGroupBox.TabIndex = 0;
            binGroupBox.TabStop = false;
            binGroupBox.Text = "Parts Bin Status";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(legendLabel3);
            groupBox1.Controls.Add(legendPanel3);
            groupBox1.Controls.Add(legendLabel2);
            groupBox1.Controls.Add(legendPanel2);
            groupBox1.Controls.Add(legendLabel1);
            groupBox1.Controls.Add(legendPanel1);
            groupBox1.Location = new Point(372, 90);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(108, 122);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Legend";
            // 
            // legendLabel3
            // 
            legendLabel3.AutoSize = true;
            legendLabel3.Location = new Point(32, 94);
            legendLabel3.Name = "legendLabel3";
            legendLabel3.Size = new Size(69, 15);
            legendLabel3.TabIndex = 5;
            legendLabel3.Text = "Last 5 Items";
            // 
            // legendPanel3
            // 
            legendPanel3.BackColor = Color.FromArgb(255, 128, 128);
            legendPanel3.BorderStyle = BorderStyle.FixedSingle;
            legendPanel3.Location = new Point(6, 92);
            legendPanel3.Name = "legendPanel3";
            legendPanel3.Size = new Size(20, 20);
            legendPanel3.TabIndex = 4;
            // 
            // legendLabel2
            // 
            legendLabel2.AutoSize = true;
            legendLabel2.Location = new Point(32, 60);
            legendLabel2.Name = "legendLabel2";
            legendLabel2.Size = new Size(62, 15);
            legendLabel2.TabIndex = 4;
            legendLabel2.Text = "< 50% Full";
            // 
            // legendPanel2
            // 
            legendPanel2.BackColor = Color.FromArgb(255, 255, 128);
            legendPanel2.BorderStyle = BorderStyle.FixedSingle;
            legendPanel2.Location = new Point(6, 57);
            legendPanel2.Name = "legendPanel2";
            legendPanel2.Size = new Size(20, 20);
            legendPanel2.TabIndex = 3;
            // 
            // legendLabel1
            // 
            legendLabel1.AutoSize = true;
            legendLabel1.Location = new Point(32, 25);
            legendLabel1.Name = "legendLabel1";
            legendLabel1.Size = new Size(59, 15);
            legendLabel1.TabIndex = 3;
            legendLabel1.Text = ">50% Full";
            // 
            // legendPanel1
            // 
            legendPanel1.BackColor = Color.FromArgb(128, 255, 128);
            legendPanel1.BorderStyle = BorderStyle.FixedSingle;
            legendPanel1.Location = new Point(6, 22);
            legendPanel1.Name = "legendPanel1";
            legendPanel1.Size = new Size(20, 20);
            legendPanel1.TabIndex = 2;
            // 
            // panel8
            // 
            panel8.Controls.Add(label10);
            panel8.Controls.Add(label11);
            panel8.Controls.Add(label12);
            panel8.Controls.Add(label9);
            panel8.Controls.Add(label8);
            panel8.Controls.Add(label7);
            panel8.Controls.Add(harnessPanel);
            panel8.Controls.Add(lensPanel);
            panel8.Controls.Add(bezelPanel);
            panel8.Controls.Add(reflectorPanel);
            panel8.Controls.Add(bulbPanel);
            panel8.Controls.Add(housingPanel);
            panel8.Location = new Point(6, 22);
            panel8.Name = "panel8";
            panel8.Size = new Size(360, 270);
            panel8.TabIndex = 7;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(274, 137);
            label10.Name = "label10";
            label10.Size = new Size(46, 21);
            label10.TabIndex = 12;
            label10.Text = "Bezel";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(154, 137);
            label11.Name = "label11";
            label11.Size = new Size(41, 21);
            label11.TabIndex = 11;
            label11.Text = "Bulb";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label12.Location = new Point(38, 137);
            label12.Name = "label12";
            label12.Size = new Size(42, 21);
            label12.TabIndex = 10;
            label12.Text = "Lens";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(263, 10);
            label9.Name = "label9";
            label9.Size = new Size(68, 21);
            label9.TabIndex = 9;
            label9.Text = "Housing";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(140, 10);
            label8.Name = "label8";
            label8.Size = new Size(72, 21);
            label8.TabIndex = 8;
            label8.Text = "Reflector";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(27, 10);
            label7.Name = "label7";
            label7.Size = new Size(66, 21);
            label7.TabIndex = 7;
            label7.Text = "Harness";
            // 
            // harnessPanel
            // 
            harnessPanel.BackColor = Color.FromArgb(128, 255, 128);
            harnessPanel.BorderStyle = BorderStyle.FixedSingle;
            harnessPanel.Controls.Add(harnessCountLabel);
            harnessPanel.Location = new Point(10, 34);
            harnessPanel.Name = "harnessPanel";
            harnessPanel.Size = new Size(100, 100);
            harnessPanel.TabIndex = 0;
            // 
            // harnessCountLabel
            // 
            harnessCountLabel.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            harnessCountLabel.Location = new Point(-1, 0);
            harnessCountLabel.Name = "harnessCountLabel";
            harnessCountLabel.Size = new Size(100, 98);
            harnessCountLabel.TabIndex = 1;
            harnessCountLabel.Text = "--";
            harnessCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            harnessCountLabel.DoubleClick += harnessCountLabel_DoubleClick;
            // 
            // lensPanel
            // 
            lensPanel.BackColor = Color.FromArgb(128, 255, 128);
            lensPanel.BorderStyle = BorderStyle.FixedSingle;
            lensPanel.Controls.Add(lensCountLabel);
            lensPanel.Location = new Point(10, 161);
            lensPanel.Name = "lensPanel";
            lensPanel.Size = new Size(100, 100);
            lensPanel.TabIndex = 2;
            // 
            // lensCountLabel
            // 
            lensCountLabel.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            lensCountLabel.Location = new Point(-1, 0);
            lensCountLabel.Name = "lensCountLabel";
            lensCountLabel.Size = new Size(100, 99);
            lensCountLabel.TabIndex = 1;
            lensCountLabel.Text = "--";
            lensCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            lensCountLabel.DoubleClick += lensCountLabel_DoubleClick;
            // 
            // bezelPanel
            // 
            bezelPanel.BackColor = Color.FromArgb(128, 255, 128);
            bezelPanel.BorderStyle = BorderStyle.FixedSingle;
            bezelPanel.Controls.Add(bezelCountLabel);
            bezelPanel.Location = new Point(248, 161);
            bezelPanel.Name = "bezelPanel";
            bezelPanel.Size = new Size(100, 100);
            bezelPanel.TabIndex = 6;
            // 
            // bezelCountLabel
            // 
            bezelCountLabel.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            bezelCountLabel.Location = new Point(-1, 0);
            bezelCountLabel.Name = "bezelCountLabel";
            bezelCountLabel.Size = new Size(100, 98);
            bezelCountLabel.TabIndex = 1;
            bezelCountLabel.Text = "--";
            bezelCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            bezelCountLabel.DoubleClick += bezelCountLabel_DoubleClick;
            // 
            // reflectorPanel
            // 
            reflectorPanel.BackColor = Color.FromArgb(128, 255, 128);
            reflectorPanel.BorderStyle = BorderStyle.FixedSingle;
            reflectorPanel.Controls.Add(reflectorCountLabel);
            reflectorPanel.Location = new Point(127, 34);
            reflectorPanel.Name = "reflectorPanel";
            reflectorPanel.Size = new Size(100, 100);
            reflectorPanel.TabIndex = 3;
            // 
            // reflectorCountLabel
            // 
            reflectorCountLabel.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            reflectorCountLabel.Location = new Point(-1, 0);
            reflectorCountLabel.Name = "reflectorCountLabel";
            reflectorCountLabel.Size = new Size(100, 98);
            reflectorCountLabel.TabIndex = 1;
            reflectorCountLabel.Text = "--";
            reflectorCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            reflectorCountLabel.DoubleClick += reflectorCountLabel_DoubleClick;
            // 
            // bulbPanel
            // 
            bulbPanel.BackColor = Color.FromArgb(128, 255, 128);
            bulbPanel.BorderStyle = BorderStyle.FixedSingle;
            bulbPanel.Controls.Add(bulbCountLabel);
            bulbPanel.Location = new Point(127, 161);
            bulbPanel.Name = "bulbPanel";
            bulbPanel.Size = new Size(100, 100);
            bulbPanel.TabIndex = 5;
            // 
            // bulbCountLabel
            // 
            bulbCountLabel.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            bulbCountLabel.Location = new Point(-1, 0);
            bulbCountLabel.Name = "bulbCountLabel";
            bulbCountLabel.Size = new Size(100, 98);
            bulbCountLabel.TabIndex = 1;
            bulbCountLabel.Text = "--";
            bulbCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            bulbCountLabel.DoubleClick += bulbCountLabel_DoubleClick;
            // 
            // housingPanel
            // 
            housingPanel.BackColor = Color.FromArgb(128, 255, 128);
            housingPanel.BorderStyle = BorderStyle.FixedSingle;
            housingPanel.Controls.Add(housingCountLabel);
            housingPanel.Location = new Point(248, 34);
            housingPanel.Name = "housingPanel";
            housingPanel.Size = new Size(100, 100);
            housingPanel.TabIndex = 4;
            // 
            // housingCountLabel
            // 
            housingCountLabel.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            housingCountLabel.Location = new Point(-1, 0);
            housingCountLabel.Name = "housingCountLabel";
            housingCountLabel.Size = new Size(100, 99);
            housingCountLabel.TabIndex = 1;
            housingCountLabel.Text = "--";
            housingCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            housingCountLabel.DoubleClick += housingCountLabel_DoubleClick;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { warningLabel, debugModeLabel });
            statusStrip1.Location = new Point(0, 361);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(517, 22);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // warningLabel
            // 
            warningLabel.ForeColor = Color.Red;
            warningLabel.Name = "warningLabel";
            warningLabel.Size = new Size(118, 17);
            warningLabel.Text = "toolStripStatusLabel1";
            // 
            // debugModeLabel
            // 
            debugModeLabel.Name = "debugModeLabel";
            debugModeLabel.Size = new Size(0, 17);
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { optionsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(517, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { refreshWorkstationsToolStripMenuItem, enableDebugModeToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // refreshWorkstationsToolStripMenuItem
            // 
            refreshWorkstationsToolStripMenuItem.Name = "refreshWorkstationsToolStripMenuItem";
            refreshWorkstationsToolStripMenuItem.Size = new Size(185, 22);
            refreshWorkstationsToolStripMenuItem.Text = "Refresh Workstations";
            refreshWorkstationsToolStripMenuItem.Click += refreshWorkstationsToolStripMenuItem_Click;
            // 
            // enableDebugModeToolStripMenuItem
            // 
            enableDebugModeToolStripMenuItem.CheckOnClick = true;
            enableDebugModeToolStripMenuItem.Name = "enableDebugModeToolStripMenuItem";
            enableDebugModeToolStripMenuItem.Size = new Size(185, 22);
            enableDebugModeToolStripMenuItem.Text = "Enable Debug Mode";
            enableDebugModeToolStripMenuItem.Click += enableDebugModeToolStripMenuItem_Click;
            // 
            // workstationComboBox
            // 
            workstationComboBox.FormattingEnabled = true;
            workstationComboBox.Location = new Point(12, 27);
            workstationComboBox.Name = "workstationComboBox";
            workstationComboBox.Size = new Size(493, 23);
            workstationComboBox.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(517, 383);
            Controls.Add(workstationComboBox);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(binGroupBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Runner Station Status";
            Load += Form1_Load;
            binGroupBox.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            harnessPanel.ResumeLayout(false);
            lensPanel.ResumeLayout(false);
            bezelPanel.ResumeLayout(false);
            reflectorPanel.ResumeLayout(false);
            bulbPanel.ResumeLayout(false);
            housingPanel.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private ToolStripMenuItem enableDebugModeToolStripMenuItem;
        private ToolStripStatusLabel debugModeLabel;
    }
}