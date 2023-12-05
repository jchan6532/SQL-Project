using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkStationAndon
{
    public class DatabaseManager : INotifyPropertyChanged
    {
        #region Private Fields

        private int _currentOrderID;

        #endregion


        #region Public Properties

        public int CurrentOrderID
        {
            get
            {
                return _currentOrderID;
            }
            set
            {
                if (_currentOrderID != value)
                {
                    _currentOrderID = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// The ID of the Workstation as it appears in the DB.
        /// </summary>
        [DefaultValue(-1)]
        public int EmployeeID
        {
            get;
            set;
        }

        [DefaultValue(-1)]
        public int WorkStationID
        {
            get;
        }

        public Employee WorkStationEmployee
        {
            get;
            set;
        }

        public Order CurrentOrder
        {
            get;
            set;
        }

        public string EmployeeName { get { return WorkStationEmployee.EmployeeName; } }

        public string EmployeeType { get { return WorkStationEmployee.EmployeeType; } }
        // no binding
        public int CurrentOrderAmount { get { return CurrentOrder.OrderAmount; } }
        // nobinding
        public int OrderFulfilled { get { return CurrentOrder.OrderFulfilled; } }
        // no binding
        public int DefectsFulfilled { get { return CurrentOrder.Defects; } }

        // no binding
        public int CurrentOrderLampsContributed
        {
            get 
            {
                int contributed = 0;
                SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT lamps_built FROM WorkstationSession WHERE order_id = {CurrentOrderID} AND workstation_id = {WorkStationID}", sqlConnection);

                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contributed += Int32.Parse(reader["lamps_built"].ToString());
                    }
                }
                sqlConnection.Close();

                return contributed;
            }
        }
        // no binding
        public int CurrentOrderLampsDefects
        {
            get
            {
                int defects = 0;
                SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT defects FROM WorkstationSession WHERE order_id = {CurrentOrderID} AND workstation_id = {WorkStationID}", sqlConnection);

                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        defects += Int32.Parse(reader["defects"].ToString());
                    }
                }
                sqlConnection.Close();

                return defects;
            }
        }

        #endregion


        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        #region Constructors

        public DatabaseManager(int employeeID)
        {
            EmployeeID = employeeID;
            WorkStationEmployee = new Employee(employeeID);
            WorkStationID = DatabaseManager.GetWorkStationID(employeeID);
        }

        #endregion

        #region Static Methods

        public static bool AuthenticateID(int employeeID)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
            SqlCommand cmd = new SqlCommand($"SELECT TOP 1 workstation_id FROM WorkstationOverview WHERE employee_id = {employeeID}",
                sqlConnection);

            sqlConnection.Open();
            object response = cmd.ExecuteScalar();
            sqlConnection.Close();

            return response != null;
        }

        public static int GetWorkStationID(int employeeID)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
            SqlCommand cmd = new SqlCommand($"SELECT TOP 1 workstation_id FROM WorkstationOverview WHERE employee_id = {employeeID}",
                sqlConnection);

            sqlConnection.Open();
            object response = cmd.ExecuteScalar();
            sqlConnection.Close();

            if (response == null)
            {
                return -1;
            }
            return Int32.Parse(response.ToString());
        }

        public static List<string> GetOrderIDs(int workstationID)
        {
            List<string> orderIDs = new List<string>();

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
            SqlCommand cmd = new SqlCommand($"SELECT DISTINCT order_id FROM WorkstationSession WHERE workstation_id = {workstationID}", sqlConnection);

            sqlConnection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string id = reader["order_id"].ToString();
                    orderIDs.Add($"Order {id}");
                }
            }


            sqlConnection.Close();
            return orderIDs;
        }

        public static Dictionary<string, int> GetOrdersReport(int orderID)
        {
            Dictionary<string, int> orderReport = new Dictionary<string, int>();

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
            SqlCommand cmd = new SqlCommand($"SELECT workstation_id, lamps_built FROM WorkstationSession WHERE order_id = {orderID}", sqlConnection);

            sqlConnection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string id = reader["workstation_id"].ToString();
                    string amount = reader["lamps_built"].ToString();

                    if (orderReport.ContainsKey(id))
                    {
                        orderReport[id] += Int32.Parse(amount);
                    }
                    else
                    {
                        orderReport.Add(id, Int32.Parse(amount));
                    }
                }
            }


            sqlConnection.Close();
            return orderReport;
        }

        public static Dictionary<string, int> GetWorkStationPartCounts(int workStationID)
        {
            Dictionary<string, int> parts = new Dictionary<string, int>();

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
            SqlCommand cmd = new SqlCommand($"SELECT part_count, part_name FROM BinOverview WHERE workstation_id = {workStationID}", sqlConnection);

            sqlConnection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader["part_name"].ToString();
                    string count = reader["part_count"].ToString();
                    parts.Add(name, Int32.Parse(count));
                }
            }


            sqlConnection.Close();
            return parts;
        }

        #endregion


        #region Private/Protected Methods

        protected virtual void OnPropertyChanged([CallerMemberName] string properyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyname));
        }

        #endregion
    }
}
