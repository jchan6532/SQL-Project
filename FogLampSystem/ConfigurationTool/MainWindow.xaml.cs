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

using System.Data;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Configuration;

namespace ConfigurationTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Load data from the database if needed
            LoadData();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = ((DataView)configDataGrid.ItemsSource).ToTable();
            DataTable dataTableToFixException =((DataView)configDataGrid.ItemsSource).Table;
            // Save changes to the database
            SaveData(dataTableToFixException);
        }

        // Implement the SaveChangesToDatabase method
        public void SaveData(DataTable table)
        {
            // Create SQL connection
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);

            // Create SQL adapter
            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM { ConfigurationManager.AppSettings["ConfigTable"] }", conn);

            // Create SQL builder
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            // Setting the prefix and suffix for database objects
            builder.QuotePrefix = "[";
            builder.QuoteSuffix = "]";

            // Open the connection
            conn.Open();

            // Update the adapter with the data table
            adapter.Update(table);

            // Close the connection
            conn.Close();
        }

        public void LoadData()
        {

            // Create a SQL command
            string sql = "SELECT * FROM ConfigSettings";

            // Create a connection
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
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

    }
}
