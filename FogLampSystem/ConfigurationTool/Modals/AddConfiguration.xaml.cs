using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ConfigurationTool.Modals
{
    /// <summary>
    /// Interaction logic for AddConfiguration.xaml
    /// </summary>
    public partial class AddConfiguration : Window
    {
        public string ConfigurationName { get; set; }

        public string ConfigurationValue { get; set; }
        public AddConfiguration()
        {
            InitializeComponent();
        }

        private void AddConfigBtn_Click(object sender, RoutedEventArgs e)
        {
            ConfigurationName = "dfs";
            ConfigurationValue = "Dg";
            Close();
        }
    }
}
