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
using ConfigurationTool.Modals;

namespace ConfigurationTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ConfigurationOptions();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex _regex = new Regex("[^0-9.-]+");
            e.Handled = _regex.IsMatch(e.Text);

            if (e.Handled)
                ErrorMsg.Text = "Input must be numeric";
            else
                ErrorMsg.Text = "";
        }

        private void ExportDataBtn_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> configData = new Dictionary<string, string>();

            
            (DataContext as ConfigurationOptions).CreateConfigurationFile();

            var addConfig = new AddConfiguration();
            addConfig.ShowDialog();
            ErrorMsg.Text = $"{addConfig.ConfigurationName} = {addConfig.ConfigurationValue}";
        }
    }
}
