using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Text;

namespace WorkstationAndonDisplay
{
    /// <summary>
    /// The database class encapsulating all necessary backend business logic like database queries or actions required for the UI
    /// </summary>
    internal class DatabaseManager
    {
        /// <summary>
        /// The SQL connection string property
        /// </summary>
        private string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// Parameterized constructor, takes in an SQL connection string as a parameter
        /// </summary>
        /// <param name="connectionString">The SQL connection string</param>
        public DatabaseManager(string connectionString)
        {
            ConnectionString = connectionString;
        }
        
        /// <summary>
        /// Gets the order amount for a specific order ID
        /// </summary>
        /// <param name="orderId">The order ID of the order that we are interested in</param>
        /// <returns>The order amount of the order</returns>
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

        /// <summary>
        /// Gets a data table filled with workstation information for a specific workstation ID
        /// </summary>
        /// <param name="workstationId">The ID of the workstation that we are interested in</param>
        /// <returns>A data tabel containing the work station information</returns>
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

        /// <summary>
        /// Gets the ID of the first incomplete order 
        /// </summary>
        /// <returns>The ID of the first incomplete order</returns>
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

        /// <summary>
        /// Gets the part count given the part name and work station ID
        /// </summary>
        /// <param name="partName">The name of the part</param>
        /// <param name="workstationId">The ID of the work station we are interested in</param>
        /// <returns>The part count</returns>
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

        /// <summary>
        /// Gets the minimum threshhold bin amount for a bin before the system should warn the runner that a bin is low in count
        /// </summary>
        /// <returns>The mimnum threshhold bin amount</returns>
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

        /// <summary>
        /// Gets the maximum capacity for the bin given the part name
        /// </summary>
        /// <param name="partName">The part name</param>
        /// <returns>The maximum capacity of the bin</returns>
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

        /// <summary>
        /// Gets the amount of lamps built by all the work stations for a certain order
        /// </summary>
        /// <param name="orderId">The ID of the order we are interestedin</param>
        /// <returns></returns>
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
        /// Gets the current number of lamps built from a workstation for an order
        /// </summary>
        /// <param name="workstationId">The ID of the work station we are finding out about</param>
        /// <param name="orderId">The ID of the order we are interested in</param>
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