using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private volatile bool _stopUpdating;

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

        public int WorkStationID
        { 
            get;
            set;
        }

        #endregion


        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DatabaseManager()
        {
            _stopUpdating = false;
        }

        public DatabaseManager(int workStationID)
        {
            _stopUpdating = false;
            WorkStationID = workStationID;
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
