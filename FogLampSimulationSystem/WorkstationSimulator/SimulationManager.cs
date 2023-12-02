/*
* FILE : SimulationManager.cs
* PROJECT : PROG3070 - Gerritt Hooyer, Justin Chan
* PROGRAMMER : Gerritt Hooyer, Justin Chan
* FIRST VERSION : 2023-11-20
* DESCRIPTION :
* Retrieves information related to the simulation from the database via custom gets on it's properties.
* Can be initialized to simulate a runner or workstation w/ employee.
*/
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;

namespace WorkstationSimulator
{
    /*
    * CLASS: SimulationManager
    * DESCRIPTION: Retrieves information related to the simulation from the database via custom gets on it's properties.
    * Can be initialized to simulate a runner or workstation w/ employee.
    */
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
                int tickRate = 1;

                sqlConnection.Open();
                object response = cmd.ExecuteScalar();
                sqlConnection.Close();

                if (response != null)
                {
                    Int32.TryParse((string)response, out tickRate);
                }

                return tickRate;
            }
        }

        public int RefillInterval
        {
            // Time interval (# of seconds) between each refill
            get
            {
                SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd =
                    new SqlCommand("SELECT [config_value] FROM ConfigSettings WHERE config_key = 'system.refill_interval'",
                        sqlConnection);
                int refillInterval = 300;

                sqlConnection.Open();
                object response = cmd.ExecuteScalar();
                sqlConnection.Close();

                if (response != null)
                {
                    Int32.TryParse((string)response, out refillInterval);
                }
                return refillInterval;
            }
        }

        public float SimulationSpeed
        {
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
                    float.TryParse((string)response, out simSpeed);
                }
                return simSpeed;
            }
        }

        public int FanTickCount
        {
            get;
            set;
        }

        public int RefillTickCount
        {
            get;
            set;
        }

        public int TickIncrement
        {
            get
            {
                int tickIncrement = 60 / TickRate;
                return tickIncrement;
            }
        }

        public int ElapsedTime { get; set; }

        public Runner SimRunner
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

        public SimulationManager()
        {
            RefillTickCount = 0;
            FanTickCount = 0;
            SimRunner = new Runner();
        }

        public void Sleep()
        {
            if (SimulationSpeed > 0)
            {
                int sleepTime = (int)(TickIncrement * 1000 / SimulationSpeed);
                Thread.Sleep(sleepTime);
            }
            ElapsedTime += TickIncrement;

            Console.WriteLine($"Elapsed Time: {ElapsedTime}(s)");
        }

    }
}
