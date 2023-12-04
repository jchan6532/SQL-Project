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

        public HomePage(int employeeID)
        {
            InitializeComponent();

            Manager = new DatabaseManager(employeeID);

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
        }

        private void WorkStationAndonForm_Load(object sender, EventArgs e)
        {
            Manager.Start();
        }

        private void WorkStationAndonForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.Stop();
            MessageBox.Show("closed");
        }
    }
}
