using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WorkStationAndon
{
    public partial class HomePage : UserControl
    {
        private volatile bool _stopUpdating = false;

        private volatile Thread _updateDataThread = null;


        public DatabaseManager Manager
        {
            get;
            set;
        }

        public int EmployeeID { get; set; }

        public HomePage(int employeeID)
        {
            InitializeComponent();
            EmployeeID = employeeID;

            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void WorkStationAndonForm_Load(object sender, EventArgs e)
        {
            Manager = new DatabaseManager(EmployeeID);

            // employee id
            EmployeeIDTextBlock.Text = Manager.EmployeeID.ToString();
            EmployeeNameTextBlock.Text = Manager.EmployeeName.ToString();
            EmployeeTypeTextBlock.Text = Manager.EmployeeType;

            CreatePArtsTable();
            PopulateParts();

            // order id
            OrderIDTextBlock.DataBindings.Add(
                "Text",
                Manager,
                "CurrentOrderID",
                false,
                DataSourceUpdateMode.OnPropertyChanged
                );

            WorkStationContributionPie.Visible = false;

            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders; // Set to AllCellsExceptHeaders

        }

        private void WorkStationAndonForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void OrdersComboBox_DropDown(object sender, EventArgs e)
        {
            var orderIDs = DatabaseManager.GetOrderIDs(Manager.WorkStationID);
            OrdersComboBox.Invoke(new Action(() =>
            {
                // Clear existing items
                OrdersComboBox.Items.Clear();

                // Add the new items
                OrdersComboBox.Items.AddRange(orderIDs.ToArray());
            }));
        }

        private async void OrdersComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedOrder = comboBox.SelectedItem.ToString();
            string[] orderComponents = selectedOrder.Split(' ');

            // top left corner
            Manager.CurrentOrderID = Int32.Parse(orderComponents[1]);
            Manager.CurrentOrder = new Order(Int32.Parse(orderComponents[1]));

            OrderAmountTextBlock.Text = Manager.CurrentOrderAmount.ToString();
            AmountContributedTextBlock.Text = Manager.CurrentOrderLampsContributed.ToString();
            DefectsContributedTextBlock.Text = Manager.CurrentOrderLampsDefects.ToString();

            // right border
            LampsCreatedTextBlock.Text = Manager.OrderFulfilled.ToString();
            DefectsTextBlock.Text = Manager.DefectsFulfilled.ToString();

            CreatePieChart();


            await StartAsync();
        }

        private void UpdatingDataAsync()
        {
            int i = 0;
            while (!_stopUpdating)
            {
                Thread.Sleep(1000);
                Invoke((MethodInvoker)delegate
                {
                    //AmountContributedTextBlock.Text = i++.ToString();
                    // Update other UI controls as needed

                    OrderAmountTextBlock.Text = Manager.CurrentOrderAmount.ToString();
                    AmountContributedTextBlock.Text = Manager.CurrentOrderLampsContributed.ToString();
                    DefectsContributedTextBlock.Text = Manager.CurrentOrderLampsDefects.ToString();

                    // right border
                    LampsCreatedTextBlock.Text = Manager.OrderFulfilled.ToString();
                    DefectsTextBlock.Text = Manager.DefectsFulfilled.ToString();

                    CreatePieChart();
                    PopulateParts();

                    // 10 seconds update
                });


            }
        }

        public async Task Stop()
        {
            if (_updateDataThread != null)
            {
                _stopUpdating = true;
                await Task.Run(() => _updateDataThread.Join());
                _updateDataThread = null;
            }
        }

        public async Task StartAsync()
        {
            if (_updateDataThread != null)
            {
                _stopUpdating = true;
                await Task.Run(() => _updateDataThread.Join());
                _updateDataThread = null;
            }
            _stopUpdating = false;
            _updateDataThread = new Thread(UpdatingDataAsync);
            _updateDataThread.Start();
        }

        private void CreatePieChart()
        {
            var data = DatabaseManager.GetOrdersReport(Manager.CurrentOrderID).ToDictionary(kvp => $"Workstation - {kvp.Key}", kvp => kvp.Value);

            int sum = 0;
            foreach (var workstation in data)
            {
                sum += workstation.Value;
            }
            if (Manager.CurrentOrderAmount >= sum)
            {
                data.Add("Uncomplete", Manager.CurrentOrderAmount - sum);
            }


            // Set up the Chart
            WorkStationContributionPie.Series[0].ChartType = SeriesChartType.Pie;
            WorkStationContributionPie.Series[0].Points.DataBind(data, "Key", "Value", "");

            // Display values on the pie chart slices
            foreach (var point in WorkStationContributionPie.Series[0].Points)
            {
                // Set the label to display the value
                point.Label = $"{point.YValues[0]:0}";

                point.LegendText = point.AxisLabel;
            }

            // Optional: Add a legend
            if (WorkStationContributionPie.Legends.FindByName("Legend") == null)
            {
                WorkStationContributionPie.Legends.Add("Legend");
                WorkStationContributionPie.Series[0].Legend = "Legend";
            }

            if (data.Count == 0)
            {
                WorkStationContributionPie.Visible = false;
            }
            else
            {
                WorkStationContributionPie.Visible = true;
            }
        }

        private void CreatePArtsTable()
        {
            DataTable dataTable = new DataTable();

            // Add columns to DataTable
            dataTable.Columns.Add("part name", typeof(string));
            dataTable.Columns.Add("count", typeof(int));

            // Set the DataTable as the DataSource
            dataGridView.DataSource = dataTable;
        }

        private void PopulateParts()
        {
            var parts = DatabaseManager.GetWorkStationPartCounts(Manager.WorkStationID);

            // Access the DataTable from the DataGridView DataSource
            DataTable dataTable = (DataTable)dataGridView.DataSource;

            // Clear existing data from the DataTable
            dataTable.Rows.Clear();

            // Add data from the dictionary to the DataTable
            foreach (var kvp in parts)
            {
                dataTable.Rows.Add(kvp.Key, kvp.Value);
            }

        }
    }
}
