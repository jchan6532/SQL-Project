using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkstationSimulator
{
    internal class Workstation
    {
        public int WorkstationId { get; set; }
        // We use a property for our bins to automatically update them on each get.
        public List<Bin> Bins { get {
                List<Bin> bins = new List<Bin>();
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT * FROM BinOverview WHERE workstation_id = {WorkstationId}", sqlConnection);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Instantiate a new bin.
                    Bin bin = new Bin();
                    bin.BinId = reader.GetInt32(0);
                    bin.PartName = reader.GetString(1);
                    bin.Count = reader.GetInt32(2);
                    bin.RefillAmount = reader.GetInt32(3); 
                    // Add the new bin to the bin list
                    bins.Add(bin);
                }

                return bins;
            } 
        }

        public Employee WorkstationEmployee
        {
            get
            {
                Employee employee = null;

                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT TOP 1 employee_id FROM WorkstationOverview WHERE workstation_id = {WorkstationId}", 
                    sqlConnection);

                sqlConnection.Open();
                object response = cmd.ExecuteScalar();
                sqlConnection.Close();

                if (response != null)
                {
                    int employeeId = int.Parse(response.ToString());
                    employee = new Employee(employeeId);
                }

                return employee;
            }
        }

        public int CurrentFanBuildTime
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT config_value FROM ConfigSettings WHERE config_key = 'system.build_time'",
                    sqlConnection);
                int defaultBuildTime = 0;

                sqlConnection.Open();
                object response = cmd.ExecuteScalar();
                sqlConnection.Close();

                if (response != null)
                {
                    defaultBuildTime = int.Parse(response.ToString());
                }

                Random rand = new Random((int)System.DateTime.Now.Ticks);
                double plusMinus = ((rand.NextDouble() * 2 - 1) / 10 * defaultBuildTime);
                int buildTime = (int)((defaultBuildTime + plusMinus) * WorkstationEmployee.BuildSpeedModifier);
                return buildTime;
            }
        }

        public Workstation(int workstationId)
        {
            WorkstationId = workstationId;
        }

        public void RefillBins()
        {
            foreach (Bin bin in Bins)
            {
                SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd = new SqlCommand("RefillBins", sqlConnection);
                cmd.Parameters.Add(new SqlParameter("bin_id", SqlDbType.Int) { Value = bin.BinId });
                cmd.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}
