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
    public partial class WorkStationAndon : Form
    {
        public UserControl PageContent
        {
            get;
            set;
        }

        public WorkStationAndon()
        {
            InitializeComponent();

        }

        private void WorkStationAndon_Load(object sender, EventArgs e)
        {
            // Initially load the log in page
            LoginPage loginPage = new LoginPage();
            SetPageContent(loginPage);
        }

        private void LoginPage_LoginSuccess(object sender, LoginEventArgs e)
        {

            // Load the home page
            HomePage homePage = new HomePage(e.EmployeeID);
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
