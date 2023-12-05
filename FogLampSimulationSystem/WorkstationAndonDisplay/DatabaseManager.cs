using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Text;

namespace WorkstationAndonDisplay
{
    internal class DatabaseManager
    {
        private string ConnectionString { get; set; }

        public DatabaseManager(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public int GetOrderAmount(int orderId)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = conn.CreateCommand();
            int result = 0;

            cmd.CommandText = $"SELECT order_amount FROM LampOrder WHERE order_id = {orderId}";

            conn.Open();
            result = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

            return result;
        }

        public DataTable GetWorkstationInfo(int workstationId)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = conn.CreateCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable workstationProductionInfo = new DataTable();

            cmd.CommandText = "SELECT employee_id, employee_name, SessionOverview.order_id, LampOrder.order_amount, " +
                              "SessionOverview.lamps_built, SessionOverview.defects, SessionOverview.defect_rate " +
                              "FROM SessionOverview " +
                              "INNER JOIN LampOrder on LampOrder.order_id = SessionOverview.order_id " +
                              "INNER JOIN WorkstationOverview ON " +
                              "WorkstationOverview.workstation_id = SessionOverview.workstation_id " +
                              $"WHERE {workstationId} = SessionOverview.workstation_id " +
                              $"ORDER BY order_id DESC";
            conn.Open();
            adapter.Fill(workstationProductionInfo);
            conn.Close();

            return workstationProductionInfo;
        }

        public int GetFirstIncompleteOrderId()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = conn.CreateCommand();
            int orderId = -1;
            object result = null;
            cmd.CommandText = "SELECT TOP 1 order_id FROM LampOrder WHERE order_fulfilled < order_amount";
            conn.Open();
            result = cmd.ExecuteScalar();
            conn.Close();
            if (result != null)
            {
                orderId = Convert.ToInt32(result);
            }
            return orderId;
        }

        public int GetPartCount(string partName, int workstationId)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand cmd =
                new SqlCommand(
                    "SELECT part_count FROM BinOverview WHERE part_name = @part_name AND workstation_id = @workstation_id", sqlConnection);
            cmd.Parameters.Add(new SqlParameter("part_name", SqlDbType.NVarChar) { Value = partName });
            cmd.Parameters.Add(new SqlParameter("workstation_id", SqlDbType.Int) { Value = workstationId });
            sqlConnection.Open();
            int count = (int)cmd.ExecuteScalar();
            sqlConnection.Close();
            return count;
        }

        public int GetBinWarningAmount()
        {
            string[] data = new string[2];
            int warning = 0;

            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(
                $"SELECT TOP 1 config_value, config_type FROM ConfigSettings WHERE config_key = '{ConfigurationManager.AppSettings["BinSizeConfigKey"]}'", sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                // Read the value and the type
                data[0] = reader.GetString(0);
                data[1] = reader.GetString(1);
                sqlConnection.Close();

                // Check our data type and parse if it's acceptable
                if (data[1].IndexOf("int", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    warning = Int32.Parse(data[0]);
                }
            }

            return warning;
        }

        public int GetPartBinSize(string partName)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(
                "SELECT TOP 1 bin_size FROM Part WHERE part_name = @part_name", sqlConnection);
            cmd.Parameters.Add(new SqlParameter("part_name", SqlDbType.NVarChar) { Value = partName });

            sqlConnection.Open();
            int binSize = (int)cmd.ExecuteScalar();
            sqlConnection.Close();

            return binSize;
        }

        public int GetWorkstationOrderFulfilled(int orderId)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = conn.CreateCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable ocTable = new DataTable();
            cmd.CommandText = "SELECT SUM(lamps_built) " +
                              $"FROM SessionOverview " +
                              $"GROUP BY order_id " +
                              $"ORDER BY order_id DESC";
            int orderFulfilled = 0;
            conn.Open();
            object result = cmd.ExecuteScalar();
            conn.Close();

            if (result != null)
            {
                orderFulfilled = Convert.ToInt32(result);
            }
            
            return orderFulfilled;
        }

        /// <summary>
        /// Gets the current number 
        /// </summary>
        /// <param name="workstationId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable GetWorkstationOrderContribution(int workstationId, int orderId)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = conn.CreateCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable ocTable = new DataTable();
            cmd.CommandText = "SELECT SUM(lamps_built), " +
                              $"(SELECT lamps_built FROM SessionOverview WHERE workstation_id = {workstationId} AND order_id = {orderId}) " +
                              $"FROM SessionOverview WHERE order_id = {orderId} AND workstation_id != {workstationId}";

            conn.Open();
            adapter.Fill(ocTable);
            conn.Close();

            return ocTable;
        }
    }
}