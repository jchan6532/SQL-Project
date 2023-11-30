using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WorkstationSimulator
{
    /*
     *
     * Class: Workstation
     * Description: Represents a Workstation. Updates it's properties with data from the database.
     */
    internal class Workstation
    {

        public int WorkstationId { get; set; }
        public List<Bin> Bins
        {
            get
            {
                // Create a list of bins and use a SQL statement to select all of the relevant bin information.
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

        /// <summary>
        /// Returns the Workstation's currently assigned Employee. Null if no employee is assigned or the workstation doesn't exist.
        /// </summary>
        public Employee WorkstationEmployee
        {
            get
            {
                // Create objects
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

        /// <summary>
        /// Returns the time it will take, in seconds to build the fan the worker
        /// is currently building.
        /// Build time is calculated as follows:
        /// buildTime = (defaultBuildTime +/- (defaultBuildTime * 0.1)) * employeeBuildSpeedModifier
        /// </summary>
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
        /// <summary>
        /// Returns a bool representing whether the workstation has enough parts present to build
        /// a lamp.
        /// </summary>
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

        /// <summary>
        /// Returns the current OrderSession, which is an object that
        /// ties a Workstation to a larger order, and tracks it's contributions
        /// to that Order.
        /// </summary>
        public OrderSession CurrentOrderSession
        {
            get
            {
                // If an OrderSession doesn't exist, create one.
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


        /// <summary>
        /// Checks whether an OrderSession currently exists for the Workstation.
        /// </summary>
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

        /// <summary>
        /// Returns the current Order that the Workstation is contributing to.
        /// null if an Order that can be contributed to does not exist.
        /// It will automatically grab the first Order that is not complete.
        /// </summary>
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

        public bool ShouldWarnRunner
        {
            get
            {
                bool result = false;
                foreach (Bin bin in Bins)
                {
                    if (bin.Count <= RefillWarningAmount)
                    {
                        result = true;
                        break;
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Returns the RefillWarningAmount, which is the point at which the runner is notified that a refill is required.
        /// 
        /// </summary>
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
                if (bin.Count <= RefillWarningAmount)
                {
                    SqlConnection sqlConnection =
                        new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("RefillBin", sqlConnection);
                    cmd.Parameters.Add(new SqlParameter("bin_id", SqlDbType.Int) { Value = bin.BinId });
                    cmd.CommandType = CommandType.StoredProcedure;

                    Console.WriteLine($"Refilled Workstation {WorkstationId}'s {bin.PartName} bin.");
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
