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
        public int WorkStationID
        {
            get;
            set;
        } = 0;

        public int EmployeeID
        {
            get;
            set;
        } = 0;

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

        public void GoOnline()
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT Top(1) workstation_id FROM {ConfigurationManager.AppSettings.Get("WorkStationTable")} WHERE is_occupied = 0";
                    WorkStationID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = $"UPDATE Top(1) {ConfigurationManager.AppSettings.Get("WorkStationTable")} SET is_occupied = 1 WHERE is_occupied = 0";
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            if (rowsAffected == 0)
            {
                WorkStationID = 0;
                throw new Exception("No available work station is available at the moment");
            }

            IsOccupied = true;
        }

        public void GoOffline()
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"UPDATE {ConfigurationManager.AppSettings.Get("WorkStationTable")} SET is_occupied = 0 WHERE workstation_id = {WorkStationID}";
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            if (rowsAffected == 0)
            {
                WorkStationID = 0;
                throw new Exception("No available work station is even online at the moment");
            }

            IsOccupied = false;
        }

        public bool IsOnline()
        {
            int isOnline = 0;
            if (WorkStationID == 0)
                return false;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT is_occupied FROM {ConfigurationManager.AppSettings.Get("WorkStationTable")} WHERE workstation_id = {WorkStationID}";

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        isOnline = Convert.ToInt32(result);
                }
            }

            IsOccupied = (isOnline == 1) ? true : false;
            return (isOnline == 1) ? true : false;
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
