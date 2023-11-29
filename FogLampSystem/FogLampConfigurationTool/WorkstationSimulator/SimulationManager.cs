using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkstationSimulator
{
    internal class SimulationManager
    {
        public int TickRate
        { // The # of ticks / minute
            get
            {
                SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd =
                    new SqlCommand("SELECT [config_value] FROM ConfigSettings WHERE config_key = 'system.tick_rate'",
                        sqlConnection);
                int tickRate = -1;

                sqlConnection.Open();
                object response = cmd.ExecuteScalar();
                sqlConnection.Close();

                if (response != null)
                {
                    tickRate = Int32.Parse((string)response);
                }

                return tickRate;
            }
        } 

        public int RefillInterval {
            // Time interval (# of seconds) between each refill
            get {
                SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd =
                    new SqlCommand("SELECT [config_value] FROM ConfigSettings WHERE config_key = 'system.refill_interval'",
                        sqlConnection);
                int refillInterval = -1;

                sqlConnection.Open();
                object response =  cmd.ExecuteScalar();
                sqlConnection.Close();

                if (response != null)
                {
                    refillInterval = Int32.Parse((string)response);
                }
                return refillInterval;
            }
        }

        public float SimulationSpeed {
            get
            {
                SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd =
                    new SqlCommand("SELECT [config_value] FROM ConfigSettings WHERE config_key = 'system.sim_speed'",
                        sqlConnection);
                float simSpeed = -1.0f;

                sqlConnection.Open();
                object response = cmd.ExecuteScalar();
                sqlConnection.Close();

                if (response != null)
                {
                    simSpeed = float.Parse((string)response);
                }
                return simSpeed;
            }
        }

        public int FanTickCount { 
            get; 
            set;
        }

        public int RefillTickCount
        {
            get; 
            set;
        }

        public Workstation SimWorkstation
        {
            get; set;
        }

        public SimulationManager(int workstationId)
        {
            FanTickCount = 0;
            RefillTickCount = 0;
            SimWorkstation = new Workstation(workstationId);
        }


    }
}
