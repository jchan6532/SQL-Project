/*
* FILE : ConfigToolForm.cs
* PROJECT : PROG3070 - Gerritt Hooyer, Justin Chan
* PROGRAMMER : Gerritt Hooyer, Justin Chan
* FIRST VERSION : 2023-11-20
* DESCRIPTION :
* Adds functionality to the form for the config tool.
*/
using System;
using System.Data;
using System.Windows.Forms;

namespace FogLampConfigurationTool
{
    /// <summary>
    /// The class that encapsulates the form for the configuration tool form
    /// </summary>
    public partial class ConfigToolForm : Form
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ConfigToolForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the configuration tool form on start up
        /// </summary>
        /// <param name="sender">object that invoked the event</param>
        /// <param name="e">event arguments</param>
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

        /// <summary>
        /// Method to call whenever a new update of the SQL server database data is required for the data grid 
        /// </summary>
        private void UpdateDataGridView()
        {
            configTableDataGridView.DataSource = DatabaseManager.GetConfigData(searchTextBox.Text);
        }

        /// <summary>
        /// Refreshes the data grid with the latest data from the SQL server database
        /// </summary>
        /// <param name="sender">object that invoked the event</param>
        /// <param name="e">event arguments</param>
        private void refreshButton_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateDataGridView();
                MessageBox.Show("Configuration table refreshed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Saves the data on the data grid into the SQL server database
        /// </summary>
        /// <param name="sender">object that invoked the event</param>
        /// <param name="e">event arguments</param>
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void deletePrompt(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = MessageBox.Show("Are you sure you want to delete that row?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No;
        }
    }
}
