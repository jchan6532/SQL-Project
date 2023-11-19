using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;

using ConfigurationTool.Model;

namespace ConfigurationTool.ViewModel
{
    public class ConfigurationViewModel : INotifyPropertyChanged
    {
        #region Private Fields

        /// <summary>
        /// Configuration key value pairs binding to the XAML page
        /// </summary>
        private ObservableCollection<ConfigurationRow> _configurationData = null;

        /// <summary>
        /// Local instance of the SQL connection
        /// </summary>
        private SqlConnection _connection = null;

        /// <summary>
        /// Local instance of the SQL command
        /// </summary>
        private SqlCommand _command = null;

        /// <summary>
        /// Local instance of the SQL data adapter to fill the data grid on the XAML page
        /// </summary>
        private SqlDataAdapter _adapter = null;

        /// <summary>
        /// Connection string for the SQL database
        /// </summary>
        private string _connStr = null;

        #endregion

        #region Public Properties

        public ObservableCollection<ConfigurationRow> ConfigurationData
        {
            get
            {
                return _configurationData;
            }
            set
            {
                _configurationData = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event for whenever a property value has changed for XAML binding
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ConfigurationViewModel()
        {
            _connStr = ConfigurationManager.AppSettings.Get("Connection String");

        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Raises the event for whenever a property value has changed
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called whenever the class needs to load the last saved configuration data
        /// </summary>
        public void GetConfigurationData()
        {
            string query = "SELECT * FROM [SQL-PROJECT].[dbo].[ConfigSettings]";
            using (_connection = new SqlConnection(_connStr))
            {
                _adapter = new SqlDataAdapter(query, _connection);
                DataSet dataset = new DataSet();
                _adapter.Fill(dataset);

                //ConfigurationData = new ObservableCollection<DataRow>(dataset.Tables[0].AsEnumerable());
                ConfigurationData = new ObservableCollection<ConfigurationRow>();
                var columns = dataset.Tables[0].Columns;

                foreach (var propertyRow in dataset.Tables[0].AsEnumerable())
                {
                    ConfigurationRow configurationRow = new ConfigurationRow();
                    configurationRow.ConfigurationValue_String = "";
                    configurationRow.ConfigurationKey = propertyRow.ItemArray.First().ToString();
                    for (int i = 0; i < propertyRow.ItemArray.Length; i++)
                    {
                        if (columns[i].ColumnName.ToLower().Contains(nameof(String).ToLower()))
                        {
                            if (!(propertyRow.ItemArray[i] is DBNull))
                                configurationRow.ConfigurationValue_String = propertyRow.ItemArray[i].ToString();
                        }
                        else if (columns[i].ColumnName.ToLower().Contains(typeof(double).Name.ToLower()))
                        {
                            if (!(propertyRow.ItemArray[i] is DBNull))
                                configurationRow.ConfigurationValue_Float = (float)Convert.ToDouble(propertyRow.ItemArray[i]);
                        }
                        else if (columns[i].ColumnName.ToLower().Contains(typeof(int).Name.ToLower()))
                        {
                            if (!(propertyRow.ItemArray[i] is DBNull))
                                configurationRow.ConfigurationValue_Int = Convert.ToInt32(propertyRow.ItemArray[i]);
                        }
                    }
                    ConfigurationData.Add(configurationRow);
                }
            }
        }

        #endregion
    }
}
