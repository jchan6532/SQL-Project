/*
* FILE : DatabaseManager.cs
* PROJECT : PROG3070 - Gerritt Hooyer
* PROGRAMMER : Gerritt Hooyer
* FIRST VERSION : 2023-11-20
* DESCRIPTION :
* Retrieves and updates data in the ConfigSettings table of the database.
*/

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace FogLampConfigurationTool
{
    internal static class DatabaseManager
    {
        public static DataTable GetConfigData()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            SqlCommand command = new SqlCommand($"SELECT * FROM {ConfigurationManager.AppSettings["ConfigTable"]}",conn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            
            conn.Open();
            adapter.Fill(dt);
            conn.Close();

            return dt;
        }

        public static void UpdateConfigData(DataTable table)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM { ConfigurationManager.AppSettings["ConfigTable"] }",conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            builder.QuotePrefix = "[";
            builder.QuoteSuffix = "]";

            conn.Open();
            adapter.Update(table);
            conn.Close();
        }

    }
}
