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
    public partial class WorkStationAndonForm : Form
    {
        public DatabaseManager Manager
        {
            get;
            set;
        }

        public WorkStationAndonForm()
        {
            InitializeComponent();

            Manager = new DatabaseManager();
            
            LampsCreatedTextBlock.DataBindings.Add(
                "Text", 
                Manager, 
                "WorkStationID", 
                false, 
                DataSourceUpdateMode.OnPropertyChanged
                );

            Manager.Start();

        }

        private void LampsCreated_Paint(object sender, PaintEventArgs e)
        {

        }

        private void WorkStationAndonForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Manager.WorkStationID = 100;
        }
    }
}
