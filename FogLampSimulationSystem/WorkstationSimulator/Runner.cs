using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkstationSimulator
{
    class Runner
    {
        private List<int> WorkstationIds
        {
            get
            {
                List<int> workstationIds = new List<int>();
                SqlConnection sqlConnection =
                                    new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
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
