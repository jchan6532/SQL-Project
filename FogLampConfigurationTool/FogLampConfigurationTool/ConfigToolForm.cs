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
            try
            {
                UpdateDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDataGridView()
        {
            configTableDataGridView.DataSource = DatabaseManager.GetConfigData();
        }


        private void refreshButton_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateDataGridView();
                MessageBox.Show("Configuration table refreshed.","Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseManager.UpdateConfigData((DataTable)configTableDataGridView.DataSource);
                UpdateDataGridView();
                MessageBox.Show("Configuration saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
