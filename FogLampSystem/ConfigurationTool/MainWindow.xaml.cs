using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

using ConfigurationTool.ViewModel;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace ConfigurationTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ConnectionString = @"Data Source=DESKTOP-UMCIMBB\SQLEXPRESS;Initial Catalog=SQL-PROJECT;Integrated Security=True";
        public MainWindow()
        {
            InitializeComponent();
            // Load data from the database if needed
            LoadData();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            // Save changes to the database
            SaveData();
        }

        public void LoadData()
        {

            // Create a SQL command
            string sql = "SELECT * FROM ConfigSettings";

            // Create a connection
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                // Create a command
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Create a data adapter
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.SelectCommand = command;

                        // Create a DataTable
                        DataTable configSettingsTable = new DataTable();

                        // Fill the DataTable with data from the database
                        adapter.Fill(configSettingsTable);

                        // Bind the DataTable to the DataGrid's ItemsSource
                        configDataGrid.ItemsSource = configSettingsTable.DefaultView;
                    }
                }
                connection.Close();
            }
        }

        // Implement the SaveChangesToDatabase method
        public void SaveData()
        {
            // Create a connection
            // Create SQL connection
            SqlConnection conn = new SqlConnection(ConnectionString);

            // Create SQL adapter
            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT config_key, int32_value, double_value, string_value FROM ConfigSettings", conn);

            // Create SQL builder
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            // Setting the prefix and suffix for database objects
            builder.QuotePrefix = "[";
            builder.QuoteSuffix = "]";

            adapter.UpdateCommand = builder.GetUpdateCommand(false);
            adapter.DeleteCommand = builder.GetDeleteCommand(false);
            adapter.InsertCommand = builder.GetInsertCommand(false);

            // Open the connection
            conn.Open();

            DataTable table = ((DataView)configDataGrid.ItemsSource).Table;
            //DataTable table = dataView.ToTable();


            // Update the adapter with the data table
            adapter.Update(table);

            // Close the connection
            conn.Close();
        }

    }
}
