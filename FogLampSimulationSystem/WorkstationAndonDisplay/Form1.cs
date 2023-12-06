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
    /// <summary>
    /// The code behind class encapsulating the business logic for the workstation andon display form
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// The ID of the work station to display onto the form
        /// </summary>
        private int WorkstationId;

        /// <summary>
        /// The ID of the order to show on the form
        /// </summary>
        private int OrderId;

        /// <summary>
        /// The SQL connection string
        /// </summary>
        private string ConnectionString;

        private delegate void guiDelegate();

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="workstationId">The workstation ID</param>
        public Form1(int workstationId)
        {
            WorkstationId = workstationId;
            ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            
            InitializeComponent();
        }

        /// <summary>
        /// The event handler whenever the form has loaded
        /// </summary>
        /// <param name="sender">The sender of the event, in this case is the form</param>
        /// <param name="e">The event arguments providing extra information for the event</param>
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

        /// <summary>
        /// Periodically updates the user interface, updates the elements and graphs
        /// </summary>
        private void guiUpdate()
        {
            while (true)
            {
                BeginInvoke(new guiDelegate(UpdateElements));
                BeginInvoke(new guiDelegate(UpdateGraph));
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Updates all necessary UI elements
        /// </summary>
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
                if (lampsBuilt > orderStatusProgressBar.Maximum)
                {
                    lampsBuilt = orderStatusProgressBar.Maximum;
                }
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

        /// <summary>
        /// Updates the chart that represents the workstation's contributionto the order
        /// </summary>
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

            // Because we have all the information we need, we may as well set the total label here.
            

            if (iTable.Rows[0].ItemArray[0] != System.DBNull.Value)
            {
                oParts = Convert.ToInt32(iTable.Rows[0].ItemArray[0]);
            }
            if (iTable.Rows[0].ItemArray[1] != System.DBNull.Value)
            {
                wsParts = Convert.ToInt32(iTable.Rows[0].ItemArray[1]);
            }
            totalLampsProduced.Text = (wsParts + oParts).ToString();
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
