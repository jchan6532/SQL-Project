using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationTool.ViewModel
{
    public class ConfigurationOptions : INotifyPropertyChanged
    {
        private Dictionary<string, string> _configurationData = null;
        public Dictionary<string, string> ConfigurationData
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
        public ConfigurationOptions()
        {
            // Setup config directory
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void CreateConfigurationFile()
        {
            // Get XML service
        }
    }
}
