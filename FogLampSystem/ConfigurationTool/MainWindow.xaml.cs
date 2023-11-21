using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

using ConfigurationTool.ViewModel;
using ConfigurationTool.Model;
using System.Data;
using System.Collections.ObjectModel;

namespace ConfigurationTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool ConfigurationKeyAdded { get; set; } = false;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ConfigurationViewModel();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var configs = (DataContext as ConfigurationViewModel).ConfigurationData;
            var nullConfigs = configs.Where(configItem => configItem.ConfigurationKey == null);
            nullConfigs.ToList().ForEach(nullConfig => configs.RemoveAt(configs.IndexOf(nullConfig)));

            (DataContext as ConfigurationViewModel).UpdateData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as ConfigurationViewModel).GetConfigurationData();
        }
    }
}
