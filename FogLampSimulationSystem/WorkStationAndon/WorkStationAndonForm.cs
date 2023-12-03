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
    public partial class WorkStationAndon : Form
    {
        public UserControl PageContent
        {
            get;
            set;
        }

        public DatabaseManager Manager
        {
            get;
            set;
        }

        public WorkStationAndon()
        {
            InitializeComponent();

            // Create new database manager
            Manager = new DatabaseManager();
        }

        private void WorkStationAndon_Load(object sender, EventArgs e)
        {
            // Initially load the log in page
            LoginPage loginPage = new LoginPage(Manager);
            SetPageContent(loginPage);
        }

        private void LoginPage_LoginSuccess(object sender, LoginEventArgs e)
        {
            Manager.WorkStationEmployee = new Employee(e.EmployeeID);
            Manager.IsAuthenticated = true;
            Manager.EmployeeID = e.EmployeeID;

            // Load the home page
            HomePage homePage = new HomePage(Manager);
            SetPageContent(homePage);
        }

        private void SetPageContent(UserControl page)
        {
            Controls.Clear();
            PageContent = page;
            PageContent.Dock = DockStyle.Fill;
            Controls.Add(page);

            if (PageContent is LoginPage loginPage)
                loginPage.LoginSuccess += LoginPage_LoginSuccess;
        }

        private void WorkStationAndon_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PageContent is HomePage homePage)
            {
                homePage.Manager.Stop();
                MessageBox.Show("thread joined");
            }
        }
    }
}
