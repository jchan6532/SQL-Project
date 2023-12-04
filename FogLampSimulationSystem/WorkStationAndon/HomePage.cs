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

        private Thread _updateDataThread = null;


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
        }

        private void WorkStationAndonForm_Load(object sender, EventArgs e)
        {
            Manager = new DatabaseManager(EmployeeID);

            // employee id
            EmployeeIDTextBlock.Text = Manager.EmployeeID.ToString();
            EmployeeNameTextBlock.Text = Manager.EmployeeName.ToString();
            EmployeeTypeTextBlock.Text = Manager.EmployeeType;

            // order id
            OrderIDTextBlock.DataBindings.Add(
                "Text",
                Manager,
                "CurrentOrderID",
                false,
                DataSourceUpdateMode.OnPropertyChanged
                );

            WorkStationContributionPie.Visible = false;
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

        private void OrdersComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (_updateDataThread?.ThreadState == ThreadState.Running || _updateDataThread?.ThreadState == ThreadState.WaitSleepJoin)
                Stop();

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

            var data = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("Category A", 30),
                new KeyValuePair<string, int>("Category B", 50),
                new KeyValuePair<string, int>("Category C", 20)
            };

            // Set up the Chart
            WorkStationContributionPie.Series[0].ChartType = SeriesChartType.Pie;
            WorkStationContributionPie.Series[0].Points.DataBind(data, "Key", "Value", "");

            // Optional: Add a legend
            if (WorkStationContributionPie.Legends.FindByName("Legend") == null)
            {
                WorkStationContributionPie.Legends.Add("Legend");
                WorkStationContributionPie.Series[0].Legend = "Legend";
            }

            WorkStationContributionPie.Visible = true;

            Start();
        }

        private void UpdatingDataAsync()
        {
            int i = 0;
            while (!_stopUpdating)
            {
                Thread.Sleep(1000);
                Invoke((MethodInvoker)delegate
                {
                    AmountContributedTextBlock.Text = i++.ToString();
                    // Update other UI controls as needed
                });
                //OrderAmountTextBlock.Text = Manager.CurrentOrderAmount.ToString();
                //AmountContributedTextBlock.Text = Manager.CurrentOrderLampsContributed.ToString();
                //DefectsContributedTextBlock.Text = Manager.CurrentOrderLampsDefects.ToString();

                //// right border
                //LampsCreatedTextBlock.Text = Manager.OrderFulfilled.ToString();
                //DefectsTextBlock.Text = Manager.DefectsFulfilled.ToString();

                // 10 seconds update

            }
        }

        public void Stop()
        {
            _stopUpdating = true;
            _updateDataThread.Join();
            MessageBox.Show("closed");
        }

        public void Start()
        {
            ThreadStart updateDataTs = new ThreadStart(UpdatingDataAsync);
            _updateDataThread = new Thread(updateDataTs);
            _updateDataThread.Start();
        }
    }
}
