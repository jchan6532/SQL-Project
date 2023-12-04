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

        private int _lampsCreated;

        private int _defectCount;

        private int _currentOrderID;

        private int _currentOrderAmount;

        private Thread _updateDataThread = null;

        private HomePage _homePage;

        #endregion


        #region Volatile Fields

        private volatile bool _stopUpdating = false;

        #endregion


        #region Public Properties

        public int LampsCreated
        {
            get
            {
                return _lampsCreated;
            }
            set
            {
                if (_lampsCreated != value)
                {
                    _lampsCreated = value;
                    OnPropertyChanged();
                }
            }
        }

        public int DefectCount
        {
            get
            {
                return _defectCount;
            }
            set
            {
                if (_defectCount != value)
                {
                    _defectCount = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public int CurrentOrderAmount
        {
            get
            {
                return _currentOrderAmount;
            }
            set
            {
                if (_currentOrderAmount != value)
                {
                    _currentOrderAmount = value;
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

        public string EmployeeName { get { return WorkStationEmployee.EmployeeName; } }

        public string EmployeeType { get { return WorkStationEmployee.EmployeeType; } }

        #endregion


        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        #region Constructors

        public DatabaseManager(int employeeID, HomePage homePage)
        {
            EmployeeID = employeeID;
            WorkStationEmployee = new Employee(employeeID);
            WorkStationID = DatabaseManager.GetWorkStationID(employeeID);
            _homePage = homePage;
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
            SqlCommand cmd = new SqlCommand($"SELECT order_id FROM WorkstationSession WHERE workstation_id = {workstationID}", sqlConnection);

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

        #endregion


        #region Private/Protected Methods

        protected virtual void OnPropertyChanged([CallerMemberName] string properyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyname));
        }

        private void UpdatingDataAsync()
        {
            while (!_stopUpdating)
            {
                _homePage.Invoke(new Action(() =>
                {
                    // Access LampsCreated directly from the DatabaseManager
                    _homePage.Manager.LampsCreated++;
                }));

                Thread.Sleep(5000);
            }
        }

        #endregion


        #region Public Methods

        public void Stop()
        {
            _stopUpdating = true;
            _updateDataThread.Join();
        }

        public void Start()
        {
            ThreadStart updateDataTs = new ThreadStart(UpdatingDataAsync);
            _updateDataThread = new Thread(updateDataTs);
            _updateDataThread.Start();
        }

        #endregion
    }
}
