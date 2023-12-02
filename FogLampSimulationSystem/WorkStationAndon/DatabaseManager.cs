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

        private int _workStationID;
        private Thread _updateDataThread = null;

        #region Volatile Fields

        private volatile bool _stopUpdating;

        #endregion

        #endregion

        #region Public Properties

        public int WorkStationID
        {
            get
            {
                return _workStationID;
            }
            set
            {
                if (_workStationID != value)
                {
                    _workStationID = value;
                    OnPropertyChanged();
                }
            }
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
                WorkStationID++;
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
