﻿using System;
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
            ConfigurationName = PropertyNameTextBox.Text;
            ConfigurationValue = PropertyValueBox.Text;
            Close();
        }

        
        private void PropertyValueBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex _regex = new Regex("[^0-9.-]+");
            e.Handled = _regex.IsMatch(e.Text);

            if (e.Handled)
                ErrorMsg.Text = "Input must be numeric";
            else
                ErrorMsg.Text = "";
        }

        private void PropertyNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex _regex = new Regex("[^A-Za-z]+");
            e.Handled = _regex.IsMatch(e.Text);

            if (e.Handled)
                ErrorMsg.Text = "Input must be numeric";
            else
                ErrorMsg.Text = "";
        }
    }
}