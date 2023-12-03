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

        private Thread _updateDataThread = null;

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
            get
            {
                if (!IsAuthenticated)
                {
                    return -1;
                }

                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT TOP 1 workstation_id FROM WorkstationOverview WHERE employee_id = {EmployeeID}",
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
        }

        public Employee WorkStationEmployee
        {
            get;
            set;
        }

        public bool IsAuthenticated
        {
            get;
            set;
        } = false;

        #endregion


        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        #region Constructors

        public DatabaseManager()
        {

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
                LampsCreated++;
                Thread.Sleep(1000);
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
