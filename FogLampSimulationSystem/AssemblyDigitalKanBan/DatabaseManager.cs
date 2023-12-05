/*
* FILE : DatabaseManager.cs
* PROJECT : PROG3070 - Gerritt Hooyer, Justin Chan
* PROGRAMMER : Gerritt Hooyer, Justin Chan
* FIRST VERSION : 2023-11-20
* DESCRIPTION :
* Retrieves and updates data in the ConfigSettings table of the database.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AssemblyDigitalKanBan
{
    internal class DatabaseManager
    {
        string ConnectionString { get;}


        public DatabaseManager(string connectionString)
        {
            ConnectionString = connectionString;
        }


        /// <summary>
        /// Returns a list with the a List of int of all current Workstation IDs.
        /// </summary>
        /// <returns></returns>
        public List<int> GetWorkstationIds()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = conn.CreateCommand();
            List<int> workstationIds = new List<int>();
            cmd.CommandText = "SELECT workstation_id FROM Workstation";
            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read()) workstationIds.Add(reader.GetInt32(0));
            }
            conn.Close();
            return workstationIds;
        }

        /// <summary>
        /// Returns a DataTable with Workstation Info containing the following columns:
        /// <br/><br/>
        /// employee_id<br/>
        /// employee_name<br/>
        /// order_id<br/>
        /// order_amount<br/>
        /// lamps_built<br/>
        /// defects<br/>
        /// defect_rate<br/>
        /// </summary>
        /// <param name="workstationId">The Workstation's ID.</param>
        /// <returns>A DataTable with info related to a particular workstation.</returns>
        public DataTable GetWorkstationInfo(int workstationId)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = conn.CreateCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable workstationProductionInfo = new DataTable();

            cmd.CommandText = "SELECT employee_id, employee_name, SessionOverview.order_id, LampOrder.order_amount, " +
                              "SessionOverview.lamps_built, SessionOverview.defects, SessionOverview.defect_rate " +
                              "FROM SessionOverview " +
                              "INNER JOIN LampOrder on LampOrder.order_id = SessionOverview.order_id " +
                              "INNER JOIN WorkstationOverview ON " +
                              "WorkstationOverview.workstation_id = SessionOverview.workstation_id " +
                              $"WHERE {workstationId} = SessionOverview.workstation_id " +
                              $"ORDER BY order_id DESC";
            conn.Open();
            adapter.Fill(workstationProductionInfo);
            conn.Close();

            return workstationProductionInfo;
        }
        /// <summary>
        /// Returns the # of lamps that would need to be built to complete the order.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>An int representing the # of lamps that need to be built.</returns>
        public int GetOrderAmount(int orderId)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = conn.CreateCommand();
            int result = 0;

            cmd.CommandText = $"SELECT order_amount FROM LampOrder WHERE order_id = {orderId}";

            conn.Open();
            result = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

            return result;
        }

        /// <summary>
        /// Gets the current number 
        /// </summary>
        /// <param name="workstationId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable GetWorkstationOrderContribution(int workstationId, int orderId)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = conn.CreateCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable ocTable = new DataTable();
            cmd.CommandText = "SELECT SUM(lamps_built), " +
                              $"(SELECT lamps_built FROM SessionOverview WHERE workstation_id = {workstationId} AND order_id = {orderId}) " +
                              $"FROM SessionOverview WHERE order_id = {orderId} AND workstation_id != {workstationId}";

            conn.Open();
            adapter.Fill(ocTable);
            conn.Close();

            return ocTable;
        }

        public int GetFirstIncompleteOrderId()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = conn.CreateCommand();
            int orderId = -1;
            object result = null;
            cmd.CommandText = "SELECT TOP 1 order_id FROM LampOrder WHERE order_fulfilled < order_amount";
            conn.Open();
            result = cmd.ExecuteScalar();
            conn.Close();
            if (result != null)
            {
                orderId = Convert.ToInt32(result);
            }
            return orderId;
        }

        public DataTable GetWorkstationAllTimeLampsProduced()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = conn.CreateCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            cmd.CommandText = "SELECT workstation_id, lamps_built FROM Workstation";
            conn.Open();
            DataTable allTimeTable = new DataTable();
            adapter.Fill(allTimeTable);
            conn.Close();

            return allTimeTable;
        }
    }
}
