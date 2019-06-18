using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace FSOperations
{
    /// <summary>
    /// Interaction logic for DefaultConfigurator.xaml
    /// </summary>
    public partial class DefaultConfigurator : Window
    {
        public DefaultConfigurator()
        {
            InitializeComponent();
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["Domain"].Value = DomainTextBox.Text;
                config.AppSettings.Settings["OU"].Value = OUTextBox.Text;
                config.AppSettings.Settings["FSLocation"].Value = FSLocationTextBox.Text;
                config.AppSettings.Settings["Description"].Value = DescriptionTextBoc.Text;
                config.Save(ConfigurationSaveMode.Modified);
                
                ConfigurationManager.RefreshSection("appSettings");
                MessageBox.Show("Configuration set succesfuly", "CPT" , MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"CPT",MessageBoxButton.OK, MessageBoxImage.Error);

            }
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadConfig();

        }

        private void LoadConfig()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            this.DomainTextBox.Text = config.AppSettings.Settings["Domain"].Value;
            this.OUTextBox.Text = config.AppSettings.Settings["OU"].Value;
            this.FSLocationTextBox.Text = config.AppSettings.Settings["FSLocation"].Value;
            this.DescriptionTextBoc.Text = config.AppSettings.Settings["Description"].Value;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            this.DomainTextBox.Clear();
            this.OUTextBox.Clear();
            this.FSLocationTextBox.Clear();
            this.DescriptionTextBoc.Clear(); 

        }
    }
}
