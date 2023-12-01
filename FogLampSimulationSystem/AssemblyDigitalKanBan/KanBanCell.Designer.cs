namespace AssemblyDigitalKanBan
{
    partial class KanBanCell
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.contributionToOrderChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.defectRateLabel = new System.Windows.Forms.Label();
            this.defectLabel = new System.Windows.Forms.Label();
            this.producedLabel = new System.Windows.Forms.Label();
            this.amountLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.workstationInfoLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.contributionToOrderChart)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contributionToOrderChart
            // 
            this.contributionToOrderChart.BorderSkin.BorderWidth = 3;
            chartArea2.Name = "ChartArea1";
            this.contributionToOrderChart.ChartAreas.Add(chartArea2);
            this.contributionToOrderChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.contributionToOrderChart.Legends.Add(legend2);
            this.contributionToOrderChart.Location = new System.Drawing.Point(0, 0);
            this.contributionToOrderChart.Name = "contributionToOrderChart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.contributionToOrderChart.Series.Add(series2);
            this.contributionToOrderChart.Size = new System.Drawing.Size(287, 146);
            this.contributionToOrderChart.TabIndex = 7;
            this.contributionToOrderChart.Text = "chart2";
            title2.Name = "Defect / Fog Lamp Ratio";
            this.contributionToOrderChart.Titles.Add(title2);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.defectRateLabel, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.defectLabel, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.producedLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.amountLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(507, 148);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // defectRateLabel
            // 
            this.defectRateLabel.AutoSize = true;
            this.defectRateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defectRateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defectRateLabel.Location = new System.Drawing.Point(379, 74);
            this.defectRateLabel.Margin = new System.Windows.Forms.Padding(0);
            this.defectRateLabel.Name = "defectRateLabel";
            this.defectRateLabel.Size = new System.Drawing.Size(127, 73);
            this.defectRateLabel.TabIndex = 7;
            this.defectRateLabel.Text = "--";
            this.defectRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // defectLabel
            // 
            this.defectLabel.AutoSize = true;
            this.defectLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defectLabel.Location = new System.Drawing.Point(256, 74);
            this.defectLabel.Name = "defectLabel";
            this.defectLabel.Size = new System.Drawing.Size(119, 73);
            this.defectLabel.TabIndex = 6;
            this.defectLabel.Text = "--";
            this.defectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // producedLabel
            // 
            this.producedLabel.AutoSize = true;
            this.producedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.producedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.producedLabel.Location = new System.Drawing.Point(130, 74);
            this.producedLabel.Name = "producedLabel";
            this.producedLabel.Size = new System.Drawing.Size(119, 73);
            this.producedLabel.TabIndex = 5;
            this.producedLabel.Text = "--";
            this.producedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // amountLabel
            // 
            this.amountLabel.AutoSize = true;
            this.amountLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.amountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountLabel.Location = new System.Drawing.Point(4, 74);
            this.amountLabel.Name = "amountLabel";
            this.amountLabel.Size = new System.Drawing.Size(119, 73);
            this.amountLabel.TabIndex = 4;
            this.amountLabel.Text = "--";
            this.amountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(386, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Defect Rate";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(267, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 50);
            this.label3.TabIndex = 2;
            this.label3.Text = "Defects Produced";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(134, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 50);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fog Lamps Produced";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Order Amount";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // workstationInfoLabel
            // 
            this.workstationInfoLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.workstationInfoLabel.AutoSize = true;
            this.workstationInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workstationInfoLabel.Location = new System.Drawing.Point(197, 12);
            this.workstationInfoLabel.Name = "workstationInfoLabel";
            this.workstationInfoLabel.Size = new System.Drawing.Size(478, 24);
            this.workstationInfoLabel.TabIndex = 9;
            this.workstationInfoLabel.Text = "Workstation # / Employee : [ ID : # | Name : Name Here ]";
            this.workstationInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.contributionToOrderChart);
            this.panel1.Location = new System.Drawing.Point(534, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(289, 148);
            this.panel1.TabIndex = 10;
            // 
            // KanBanCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.workstationInfoLabel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "KanBanCell";
            this.Size = new System.Drawing.Size(841, 206);
            this.Load += new System.EventHandler(this.KanBanCell_Load);
            ((System.ComponentModel.ISupportInitialize)(this.contributionToOrderChart)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart contributionToOrderChart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label defectRateLabel;
        private System.Windows.Forms.Label defectLabel;
        private System.Windows.Forms.Label producedLabel;
        private System.Windows.Forms.Label amountLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label workstationInfoLabel;
        private System.Windows.Forms.Panel panel1;
    }
}
