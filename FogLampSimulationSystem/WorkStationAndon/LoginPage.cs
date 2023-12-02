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

        public LoginPage()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            LoginSuccess?.Invoke(this, EventArgs.Empty);
        }
    }
}
