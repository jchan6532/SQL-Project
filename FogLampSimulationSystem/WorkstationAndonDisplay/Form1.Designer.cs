namespace WorkstationAndonDisplay
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.runnerSignalLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.andonPanelTable = new System.Windows.Forms.TableLayoutPanel();
            this.outTablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.orderIdLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.defectRateLabel = new System.Windows.Forms.Label();
            this.defectsProducedLabel = new System.Windows.Forms.Label();
            this.lampsProducedLabel = new System.Windows.Forms.Label();
            this.orderAmountLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lampsProducedHereTitleLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.orderStatusProgressBar = new RunnerStationStatusViewer.ColorableProgressBar();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.totalLampsProduced = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.contributionToOrderChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel1.SuspendLayout();
            this.outTablePanel.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contributionToOrderChart)).BeginInit();
            this.SuspendLayout();
            // 
            // runnerSignalLabel
            // 
            this.runnerSignalLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.runnerSignalLabel.AutoSize = true;
            this.runnerSignalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runnerSignalLabel.ForeColor = System.Drawing.Color.Red;
            this.runnerSignalLabel.Location = new System.Drawing.Point(280, 9);
            this.runnerSignalLabel.Name = "runnerSignalLabel";
            this.runnerSignalLabel.Size = new System.Drawing.Size(520, 26);
            this.runnerSignalLabel.TabIndex = 0;
            this.runnerSignalLabel.Text = "Workstation # : NEED REFILL [ Harness, Lens ]";
            this.runnerSignalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.runnerSignalLabel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1081, 44);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // andonPanelTable
            // 
            this.andonPanelTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.andonPanelTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.andonPanelTable.ColumnCount = 6;
            this.andonPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.andonPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.andonPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.andonPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.andonPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.andonPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.andonPanelTable.Location = new System.Drawing.Point(1, 29);
            this.andonPanelTable.Margin = new System.Windows.Forms.Padding(0);
            this.andonPanelTable.Name = "andonPanelTable";
            this.andonPanelTable.RowCount = 1;
            this.andonPanelTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.andonPanelTable.Size = new System.Drawing.Size(1079, 106);
            this.andonPanelTable.TabIndex = 3;
            // 
            // outTablePanel
            // 
            this.outTablePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outTablePanel.ColumnCount = 1;
            this.outTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.outTablePanel.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.outTablePanel.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.outTablePanel.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.outTablePanel.Location = new System.Drawing.Point(12, 12);
            this.outTablePanel.Name = "outTablePanel";
            this.outTablePanel.RowCount = 3;
            this.outTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.26263F));
            this.outTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.73737F));
            this.outTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 269F));
            this.outTablePanel.Size = new System.Drawing.Size(1087, 462);
            this.outTablePanel.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.orderIdLabel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.orderStatusProgressBar, 0, 2);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 195);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1081, 264);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // orderIdLabel
            // 
            this.orderIdLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.orderIdLabel.AutoSize = true;
            this.orderIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderIdLabel.Location = new System.Drawing.Point(457, 2);
            this.orderIdLabel.Name = "orderIdLabel";
            this.orderIdLabel.Size = new System.Drawing.Size(166, 37);
            this.orderIdLabel.TabIndex = 0;
            this.orderIdLabel.Text = "Order #----";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 6;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tableLayoutPanel4.Controls.Add(this.orderAmountLabel, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label5, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.defectRateLabel, 4, 1);
            this.tableLayoutPanel4.Controls.Add(this.defectsProducedLabel, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.lampsProducedLabel, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.lampsProducedHereTitleLabel, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.totalLampsProduced, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.contributionToOrderChart, 5, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(4, 44);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1073, 154);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // defectRateLabel
            // 
            this.defectRateLabel.AutoSize = true;
            this.defectRateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defectRateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defectRateLabel.Location = new System.Drawing.Point(737, 77);
            this.defectRateLabel.Margin = new System.Windows.Forms.Padding(0);
            this.defectRateLabel.Name = "defectRateLabel";
            this.defectRateLabel.Size = new System.Drawing.Size(183, 76);
            this.defectRateLabel.TabIndex = 9;
            this.defectRateLabel.Text = "--";
            this.defectRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // defectsProducedLabel
            // 
            this.defectsProducedLabel.AutoSize = true;
            this.defectsProducedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defectsProducedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defectsProducedLabel.Location = new System.Drawing.Point(556, 77);
            this.defectsProducedLabel.Name = "defectsProducedLabel";
            this.defectsProducedLabel.Size = new System.Drawing.Size(177, 76);
            this.defectsProducedLabel.TabIndex = 8;
            this.defectsProducedLabel.Text = "--";
            this.defectsProducedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lampsProducedLabel
            // 
            this.lampsProducedLabel.AutoSize = true;
            this.lampsProducedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lampsProducedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lampsProducedLabel.Location = new System.Drawing.Point(372, 77);
            this.lampsProducedLabel.Name = "lampsProducedLabel";
            this.lampsProducedLabel.Size = new System.Drawing.Size(177, 76);
            this.lampsProducedLabel.TabIndex = 7;
            this.lampsProducedLabel.Text = "--";
            this.lampsProducedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // orderAmountLabel
            // 
            this.orderAmountLabel.AutoSize = true;
            this.orderAmountLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderAmountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderAmountLabel.Location = new System.Drawing.Point(4, 77);
            this.orderAmountLabel.Name = "orderAmountLabel";
            this.orderAmountLabel.Size = new System.Drawing.Size(177, 76);
            this.orderAmountLabel.TabIndex = 6;
            this.orderAmountLabel.Text = "--";
            this.orderAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(774, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 74);
            this.label5.TabIndex = 5;
            this.label5.Text = "Defect Rate";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(567, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 74);
            this.label4.TabIndex = 4;
            this.label4.Text = "Defects Produced";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lampsProducedHereTitleLabel
            // 
            this.lampsProducedHereTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lampsProducedHereTitleLabel.AutoSize = true;
            this.lampsProducedHereTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lampsProducedHereTitleLabel.Location = new System.Drawing.Point(383, 14);
            this.lampsProducedHereTitleLabel.Name = "lampsProducedHereTitleLabel";
            this.lampsProducedHereTitleLabel.Size = new System.Drawing.Size(154, 48);
            this.lampsProducedHereTitleLabel.TabIndex = 3;
            this.lampsProducedHereTitleLabel.Text = "Lamps Produced by Workstation ";
            this.lampsProducedHereTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 74);
            this.label2.TabIndex = 2;
            this.label2.Text = "Order Amount";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // orderStatusProgressBar
            // 
            this.orderStatusProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderStatusProgressBar.Location = new System.Drawing.Point(4, 205);
            this.orderStatusProgressBar.Name = "orderStatusProgressBar";
            this.orderStatusProgressBar.Size = new System.Drawing.Size(1073, 55);
            this.orderStatusProgressBar.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.andonPanelTable, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 53);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.81448F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.18552F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1081, 136);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1073, 27);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sub Assembly Parts Bin Status";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalLampsProduced
            // 
            this.totalLampsProduced.AutoSize = true;
            this.totalLampsProduced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalLampsProduced.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalLampsProduced.Location = new System.Drawing.Point(188, 77);
            this.totalLampsProduced.Name = "totalLampsProduced";
            this.totalLampsProduced.Size = new System.Drawing.Size(177, 76);
            this.totalLampsProduced.TabIndex = 10;
            this.totalLampsProduced.Text = "--";
            this.totalLampsProduced.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label3.Location = new System.Drawing.Point(188, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 75);
            this.label3.TabIndex = 11;
            this.label3.Text = "Total Lamps for Active Order";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contributionToOrderChart
            // 
            this.contributionToOrderChart.BorderSkin.BorderWidth = 3;
            chartArea1.Name = "ChartArea1";
            this.contributionToOrderChart.ChartAreas.Add(chartArea1);
            this.contributionToOrderChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.contributionToOrderChart.Legends.Add(legend1);
            this.contributionToOrderChart.Location = new System.Drawing.Point(921, 1);
            this.contributionToOrderChart.Margin = new System.Windows.Forms.Padding(0);
            this.contributionToOrderChart.Name = "contributionToOrderChart";
            this.tableLayoutPanel4.SetRowSpan(this.contributionToOrderChart, 2);
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.contributionToOrderChart.Series.Add(series1);
            this.contributionToOrderChart.Size = new System.Drawing.Size(151, 152);
            this.contributionToOrderChart.TabIndex = 12;
            this.contributionToOrderChart.Text = "chart2";
            title1.Name = "Defect / Fog Lamp Ratio";
            this.contributionToOrderChart.Titles.Add(title1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 486);
            this.Controls.Add(this.outTablePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Fog Lamp Andon Display";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.outTablePanel.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contributionToOrderChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label runnerSignalLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel andonPanelTable;
        private System.Windows.Forms.TableLayoutPanel outTablePanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label orderIdLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label defectRateLabel;
        private System.Windows.Forms.Label defectsProducedLabel;
        private System.Windows.Forms.Label lampsProducedLabel;
        private System.Windows.Forms.Label orderAmountLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lampsProducedHereTitleLabel;
        private System.Windows.Forms.Label label2;
        private RunnerStationStatusViewer.ColorableProgressBar orderStatusProgressBar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label totalLampsProduced;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart contributionToOrderChart;
    }
}

