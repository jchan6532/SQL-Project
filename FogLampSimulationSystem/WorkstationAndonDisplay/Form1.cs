using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WorkstationAndonDisplay
{
    public partial class Form1 : Form
    {
        private int WorkstationId;
        private int OrderId;
        private string ConnectionString;

        private delegate void guiDelegate();

        public Form1(int workstationId)
        {
            WorkstationId = workstationId;
            ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(guiUpdate);
            andonPanelTable.Controls.Add(new AndonPanel("Harness", WorkstationId));
            andonPanelTable.Controls.Add(new AndonPanel("Reflector", WorkstationId));
            andonPanelTable.Controls.Add(new AndonPanel("Housing", WorkstationId));
            andonPanelTable.Controls.Add(new AndonPanel("Lens", WorkstationId));
            andonPanelTable.Controls.Add(new AndonPanel("Bulb", WorkstationId));
            andonPanelTable.Controls.Add(new AndonPanel("Bezel", WorkstationId));

            foreach (Control control in andonPanelTable.Controls)
            {
                control.Anchor = AnchorStyles.None;
            }

            lampsProducedHereTitleLabel.Text += WorkstationId.ToString();
        }


        private void guiUpdate()
        {
            while (true)
            {
                BeginInvoke(new guiDelegate(UpdateElements));
                BeginInvoke(new guiDelegate(UpdateGraph));
                Thread.Sleep(1000);
            }
        }

        private void UpdateElements()
        {
            DatabaseManager dbManager = new DatabaseManager(ConnectionString);
            DataTable orderInfo = dbManager.GetWorkstationInfo(WorkstationId);

            if (orderInfo.Rows.Count != 0)
            {
                DataRow row = orderInfo.Rows[0];
                OrderId = Convert.ToInt32(row["order_id"]);

                double defectRate = Convert.ToDouble(row["defect_rate"]);
                int lampsBuilt = dbManager.GetWorkstationOrderFulfilled(OrderId);
                int refillAmount = dbManager.GetBinWarningAmount();

                int orderAmount = Convert.ToInt32(row["order_amount"]);



                orderIdLabel.Text = OrderId != -1 ? $"Order #{OrderId}" : "NO OPEN ORDERS";
                orderAmountLabel.Text = orderAmount.ToString();
                defectsProducedLabel.Text = row["defects"].ToString();
                lampsProducedLabel.Text = row["lamps_built"].ToString();
                defectRateLabel.Text = defectRate.ToString("#.###");
                
                Color color = defectRate > 0.5 ? Color.LightCoral : Color.LightGreen;
                defectRateLabel.BackColor = color;
                orderStatusProgressBar.Maximum = Convert.ToInt32(row["order_amount"]);
                orderStatusProgressBar.Value = lampsBuilt;

                runnerSignalLabel.Text = $"Workstation {WorkstationId}";
                runnerSignalLabel.ForeColor = Color.Black;
                runnerSignalLabel.UseCompatibleTextRendering = true;
                bool refill = false;
                string partNames = String.Empty;
                foreach (AndonPanel panel in andonPanelTable.Controls)
                {
                    if (panel.Amount <= panel.RefillAmount)
                    {
                        refill = true;
                        partNames += $" {panel.PartName}, ";
                        runnerSignalLabel.ForeColor = Color.Red;
                    }
                }
                if (refill == true)
                {
                    runnerSignalLabel.Text += $" : [ " + partNames + " ]";
                }  
            }
        }

        private void UpdateGraph()
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
            series.IsVisibleInLegend = false;
            series.Points.Add((int)oParts);
            series.Points.Last().Label = oParts.ToString();
            series.Points.Last().Color = Color.GreenYellow;
            series.Points.Add((int)wsParts);
            series.Points.Last().Label = wsParts.ToString();
            series.Points.Last().Color = Color.Green;
            series.Points.Add((int)oTotal);
            series.Points.Last().Label = oTotal.ToString();
            series.Points.Last().Color = Color.Red;
        }

    }
}
