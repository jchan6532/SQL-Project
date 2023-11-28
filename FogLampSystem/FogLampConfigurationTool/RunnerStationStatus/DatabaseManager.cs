using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnerStationStatus
{
    internal class DatabaseManager
    {
        public string ConnectionString { get; set; }

        public DatabaseManager(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public Dictionary<int, string> GetWorkstationNames()
        {
            Dictionary<int, string> workstations = new Dictionary<int, string>();
            DataTable workstationTable = new DataTable();

            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT workstation_id, employee_id, employee_name FROM WorkstationOverview",sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            sqlConnection.Open();
            adapter.Fill(workstationTable);
            sqlConnection.Close();

            foreach (DataRow row in workstationTable.Rows)
            {
                string workstationName =
                    $"Workstation {row.ItemArray[0].ToString()} ( ID : {row.ItemArray[1]} | {row.ItemArray[2]} )";
                workstations.Add((int)row.ItemArray[0],workstationName);
            }

            return workstations;
        }

        public int GetPartCount(string partName, int workstationId)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand cmd =
                new SqlCommand(
                    "SELECT part_count FROM WorkstationOverview WHERE part_name = @part_name AND workstation_id = @workstation_id",sqlConnection);
            cmd.Parameters.Add(new SqlParameter("part_name", SqlDbType.NVarChar) { Value = partName });
            cmd.Parameters.Add(new SqlParameter("workstation_id", SqlDbType.Int) { Value = workstationId});
            sqlConnection.Open();
            int count = (int)cmd.ExecuteScalar();
            sqlConnection.Close();
            return count;
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

        public int GetBinWarningAmount()
        {
            string[] data = new string[2];
            int warning = 0;

            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(
                "SELECT TOP 1 config_value, config_type FROM ConfigSettings WHERE config_key = 'simulation.runner.bin_warning_size'",sqlConnection);
            
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

        public bool GetWarningMessage(out string message)
        {
            List<int> workstationIds = GetWorkstationIds();
            bool result = false;
            message = string.Empty;
            foreach (int id in workstationIds)
            {
                result = CheckForWarning(id, out message);
                if (result)
                {
                    break;
                }
            }
            return result;
        }

        private bool CheckForWarning(int id, out string message)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM WorkstationOverview WHERE workstation_id = @workstation_id",sqlConnection);
            cmd.Parameters.Add(new SqlParameter("workstation_id", SqlDbType.Int) { Value = id });
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataTable data = new DataTable();

            sqlConnection.Open();
            adapter.Fill(data);
            sqlConnection.Close();
            bool warning = false;
            message = string.Empty;
            foreach (DataRow row in data.Rows)
            {
                if ((int)row["part_count"] <= 5)
                {
                    message = $"Workstation {row["workstation_id"]} needs a {row["part_name"]} refill!";
                    warning = true;
                    break;
                }
            }
            return warning;
        }

        private List<int> GetWorkstationIds()
        {
            List<int> workstationIds = new List<int>();
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT workstation_id FROM Workstation", sqlConnection);
            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            { 
                workstationIds.Add(reader.GetInt32(0));
            }

            return workstationIds;
        }
    }
}
