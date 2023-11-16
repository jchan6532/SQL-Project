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

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("justin", "hfghfg");
            data.Add("sdf", "hfghfg");
            data.Add("jushfghtin", "hfghfg");
            data.Add("jushjkjktin", "hfghfg");
            (DataContext as ConfigurationOptions).ConfigurationData = data;
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


        }

        private void AddConfigurationBtn_Click(object sender, RoutedEventArgs e)
        {
            var addConfig = new AddConfiguration();
            addConfig.ShowDialog();

            Label nameLabel = new Label();
            nameLabel.Content = addConfig.ConfigurationName;
            nameLabel.Margin = new Thickness
            {
                Left = 10,
                Right = 10,
                Bottom = 0,
                Top = 0
            };
            nameLabel.FontSize = 15;

            TextBox nameTextBox = new TextBox();
            nameTextBox.Width = 100;
            nameTextBox.Margin = new Thickness
            {
                Left = 10,
                Right = 10,
                Bottom = 0,
                Top = 0
            };
            nameTextBox.FontSize = 15;
            nameTextBox.Text = addConfig.ConfigurationValue;
            nameTextBox.PreviewTextInput += TextBox_PreviewTextInput;

            StackPanel configStackPanel = new StackPanel();
            configStackPanel.Orientation = Orientation.Horizontal;
            configStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            configStackPanel.Margin = new Thickness
            {
                Left = 0,
                Right = 0,
                Bottom = 5,
                Top = 5
            };

            configStackPanel.Children.Add(nameLabel);
            configStackPanel.Children.Add(nameTextBox);

            int buttonIndex = Configurations.Children.IndexOf(ExportDataBtn);
            Configurations.Children.Insert(buttonIndex, configStackPanel);

            
        }
    }
}
