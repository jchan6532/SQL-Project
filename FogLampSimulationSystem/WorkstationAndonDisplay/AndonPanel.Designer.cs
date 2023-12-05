namespace WorkstationAndonDisplay
{
    partial class AndonPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.partBinStatusBar = new RunnerStationStatusViewer.ColorableProgressBar();
            this.partNamePanel = new System.Windows.Forms.TableLayoutPanel();
            this.partNameLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.partCountLabel = new System.Windows.Forms.Label();
            this.partNamePanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // partBinStatusBar
            // 
            this.partBinStatusBar.BackColor = System.Drawing.Color.IndianRed;
            this.partBinStatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.partBinStatusBar.Location = new System.Drawing.Point(3, 122);
            this.partBinStatusBar.Name = "partBinStatusBar";
            this.partBinStatusBar.Size = new System.Drawing.Size(144, 25);
            this.partBinStatusBar.TabIndex = 0;
            // 
            // partNamePanel
            // 
            this.partNamePanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.partNamePanel.ColumnCount = 1;
            this.partNamePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.partNamePanel.Controls.Add(this.partNameLabel, 0, 0);
            this.partNamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.partNamePanel.Location = new System.Drawing.Point(3, 3);
            this.partNamePanel.Name = "partNamePanel";
            this.partNamePanel.RowCount = 1;
            this.partNamePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.partNamePanel.Size = new System.Drawing.Size(144, 55);
            this.partNamePanel.TabIndex = 1;
            // 
            // partNameLabel
            // 
            this.partNameLabel.AutoSize = true;
            this.partNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.partNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.partNameLabel.Location = new System.Drawing.Point(4, 1);
            this.partNameLabel.Name = "partNameLabel";
            this.partNameLabel.Size = new System.Drawing.Size(136, 53);
            this.partNameLabel.TabIndex = 0;
            this.partNameLabel.Text = "PART NAME LONG";
            this.partNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.partNamePanel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.partBinStatusBar, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.partCountLabel, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.32743F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.67257F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(150, 150);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // partCountLabel
            // 
            this.partCountLabel.AutoSize = true;
            this.partCountLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.partCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.partCountLabel.Location = new System.Drawing.Point(3, 61);
            this.partCountLabel.Name = "partCountLabel";
            this.partCountLabel.Size = new System.Drawing.Size(144, 58);
            this.partCountLabel.TabIndex = 2;
            this.partCountLabel.Text = "--";
            this.partCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AndonPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "AndonPanel";
            this.Load += new System.EventHandler(this.AndonPanel_Load);
            this.partNamePanel.ResumeLayout(false);
            this.partNamePanel.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RunnerStationStatusViewer.ColorableProgressBar partBinStatusBar;
        private System.Windows.Forms.TableLayoutPanel partNamePanel;
        private System.Windows.Forms.Label partNameLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label partCountLabel;
    }
}
