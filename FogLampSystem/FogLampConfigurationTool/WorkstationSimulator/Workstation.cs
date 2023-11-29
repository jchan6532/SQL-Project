using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
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
                sqlConnection.Close();
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

        public bool HasEnoughParts
        {
            get
            {
                bool hasParts = true;
                foreach (Bin bin in Bins)
                {
                    if (bin.Count == 0)
                    {
                        hasParts = false; 
                        break;
                    }
                }
                return hasParts;
            }
        }

        public OrderSession CurrentOrderSession
        {
            get
            {
                if (!OrderSessionExists)
                {
                    CreateSession();
                }

                OrderSession session = null;
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT * FROM WorkstationSession " +
                                                $"WHERE workstation_id = {WorkstationId} AND order_id = {CurrentOrder.OrderId}",
                    sqlConnection);
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int lampsBuilt = Convert.ToInt32(reader["lamps_built"]);
                    int defects = Convert.ToInt32(reader["defects"]);
                    session = new OrderSession(WorkstationId, CurrentOrder.OrderId, lampsBuilt, defects);
                }
                sqlConnection.Close();
                return session;
            }
        }

        

        private bool OrderSessionExists
        {
            get
            {
                bool sessionExists = false;
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT workstation_id FROM WorkstationSession " +
                                                $"WHERE workstation_id = {WorkstationId} AND order_id = {CurrentOrder.OrderId}",
                    sqlConnection);
                object result;
                sqlConnection.Open();
                result = cmd.ExecuteScalar();
                sqlConnection.Close();
                if (result != null)
                {
                    sessionExists = true;
                }

                return sessionExists;
            }
        }

        public Order CurrentOrder
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT TOP 1 order_id FROM LampOrder WHERE order_amount > order_fulfilled",
                    sqlConnection);
                Order order = null;
                int orderId = 0;

                sqlConnection.Open();
                object response = cmd.ExecuteScalar();
                sqlConnection.Close();

                if (response != null)
                {

                    Int32.TryParse(response.ToString(), out orderId);
                    order = new Order(orderId);
                }

                return order;
            }
        }


        private int RefillWarningAmount
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT config_value FROM ConfigSettings WHERE config_key = 'system.refill_warning_amount'",
                    sqlConnection);
                
                sqlConnection.Open();
                object response = cmd.ExecuteScalar();
                sqlConnection.Close();

                if (!Int32.TryParse(response.ToString(), out int result))
                {
                    result = 0;
                }

                return result;
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
                if (bin.Count < RefillWarningAmount)
                {
                    SqlConnection sqlConnection =
                        new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("RefillBin", sqlConnection);
                    cmd.Parameters.Add(new SqlParameter("bin_id", SqlDbType.Int) { Value = bin.BinId });
                    cmd.CommandType = CommandType.StoredProcedure;

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void BuildLamp()
        {
            SqlConnection sqlConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            SqlCommand cmd = new SqlCommand("BuildNewFan", sqlConnection);
            cmd.Parameters.Add(new SqlParameter("workstation_id", SqlDbType.Int) { Value = WorkstationId });
            cmd.Parameters.Add(new SqlParameter("order_id", SqlDbType.Int) { Value = CurrentOrder.OrderId });
            cmd.CommandType = CommandType.StoredProcedure;

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void BuildDefect()
        {
            SqlConnection sqlConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            SqlCommand cmd = new SqlCommand("BuildNewDefect", sqlConnection);
            cmd.Parameters.Add(new SqlParameter("workstation_id", SqlDbType.Int) { Value = WorkstationId });
            cmd.Parameters.Add(new SqlParameter("order_id", SqlDbType.Int) { Value = CurrentOrder.OrderId });
            cmd.CommandType = CommandType.StoredProcedure;

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        private void CreateSession()
        {
            if (!OrderSessionExists)
            {
                SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"INSERT INTO WorkstationSession VALUES" +
                                                $"({WorkstationId},{CurrentOrder.OrderId},0,0)", sqlConnection);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}
