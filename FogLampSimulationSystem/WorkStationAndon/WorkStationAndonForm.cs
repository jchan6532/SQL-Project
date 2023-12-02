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
            PageContent = new LoginPage();
            PageContent.Dock = DockStyle.Fill;
            Controls.Add(PageContent);

            if (PageContent is LoginPage loginPage)
                loginPage.LoginSuccess += LoginPage_LoginSuccess;
        }

        private void LoginPage_LoginSuccess(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage(2);
            SetPageContent(homePage);
        }

        private void SetPageContent(UserControl page)
        {
            Controls.Clear();
            PageContent = page;
            Controls.Add(page);
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
