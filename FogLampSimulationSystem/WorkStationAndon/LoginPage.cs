﻿using System;
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

        public event EventHandler<LoginEventArgs> LoginSuccess;

        public LoginPage()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            int employeeID = -1;
            if (Int32.TryParse(EmployeeIDTextBox.Text, out employeeID))
            {
                bool authenticated = DatabaseManager.AuthenticateID(employeeID);

                if (authenticated)
                {
                    LoginSuccess?.Invoke(this, new LoginEventArgs(employeeID));
                }
                else
                {
                    MessageBox.Show("failed to authenticate");
                }
            }
        }
    }
}