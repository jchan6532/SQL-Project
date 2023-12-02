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
    public partial class LoginPage : UserControl
    {

        public event EventHandler LoginSuccess;

        public DatabaseManager Manager { get; set; }

        public LoginPage(DatabaseManager manager)
        {
            InitializeComponent();
            Manager = manager;
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            int employeeID = -1;
            if (Int32.TryParse(WorkStationIDTextBox.Text, out employeeID))
            {
                Manager.WorkStationEmployee = employeeID;

                LoginSuccess?.Invoke(this, EventArgs.Empty);


            }
        }
    }
}
