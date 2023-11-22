using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

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

        public WorkStation()
        {
            bool isAvailable = WorkStation.CheckAvailability();
            if (!isAvailable)
                throw new Exception("No working station are available at the moment");
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

        public void GoOnline()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT TOP(1) workstation_id FROM {ConfigurationManager.AppSettings.Get("WorkStationTable")} WHERE is_occupied = 0";
                    WorkStationID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = $"UPDATE {ConfigurationManager.AppSettings.Get("WorkStationTable")} SET is_occupied = 1 WHERE workstation_id = {WorkStationID}";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void GoOffline()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"UPDATE {ConfigurationManager.AppSettings.Get("WorkStationTable")} SET is_occupied = 0 WHERE workstation_id = {WorkStationID}";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool IsOnline()
        {
            int isOnline = 0;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT is_occupied FROM {ConfigurationManager.AppSettings.Get("WorkStationTable")} WHERE workstation_id = {WorkStationID}";
                    isOnline = (int)cmd.ExecuteScalar();
                }
            }

            return (isOnline == 1) ? true : false;
        }
    }
}
