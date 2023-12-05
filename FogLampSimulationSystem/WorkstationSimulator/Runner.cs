/*
* FILE : Runner.cs
* PROJECT : PROG3070 - Gerritt Hooyer, Justin Chan
* PROGRAMMER : Gerritt Hooyer, Justin Chan
* FIRST VERSION : 2023-11-20
* DESCRIPTION :
* Contains the class that allows the program to simulate a single runner.
*/
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace WorkstationSimulator
{
    /// <summary>
    /// CLASS: Runner
    /// DESCRIPTION: Includes properties and methods that allow the program
    /// to simulate a single runner, refilling the required bins @ a consistent interval
    /// as defined in the config table of the program.
    /// </summary>
    class Runner
    {
        /// <summary>
        /// Retrieves a List of int that includes all of the extant Workstation IDs.
        /// </summary>
        private List<int> WorkstationIds
        {
            get
            {
                List<int> workstationIds = new List<int>();
                SqlConnection sqlConnection =
                                    new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
                SqlCommand cmd =
                    new SqlCommand("SELECT workstation_id FROM Workstation",
                        sqlConnection);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    workstationIds.Add(reader.GetInt32(0));
                }

                sqlConnection.Close();

                return workstationIds;
            }
        }

        /// <summary>
        /// Refills all bins that are below the threshold for all workstations.
        /// </summary>
        public void RefillBins()
        {
            foreach (int id in WorkstationIds)
            {
                Workstation workstation = new Workstation(id);
                workstation.RefillBins();
            }
        }
    }
}
