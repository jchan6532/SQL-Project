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

        /// <summary>
        /// 
        /// </summary>
        public float EmployeeBuildSpeed
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public float EmployeeDefectRate
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int AverageBuildSpeed
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


        public int LampTickCount
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
        public Dictionary<string, List<string>> PartsCount
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

            AverageBuildSpeed = Convert.ToInt32(configMetrics["average_build_time"]);

            if (!configMetrics.ContainsKey("system.sim_speed"))
            {
                // sim is real time
                SimSpeed = -1;
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

        private static Dictionary<string, List<string>> GetPartTypesAndCounts()
        {
            Dictionary<string, List<string>> partsCount = new Dictionary<string, List<string>>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["justin"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT part_id, part_name, bin_size FROM {ConfigurationManager.AppSettings.Get("PartTable")}";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string partID = reader["part_id"].ToString();
                            string partName = reader["part_name"].ToString();
                            string count = reader["bin_size"].ToString();

                            var nameAndCount = new List<string>();
                            nameAndCount.Add(partName);
                            nameAndCount.Add(count);
                            partsCount.Add(partID, nameAndCount);
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
            bool CurrentLampFinished = false;
            Console.ForegroundColor = ConsoleColor.Blue;
            while (true)
            {
                if (CurrentLampFinished)
                {
                    Console.WriteLine($"Working Station: ID{WorkStationID}\n");
                    foreach (var partIDtoCount in PartsCount)
                    {
                        Console.WriteLine($"{partIDtoCount.Key}: {partIDtoCount.Value}");
                    }
                    Console.WriteLine("Processing ...\n");
                    CurrentLampFinished = false;
                }

                // Sleep to simulate time
                if (SimSpeed != -1)
                {
                    Thread.Sleep(SimulationSleepInterval);
                    BinRefillTickCount += (SimulationSleepInterval / 1000) / SecondsPerTick;
                    LampTickCount += (SimulationSleepInterval / 1000) / SecondsPerTick;

                    if (BinRefillTickCount * SecondsPerTick >= RefillIntervalSeconds)
                    {
                        BinRefillTickCount = 0;
                        RefillBin();
                    }

                    if (LampTickCount * SecondsPerTick >= AverageBuildSpeed * EmployeeBuildSpeed)
                    {
                        CreatedFogLamp();
                        CurrentLampFinished = true;
                        LampTickCount = 0;
                    }
                }
            }
        }

        private void RefillBin()
        {
            throw new NotImplementedException();
        }

        private void CreatedFogLamp()
        {
            bool fogLampIsDefect = false;
            Random random = new Random();

            // Generate a random number between 0 (inclusive) and 1 (inclusive)
            int randomNumber = random.Next();
            int defectRatePercentage = (int)(EmployeeDefectRate * 100);


            if (randomNumber > defectRatePercentage)
            {
                fogLampIsDefect = false;
            }
            else
            {
                fogLampIsDefect = true;
                DefectCount++;
            }

            LampsBuilt++;

            // Decrement count
            foreach (var partNameandCount in PartsCount)
            {
                int count = Convert.ToInt32(partNameandCount.Value[1]);
                count = count - 1;
                PartsCount[partNameandCount.Key][1] = count.ToString();
            }

            int rowsAffected = 0;
            // Update new fog lamp into work station table
            using (SqlConnection conn = new SqlConnection())
            {
                string workStationTable = ConfigurationManager.AppSettings.Get("WorkStationTable");
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["justin"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"UPDATE {workStationTable} SET lamps_built = lamps_built + 1 WHERE employee_id = {EmployeeID}";

                    if (fogLampIsDefect)
                        cmd.CommandText = $"UPDATE {workStationTable} SET lamps_built = lamps_built + 1 =, defects = defects + 1 WHERE employee_id = {EmployeeID}";

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            if (rowsAffected == 0)
                throw new Exception("operation was not successful");

            // Update new fog lamp into bin table
            using (SqlConnection conn = new SqlConnection())
            {
                string binTable = ConfigurationManager.AppSettings.Get("BinTable");
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["justin"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"UPDATE {binTable} SET part_count = part_count - 1 WHERE workstation_id = {WorkStationID}";

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            if (rowsAffected == 0)
                throw new Exception("operation was not successful");
        }
    }
}