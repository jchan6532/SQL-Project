/*
* FILE : ConfigToolForm.cs
* PROJECT : PROG3070 - Gerritt Hooyer
* PROGRAMMER : Gerritt Hooyer
* FIRST VERSION : 2023-11-20
* DESCRIPTION :
* Adds functionality to the form for the config tool.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FogLampConfigurationTool
{
    public partial class ConfigToolForm : Form
    {
        public ConfigToolForm()
        {
            InitializeComponent();
        }

        private void ConfigToolForm_Load(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void UpdateDataGridView()
        {
            configTableDataGridView.DataSource = DatabaseManager.GetConfigData();
        }


        private void refreshButton_Click(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DatabaseManager.UpdateConfigData((DataTable)configTableDataGridView.DataSource);
            UpdateDataGridView();
        }
    }
}
