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

        private DataSet _dataset = null;

        private DataTable _table = null;

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

        public List<string> Columns
        {
            get;
            set;
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
            Columns = new List<string>();
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

        public void UpdateData()
        {
            using (_connection = new SqlConnection(_connStr))
            {
                // Configure IUpdateCommand for new rows
                _adapter.UpdateCommand = new SqlCommand();
                _adapter.UpdateCommand.Connection = _connection;
                _adapter.UpdateCommand.CommandText = "UPDATE ConfigSettings " +
                    "SET int32_value = @int32_value, " +
                    "double_value = @double_value, " +
                    "string_value = @string_value " +
                    "WHERE config_key = @config_key";

                _adapter.UpdateCommand.Parameters.Add("@int32_value", SqlDbType.Int, 4, "int32_value");
                _adapter.UpdateCommand.Parameters.Add("@double_value", SqlDbType.Float, 8, "double_value");
                _adapter.UpdateCommand.Parameters.Add("@string_value", SqlDbType.NVarChar, 60, "string_value");
                _adapter.UpdateCommand.Parameters.Add("@config_key", SqlDbType.NVarChar, 60, "config_key");


                // Configure InsertCommand for new rows
                _adapter.InsertCommand = new SqlCommand();
                _adapter.InsertCommand.Connection = _connection;
                _adapter.InsertCommand.CommandText = "INSERT INTO ConfigSettings " +
                    "(config_key, int32_value, double_value, string_value) " +
                    "VALUES (@config_key, @int32_value, @double_value, @string_value)";

                _adapter.InsertCommand.Parameters.Add("@config_key", SqlDbType.NVarChar, 60, "config_key");
                _adapter.InsertCommand.Parameters.Add("@int32_value", SqlDbType.Int, 4, "int32_value");
                _adapter.InsertCommand.Parameters.Add("@double_value", SqlDbType.Float, 8, "double_value");
                _adapter.InsertCommand.Parameters.Add("@string_value", SqlDbType.NVarChar, 60, "string_value");


                // Ensure that the DataTable has the proper PrimaryKey set
                _table.PrimaryKey = new DataColumn[] { _table.Columns["config_key"] };

                foreach (ConfigurationRow configRow in ConfigurationData)
                {
                    DataRow row = _table.Rows.Find(configRow.ConfigurationKey);

                    // Row is existing row from the table
                    if (row != null)
                    {
                        // Update the existing values in the DataRow
                        row["int32_value"] = configRow.ConfigurationValue_Int;
                        row["double_value"] = configRow.ConfigurationValue_Float;
                        row["string_value"] = configRow.ConfigurationValue_String;
                    }
                    // Row was newly added, not yet reflected in the table
                    else
                    {
                        // Create new row object
                        DataRow newRow = _table.NewRow();
                        newRow["config_key"] = configRow.ConfigurationKey;
                        newRow["int32_value"] = configRow.ConfigurationValue_Int;
                        newRow["double_value"] = configRow.ConfigurationValue_Float;
                        newRow["string_value"] = configRow.ConfigurationValue_String;

                        // Add the new row to the DataTable
                        _table.Rows.Add(newRow);

                        // Insert the new key and default values to the default configuration table
                        CreateNewConfigKeyInDefault(configRow);
                    }
                }

                // Update the database with the changes
                _adapter.Update(_table);
                
            }
        }

        private void CreateNewConfigKeyInDefault(ConfigurationRow newConfig)
        {
            if (newConfig.ConfigurationValue_String == null)
                newConfig.ConfigurationValue_String = "none";

            string query = $"INSERT INTO DefaultSettings (config_key, int32_value, double_value, string_value) " +
                                   $"VALUES ('{newConfig.ConfigurationKey}', '{newConfig.ConfigurationValue_Int}', '{newConfig.ConfigurationValue_Float}', '{newConfig.ConfigurationValue_String}')";
            using (_connection = new SqlConnection(_connStr))
            {
                _connection.Open();

                using (_command = new SqlCommand(query, _connection))
                {
                    _command.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Called whenever the class needs to load the last saved configuration data
        /// </summary>
        public void GetConfigurationData()
        {
            string query = "SELECT * FROM ConfigSettings";
            using (_connection = new SqlConnection(_connStr))
            {
                _adapter = new SqlDataAdapter(query, _connection);
                _dataset = new DataSet();
                _adapter.Fill(_dataset);
                

                //ConfigurationData = new ObservableCollection<DataRow>(dataset.Tables[0].AsEnumerable());
                ConfigurationData = new ObservableCollection<ConfigurationRow>();
                _table = _dataset.Tables[0];
                var columns = _dataset.Tables[0].Columns;
                foreach (DataColumn column in columns)
                    Columns.Add(column.ColumnName);

                foreach (var propertyRow in _dataset.Tables[0].AsEnumerable())
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
