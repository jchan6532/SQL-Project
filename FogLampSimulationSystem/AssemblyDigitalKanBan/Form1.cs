using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AssemblyDigitalKanBan
{
    /// <summary>
    /// Class representing the code behind logic for the assmebly digital kanban form
    /// </summary>
    public partial class Form1 : Form
    {

        private delegate void uiDelegate();

        /// <summary>
        /// The current order ID
        /// </summary>
        private int OrderId
        {
            get;
            set;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler when the form loads
        /// </summary>
        /// <param name="sender">the sender of the event, which is the form</param>
        /// <param name="e">event arguments for extra information</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            DatabaseManager dbManager =
                new DatabaseManager(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            List<int> wIds = dbManager.GetWorkstationIds();

            foreach (int wId in wIds)
            {
                KanBanCell cell = new KanBanCell(wId);
                cell.Dock = DockStyle.Fill;
                tableLayoutPanel2.Controls.Add(cell);
            }
            Task.Run(guiUpdate);
        }

        /// <summary>
        /// Method for periodically updating the user interface for new data to show on the graphs
        /// </summary>
        private void guiUpdate()
        {
            while (true)
            {
                BeginInvoke(new uiDelegate(UpdateContributionGraph));
                BeginInvoke(new uiDelegate(UpdateHistoricalGraph));
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Updates the historical graph representing the lamps built from the beginning of time for each work station
        /// </summary>
        private void UpdateHistoricalGraph()
        {
            DatabaseManager dbManager =
                new DatabaseManager(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            DataTable historicalData = dbManager.GetWorkstationAllTimeLampsProduced();
            historicalPartsProducedGraph.Series.Clear();
            historicalPartsProducedGraph.Titles.Clear();
            historicalPartsProducedGraph.Titles.Add("Total Fog Lamps Built by Workstation");
            historicalPartsProducedGraph.ChartAreas[0].AxisX.LabelStyle.Format = "Workstation {0}";
            string seriesName = $"Total Fog Lamps Built by Workstation";
            Series series = historicalPartsProducedGraph.Series.Add(seriesName);
            
            series.Points.DataBind(historicalData.Rows, "workstation_id", "lamps_built", string.Empty);
            series.IsVisibleInLegend = false;

            int i = 0;
            foreach (DataPoint point in series.Points)
            {
                int r = (i + 2) % 3 == 0 ? 255 : 128;
                int g = (i + 1) % 3 == 0 ? 255 : 128;
                int b = (i + 0) % 3 == 0 ? 255 : 128;
                point.Color = Color.FromArgb(r, g, b);
                i++;
            }
        }

        /// <summary>
        /// Updates the contribution graph representing the lamps built from each work station for the first incomplete order
        /// </summary>
        private void UpdateContributionGraph()
        {
            DatabaseManager dbManager =
                new DatabaseManager(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            List<int> wIds = dbManager.GetWorkstationIds();
            int orderId = dbManager.GetFirstIncompleteOrderId();
            int orderAmount = dbManager.GetOrderAmount(orderId);
            int orderFulfilled = GraphContributionTotal();
            partContributionGraph.Titles.Clear();
            partContributionGraph.Titles.Add("title").Text = $"Total # Of Fog Lamps Built for Order # {orderId}";
            partContributionGraph.Titles[0].Font = new Font(FontFamily.GenericSansSerif, 12);
            partContributionGraph.ChartAreas[0].AxisX.Title = "Time(s)";
            partContributionGraph.ChartAreas[0].AxisY.Title = "# of Fog Lamps Produced";
            foreach (int wId in wIds)
            {
                if (orderId == -1)
                {
                    continue;
                }
                if (orderId != OrderId)
                {
                    partContributionGraph.Series.Clear();
                    OrderId = orderId;
                }

                DataTable orderInfo = dbManager.GetWorkstationOrderContribution(wId, OrderId);
                AddPoint(orderInfo,wId);
            }
            if (partContributionGraph.Series.IsUniqueName("range"))
            {
                partContributionGraph.Series.Add("range");
            }
            Series range = partContributionGraph.Series["range"];
            range.Points.Clear();
            range.IsVisibleInLegend = false;
            range.Color = Color.Transparent;
            range.ChartType = SeriesChartType.Line;
            range.Points.Add(orderAmount - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GraphContributionTotal()
        {
            DatabaseManager dbManager =
                new DatabaseManager(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            List<int> wIds = dbManager.GetWorkstationIds();
            int orderId = dbManager.GetFirstIncompleteOrderId();
            int totalValue = -1;
            if (orderId != OrderId)
            {
                partContributionGraph.Series.Clear();
                OrderId = orderId;
            }
            if (orderId != -1)
            {
                DataTable orderInfo = dbManager.GetWorkstationOrderContribution(wIds[0], OrderId);
                if (partContributionGraph.Series.IsUniqueName("total"))
                {
                    partContributionGraph.Series.Add("total");
                }
                Series total = partContributionGraph.Series["total"];
                total.ChartType = SeriesChartType.Line;
                total.LegendText = $"Total Parts Built for Order #{orderId}";
                total.BorderWidth = 4;
                DataRow row = orderInfo.Rows[0];
                totalValue = Convert.ToInt32(row.ItemArray[1]) + Convert.ToInt32(row.ItemArray[0]);
                if (row.ItemArray[0] != System.DBNull.Value && row.ItemArray[1] != System.DBNull.Value)
                    total.Points.Add(totalValue);

            }
            return totalValue;
        }

        private void AddPoint(DataTable table, int wId)
        {
            if (table.Rows.Count > 0)
            {
                Series series = null;
                string seriesName = $"Workstation {wId}";

                if (partContributionGraph.Series.IsUniqueName(seriesName))
                {
                    partContributionGraph.Series.Add(seriesName);
                    series = partContributionGraph.Series.Last();
                    series.LegendText = $"Workstation {wId}";
                    series.ChartType = SeriesChartType.Line;

                }
                else
                {
                    series = partContributionGraph.Series[seriesName];
                }
                if (series != null)
                {
                    DataRow row = table.Rows[0];
                    series.BorderWidth = 4;
                    if (row.ItemArray[1] != System.DBNull.Value) 
                        series.Points.Add(Convert.ToInt32(row.ItemArray[1]));
                }
            }
        }



    }
}
