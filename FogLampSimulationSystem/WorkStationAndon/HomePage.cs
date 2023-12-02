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

        public HomePage(DatabaseManager manager)
        {
            InitializeComponent();

            Manager = manager;

            LampsCreatedTextBlock.DataBindings.Add(
                "Text",
                Manager,
                "LampsCreated",
                false,
                DataSourceUpdateMode.OnPropertyChanged
                );

            Manager.Start();
        }

        private void WorkStationAndonForm_Load(object sender, EventArgs e)
        {

        }

        private void WorkStationAndonForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.Stop();
            MessageBox.Show("closed");
        }
    }
}
