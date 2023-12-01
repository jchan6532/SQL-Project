using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AssemblyDigitalKanBan
{
    public partial class KanBanCell : UserControl
    {
        private int WorkstationId;
        private int OrderId;

        private delegate void guiDelegate();

        public KanBanCell(int workstationId)
        {
            WorkstationId = workstationId;
            InitializeComponent();
        }

        private void KanBanCell_Load(object sender, EventArgs e)
        {
            Task.Run(Update);
        }

        private void Update()
        {
            while (true)
            {
                BeginInvoke(new guiDelegate(SetLabelText));
                BeginInvoke(new guiDelegate(SetPieChart));
                Thread.Sleep(1000);
            }
        }

        private void SetLabelText()
        {
            DatabaseManager dbManager =
                new DatabaseManager(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            DataTable iTable = dbManager.GetWorkstationInfo(WorkstationId);

            if (iTable.Rows.Count > 0)
            {
                DataRow row = iTable.Rows[0];
                OrderId = Convert.ToInt32(row["order_id"]);
                workstationInfoLabel.Text = $"Workstation {WorkstationId} / Employee : [ ID : {row["employee_id"]} | Name : {row["employee_name"]} ]";

                amountLabel.Text = row["order_amount"].ToString();
                producedLabel.Text = row["lamps_built"].ToString();
                defectLabel.Text = row["defects"].ToString();
                
                double defectRate = Convert.ToDouble(row["defect_rate"]);
                Color color = defectRate > 0.5 ? Color.LightCoral : Color.LightGreen;
                defectRateLabel.BackColor = color;
                defectRateLabel.Text = defectRate.ToString("##.###");
            }

            
        }

        private void SetPieChart()
        {
            DatabaseManager dbManager =
                new DatabaseManager(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            DataTable iTable = dbManager.GetWorkstationOrderContribution(WorkstationId,OrderId);
            contributionToOrderChart.Series.Clear();
            contributionToOrderChart.Series.Add("series");
            Series series = contributionToOrderChart.Series["series"];
            contributionToOrderChart.Titles.Clear();
            contributionToOrderChart.Titles.Add($"Contribution Rate Towards Order {OrderId}");
            contributionToOrderChart.Legends.Last().IsTextAutoFit = true;
            contributionToOrderChart.Legends.Last().TextWrapThreshold = 5;

            int wsParts = 0;
            int oParts = 0;
            int oTotal = dbManager.GetOrderAmount(OrderId);
            if (iTable.Rows[0].ItemArray[0] != System.DBNull.Value)
            {
                oParts = Convert.ToInt32(iTable.Rows[0].ItemArray[0]);
            }
            if (iTable.Rows[0].ItemArray[1] != System.DBNull.Value)
            {
                wsParts = Convert.ToInt32(iTable.Rows[0].ItemArray[1]);
            }
            oTotal = oTotal - (wsParts + oParts);
            series.ChartType = SeriesChartType.Pie;
            series.Points.Add((int)oParts);
            series.Points.Last().LegendText = $"Parts Produced Everyone Else";
            series.Points.Last().Label = oParts.ToString();
            series.Points.Last().Color = Color.GreenYellow;
            series.Points.Add((int)wsParts);
            series.Points.Last().LegendText = $"Parts Produced @ WS{WorkstationId}";
            series.Points.Last().Label = wsParts.ToString();
            series.Points.Last().Color = Color.Green;
            series.Points.Add((int)oTotal);
            series.Points.Last().LegendText = $"Order Remainder";
            series.Points.Last().Label = oTotal.ToString();
            series.Points.Last().Color = Color.Red;
        }
    }
}
