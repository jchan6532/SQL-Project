/*
* FILE : DatabaseManager.cs
* PROJECT : PROG3070 - Gerritt Hooyer, Justin Chan
* PROGRAMMER : Gerritt Hooyer, Justin Chan
* FIRST VERSION : 2023-11-20
* DESCRIPTION :
* Retrieves and updates data in the ConfigSettings table of the database.
*/

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;

namespace ConfigurationTool
{
    /// <summary>
    /// The class that encapsulates all the database operations required for the data grid on the configuration tool form
    /// </summary>
    internal static class DatabaseManager
    {
        /// <summary>
        /// Gets current data stored in the configuration table in the SQL server database
        /// </summary>
        /// <returns>A data table encapsulating all the necessary data</returns>
        public static DataTable GetConfigData()
        {
            // Create new data table
            DataTable dt = new DataTable();

            // Create SQL connection
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);

            // Create SQL command
            SqlCommand command = new SqlCommand($"SELECT * FROM {ConfigurationManager.AppSettings["ConfigTable"]}", conn);

            // Create SQL adapter for data manipulation
            SqlDataAdapter adapter = new SqlDataAdapter();

            // Set the adapter's select command
            adapter.SelectCommand = command;

            // Open the connection
            conn.Open();

            // Fill the data table with configuration table data
            adapter.Fill(dt);

            // Close the connection
            conn.Close();

            return dt;
        }

        /// <summary>
        /// Updates the configuration table with the new updated data from the data grid on the configuration tool form
        /// </summary>
        /// <param name="table"></param>
        public static void UpdateConfigData(DataTable table)
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

            foreach (DataRow row in table.AsEnumerable())
            {
                if (row.RowState == DataRowState.Added)
                    InsertIntoDefaultSettings(row);
            }

            // Update the adapter with the data table
            adapter.Update(table);

            // Close the connection
            conn.Close();
        }

        private static void InsertIntoDefaultSettings(DataRow row)
        {
            string configKey = row["config_key"].ToString();
            string configValue = row.ItemArray[1].ToString();

            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"INSERT INTO {ConfigurationManager.AppSettings.Get("DefaultConfigTable")} (config_key, config_value) VALUES ('{configKey}', {configValue})";
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            if (rowsAffected == 0)
                throw new Exception("The insert did not work");
        }

    }
}