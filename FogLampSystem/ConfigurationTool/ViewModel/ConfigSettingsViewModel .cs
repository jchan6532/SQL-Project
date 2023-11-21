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

namespace ConfigurationTool.ViewModel
{
    public class ConfigSettingsViewModel : INotifyPropertyChanged
    {
        // Define properties for your ConfigSettings columns
        private string _configKey;
        public string ConfigKey
        {
            get { return _configKey; }
            set
            {
                if (_configKey != value)
                {
                    _configKey = value;
                    OnPropertyChanged(nameof(ConfigKey));
                }
            }
        }

        private int _int_value;
        public int IntValue
        {
            get { return _int_value; }
            set
            {
                if (_int_value != value)
                {
                    _int_value = value;
                    OnPropertyChanged(nameof(IntValue));
                }
            }
        }

        private double _float_value;
        public double FloatValue
        {
            get { return _float_value; }
            set
            {
                if (_float_value != value)
                {
                    _float_value = value;
                    OnPropertyChanged(nameof(FloatValue));
                }
            }
        }

        private string _string_value;
        public string StringValue
        {
            get { return _string_value; }
            set
            {
                if (_string_value != value)
                {
                    _string_value = value;
                    OnPropertyChanged(nameof(StringValue));
                }
            }
        }

        // Add properties for other columns (int_value, float_value, string_value)

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
