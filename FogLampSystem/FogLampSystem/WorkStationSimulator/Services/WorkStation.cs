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
        /// <summary>
        /// 
        /// </summary>
        public string WorkStationID
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string EmployeeID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string EmployeeName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string EmployeeType
        {
            get;
            set;
        }

        public float EmployeeBuildSpeed
        {
            get;
            set;
        }

        public float EmployeeDefectRate
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int LampsBuilt
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int DefectCount
        {
            get;
            set;
        } = 0;


        public int FanTickCount
        {
            get;
            set;
        } = 0;

        public int BinRefillTickCount
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 
        /// </summary>
        public bool IsRealTime
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 
        /// </summary>
        public int SimSpeed
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int TicksPerMinute
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int SecondsPerTick
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int RefillIntervalSeconds
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int SimulationSleepInterval
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, int> PartsCount 
        {
            get;
            set;
        }

        public WorkStation()
        {
            PartsCount = GetPartTypesAndCounts();
        }

        public WorkStation(string employeeId)
        {
            var empData = WorkStation.GetEmployeeData(employeeId);
            if (empData.Count == 0)
                throw new Exception("Currently no work station is associated with that employee ID");

            EmployeeID = empData[0];
            WorkStationID = empData[1];
            EmployeeName = empData[2];
            LampsBuilt = Convert.ToInt32(empData[3]);
            DefectCount = Convert.ToInt32(empData[4]);

            PartsCount = GetPartTypesAndCounts();
            EmployeeType = GetEmployeeType(EmployeeID);

            List<string> defaultEmpMetrics = GetEmployeeMetrics(EmployeeType);

            var configMetrics = GetConfigMetrics();

            if (!configMetrics.ContainsKey("system.sim_speed"))
            {
                IsRealTime = true;
            }
            else
            {
                SimSpeed = Convert.ToInt32(configMetrics["system.sim_speed"]);
            }
            RefillIntervalSeconds = Convert.ToInt32(configMetrics["refill_increment"]);
            TicksPerMinute = Convert.ToInt32(configMetrics["system.tickrate"]);
            SecondsPerTick = 60 / TicksPerMinute;
            SimulationSleepInterval = (SecondsPerTick * 1000) / SimSpeed;

            if (!configMetrics.ContainsKey($"employee.{EmployeeType}.defect_rate"))
            {
                EmployeeDefectRate = (float)Convert.ToDouble(defaultEmpMetrics[0]);
            }
            else
            {
                EmployeeDefectRate = (float)Convert.ToDouble(configMetrics[$"employee.{EmployeeType}.defect_rate"]);
            }

            if (!configMetrics.ContainsKey($"employee.{EmployeeType}.build_speed"))
            {
                EmployeeBuildSpeed = (float)Convert.ToDouble(defaultEmpMetrics[1]);
            }
            else
            {
                EmployeeBuildSpeed = (float)Convert.ToDouble(configMetrics[$"employee.{EmployeeType}.build_speed"]);
            }

        }

        public static List<string> GetEmployeeData(string empId)
        {
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
                    cmd.CommandText = $"SELECT {workStationTable}.employee_id, workstation_id, {employeeTable}.employee_name, lamps_built, defects" +
                        $" FROM {workStationTable} JOIN {employeeTable} ON {workStationTable}.employee_id = {employeeTable}.employee_id" +
                        $" WHERE {workStationTable}.employee_id = {empId}";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(reader["employee_id"].ToString());
                            results.Add(reader["workstation_id"].ToString());
                            results.Add(reader["employee_name"].ToString());
                            results.Add(reader["lamps_built"].ToString());
                            results.Add(reader["defects"].ToString());
                        }
                    }

                }
            }
            return results;
        }

        private static Dictionary<string, int> GetPartTypesAndCounts()
        {
            Dictionary<string, int> partsCount = new Dictionary<string, int>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["justin"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT part_name, bin_size FROM {ConfigurationManager.AppSettings.Get("PartTable")}";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string partID = reader["part_name"].ToString();
                            string count = reader["bin_size"].ToString();
                            partsCount.Add(partID, Convert.ToInt32(count));
                        }
                    }

                }
            }
            return partsCount;
        }

        public static string GetEmployeeType(string empId)
        {
            string employeeTypeTable = ConfigurationManager.AppSettings.Get("EmployeeTypeTable");
            string employeeTable = ConfigurationManager.AppSettings.Get("EmployeeTable");

            string employeeType = null;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["justin"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT [type_name] FROM {employeeTypeTable} JOIN {employeeTable} ON [type_id] = employee_type WHERE {employeeTable}.employee_id = {empId}";
                    employeeType = cmd.ExecuteScalar().ToString();

                }
            }
            return employeeType;
        }

        private static Dictionary<string, string> GetConfigMetrics()
        {
            string configTable = ConfigurationManager.AppSettings.Get("ConfigTable");

            Dictionary<string, string> results = new Dictionary<string, string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["justin"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT config_key, config_value FROM {configTable}";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(reader["config_key"].ToString(), reader["config_value"].ToString());
                        }
                    }

                }
            }
            return results;
        }
        private static List<string> GetEmployeeMetrics(string empType)
        {
            string employeeTypeTable = ConfigurationManager.AppSettings.Get("EmployeeTypeTable");

            List<string> results = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["justin"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT defect_rate, build_speed FROM {employeeTypeTable} WHERE type_name = '{empType}'";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(reader["defect_rate"].ToString());
                            results.Add(reader["build_speed"].ToString());
                        }
                    }

                }
            }
            return results;
        }

        public void ProcessWorkStation()
        {
            int simInterval = Convert.ToInt32(ConfigurationManager.AppSettings.Get("SimulationInterval"));

            Console.ForegroundColor = ConsoleColor.Blue;
            while (true)
            {
                Console.WriteLine($"Working Station: ID{WorkStationID}\n");
                foreach (var partIDtoCount in PartsCount)
                {
                    Console.WriteLine($"{partIDtoCount.Key}: {partIDtoCount.Value}");
                }
                Console.WriteLine("Processing ...\n");
                Thread.Sleep(simInterval*1000);
                
                CreateFogLamps();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void CreateFogLamps()
        {

        }

        private void CreatedLampDefected()
        {
            Random random = new Random();

            // Generate a random number between 0 and 100
            int randomNumber = random.Next(0, 101);
        }
    }
}