using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkStationAndon
{
    public partial class HomePage : UserControl
    {
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
            Manager = new DatabaseManager(EmployeeID, this);

            // Lamps created
            LampsCreatedTextBlock.DataBindings.Add(
                "Text",
                Manager,
                "LampsCreated",
                false,
                DataSourceUpdateMode.OnPropertyChanged
                );

            // employee id
            EmployeeIDTextBlock.DataBindings.Add(
                "Text",
                Manager,
                "EmployeeID",
                false,
                DataSourceUpdateMode.OnPropertyChanged
                );

            // employee name
            EmployeeNameTextBlock.DataBindings.Add(
                "Text",
                Manager,
                "EmployeeName",
                false,
                DataSourceUpdateMode.OnPropertyChanged
                );

            // employee type
            EmployeeTypeTextBlock.DataBindings.Add(
                "Text",
                Manager,
                "EmployeeType",
                false,
                DataSourceUpdateMode.OnPropertyChanged
                );

            // order id
            OrderIDTextBlock.DataBindings.Add(
                "Text",
                Manager,
                "CurrentOrderID",
                false,
                DataSourceUpdateMode.OnPropertyChanged
                );
        }

        private void WorkStationAndonForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.Stop();
            MessageBox.Show("closed");
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
            ComboBox comboBox = (ComboBox)sender;

            string selectedOrder = comboBox.SelectedItem.ToString();
            string[] orderComponents = selectedOrder.Split(' ');
            Manager.CurrentOrderID = Int32.Parse(orderComponents[1]);
            Manager.CurrentOrder = new Order(Int32.Parse(orderComponents[1]));

            OrderAmountTextBlock.Text = Manager.CurrentOrderAmount.ToString();
            AmountContributedTextBlock.Text = Manager.CurrentOrderLampsContributed.ToString();
            DefectsContributedTextBlock.Text = Manager.CurrentOrderLampsDefects.ToString();
            
            Manager.Start();
        }
    }
}
