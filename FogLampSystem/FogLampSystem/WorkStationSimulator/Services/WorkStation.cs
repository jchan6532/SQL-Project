using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

using WorkStationSimulator.Constants;

namespace WorkStationSimulator.Services
{
    public class WorkStation
    {
        public string WorkStationID
        {
            get;
            set;
        }

        public string EmployeeID
        {
            get;
            set;
        }

        public string EmployeeName
        {
            get;
            set;
        }

        public EmployeeType EmployeeType
        {
            get;
            set;
        } = EmployeeType.Invalid;

        public int FansBuilt
        {
            get;
            set;
        } = 0;

        public int DefectCount
        {
            get;
            set;
        } = 0;

        public bool IsOccupied
        {
            get;
            set;
        } = false;

        public Dictionary<string, int> PartsCount { get; set; }

        public WorkStation()
        {
            bool isAvailable = WorkStation.CheckAvailability();
            if (!isAvailable)
                throw new Exception("No working station are available at the moment");

            PartsCount = GetPartTypesAndCounts();
        }

        public WorkStation(string employeeId)
        {
            var results = WorkStation.GetEmployeeData(employeeId);
            if (results.Count == 0)
                throw new Exception("Currently no work station is associated with that employee ID");

            EmployeeID = results[0];
            WorkStationID = results[1];
            EmployeeName = results[2];
        }

        public static List<string> GetEmployeeData(string empId)
        {
            bool isValidEmployee = false;
            string workStationTable = ConfigurationManager.AppSettings.Get("WorkStationTable");
            string employeeTable = ConfigurationManager.AppSettings.Get("EmployeeTable");

            List<string> results = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["justin"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT {workStationTable}.employee_id, workstation_id, {employeeTable}.employee_name" +
                        $" FROM {workStationTable} JOIN {employeeTable} ON {workStationTable}.employee_id = {employeeTable}.employee_id" +
                        $" WHERE {workStationTable}.employee_id = {empId}";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(reader["employee_id"].ToString());
                            results.Add(reader["workstation_id"].ToString());
                            results.Add(reader["employee_name"].ToString());
                        }
                    }

                }
            }
            return results;
        }

        private static bool CheckAvailability()
        {
            bool stationAvailable = false;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT COUNT(*) FROM {ConfigurationManager.AppSettings.Get("WorkStationTable")} WHERE is_occupied = 0";
                    int numEmptyStation = (int)cmd.ExecuteScalar();
                    if (numEmptyStation > 0)
                        stationAvailable = true;

                }
            }

            return stationAvailable;
        }

        private static Dictionary<string, int> GetPartTypesAndCounts()
        {
            Dictionary<string, int> partsCount = new Dictionary<string, int>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT part_id, config_value FROM {ConfigurationManager.AppSettings.Get("PartTable")} JOIN " +
                        $"{ConfigurationManager.AppSettings.Get("DefaultConfigTable")} ON part_id = config_key";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string partID = reader.GetValue(0).ToString();
                            string count = reader.GetValue(1).ToString();
                            partsCount.Add(partID, Convert.ToInt32(count));
                        }
                    }

                }
            }
            return partsCount;
        }

        public static List<string> GetEmployeeTypes()
        {
            List<string> employeeTypes = new List<string>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT employee_type FROM {ConfigurationManager.AppSettings.Get("EmployeeTypesTable")}";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeeTypes.Add(reader.GetValue(0).ToString());
                        }
                    }
                }
            }

            return employeeTypes;
        }

        public void ProcessWorkStation()
        {
            if (!IsOccupied)
                throw new Exception("Work station is not even online yet");

            Console.ForegroundColor = ConsoleColor.Blue;
            while (IsOccupied)
            {
                Console.WriteLine($"Working Station: ID{WorkStationID}\n");
                Console.WriteLine($"Working Station: ID{EmployeeType}\n");
                foreach (var partIDtoCount in PartsCount)
                {
                    Console.WriteLine($"{partIDtoCount.Key}: {partIDtoCount.Value}");
                }
                Console.WriteLine("Processing ...\n");
                Thread.Sleep(10000);
                CreateFogLamps();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void CreateFogLamps()
        {

        }
    }
}