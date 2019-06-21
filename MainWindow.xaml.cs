//MIT License

//Copyright(c) 2019 Hayk Jomardyan

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Media;
using SwiftNTFS;
using System.Resources;
using NLog;
using NLog.Config;
using FS_Operations.Helper;
using NLog.Targets.Wrappers;
using CommonFeatures;

namespace FSOperations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// /// Test    
    /// </summary>
    public partial class MainWindow : Window
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public List<PermissionsEngine> ts = new List<PermissionsEngine>();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
            
        }

        void SwiftLogger(object sender, EngineEventArgs eventArgs)
        {
            logger.Info($" Log  {sender.ToString()} - {eventArgs.OriginName} - {eventArgs.OriginMessage}");

        }
        void SwiftLogger(object sender, LoggerEventArgs eventArgs)
        {
            logger.Info($" Log  {sender.ToString()} - {eventArgs.OriginName} - {eventArgs.LogMessage}");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            PBar.IsIndeterminate = true;
            //Loading Configs from AppSeetings
            Domain.Text = ConfigurationManager.AppSettings.Get("Domain");
            ADOU.Text = ConfigurationManager.AppSettings.Get("OU");
            FSLoc.Text = ConfigurationManager.AppSettings.Get("FSLocation");
            PBar.IsIndeterminate = false;
            logger.Info("Config Loaded");


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // var chktest = Convert.ToBoolean(cFS_R.IsChecked) | Convert.ToBoolean(cFS_RW.IsChecked)
            // | Convert.ToBoolean(cFS_RWX.IsChecked);
            if (Domain.Text.Length > 0)
            {
                if (ADOU.Text.Length > 0)
                {
                    if (ts.Count > 0)
                    {
                        if (FSLoc.Text.Length > 0)
                        {
                            if (TB_Name.Text.Length > 0)
                            {
                                BtnStartOperation();
                            }
                            else
                            {
                                MessageBox.Show("Please enter Folder Name AD/SG", "", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter FileServer location", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select Access level", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter Active Directory Organization Unit (OU) Path path", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                
                MessageBox.Show("Please enter domain name", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnStartOperation()
        {
            try
            {
                foreach (var item in ts)
                {
                    
                    //logger.Info($"Started job: {TB_Name.Text} {item.Accessflag}");

                    item.DataSetFinished += SwiftLogger;
                    item.DataSet(@FSLoc.Text, TB_Name.Text, Domain.Text, ADOU.Text, TB_Owner.Text);
                    item.Start();
                    logger.Info($"Finished: {TB_Name.Text} {item.Accessflag}");
                }
                MessageBox.Show("FInish");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AutoNTFS_Checked(object sender, RoutedEventArgs e)
        {
            FSStatus.Fill = new SolidColorBrush(Colors.Red);
        }

        private void AutoNTFS_Unchecked(object sender, RoutedEventArgs e)
        {
            FSStatus.Fill = new SolidColorBrush(Colors.Transparent);
        }

        private void FSLoc_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (FSLoc.Text.Length == 0)
            {
                FSStatus.Fill = new SolidColorBrush(Colors.Red);
            }
            else if (FSLoc.Text.Length != 0)
            {
                FSStatus.Fill = new SolidColorBrush(Colors.Green);
            }
        }

        private void CFS_R_Checked(object sender, RoutedEventArgs e)
        {
            PermissionsEngine R = new PermissionsEngine();
            R.Accessflag = "R";
            R.Index = 0;
            ts.Add(R);
            AutoNTFS.IsChecked = true;
            


        }

        private void CFS_RW_Checked(object sender, RoutedEventArgs e)
        {
            PermissionsEngine RW = new PermissionsEngine();
            RW.Accessflag = "RW";
            RW.Index = 1;
            ts.Add(RW);
            AutoNTFS.IsChecked = true;
            
        }

        

        private void CFS_RWX_Checked(object sender, RoutedEventArgs e)
        {
            PermissionsEngine RWX = new PermissionsEngine();
            RWX.Accessflag = "RWX";
            RWX.Index = 2;
            ts.Add(RWX);
            AutoNTFS.IsChecked = true;
        }

        private void CFS_R_Unchecked(object sender, RoutedEventArgs e)
        {
            SwiftIO.DeleteListItemByQuery(ts, 0);
        }

        private void CFS_RW_Unchecked(object sender, RoutedEventArgs e)
        {
            SwiftIO.DeleteListItemByQuery(ts, 1);
        }

        private void CFS_RWX_Unchecked(object sender, RoutedEventArgs e)
        {
            SwiftIO.DeleteListItemByQuery(ts, 2);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.Description = "Select main directory";
                _ = dialog.ShowDialog();

                FSLoc.Text = dialog.SelectedPath;
            }
        }

        //public static string GetFSLoc()
        //{
        //    return ADOU.Text;
        //}




        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var target = new WpfRichTextBoxTarget {
                    Name = "RichText",
                    Layout =
                        "[${date:useUTC=false}] :: [${level:uppercase=true}]:: ${message} ${exception:innerFormat=tostring:maxInnerExceptionLevel=10:separator=,:format=tostring}",
                    ControlName = RichLog.Name,
                    FormName = GetType().Name,
                    AutoScroll = true,
                    MaxLines = 100000,
                    UseDefaultRowColoringRules = true,
                };
                var asyncWrapper = new AsyncTargetWrapper { Name = "RichTextAsync", WrappedTarget = target };

                LogManager.Configuration.AddTarget(asyncWrapper.Name, asyncWrapper);
                LogManager.Configuration.LoggingRules.Insert(0, new LoggingRule("*",LogLevel.Debug , asyncWrapper));
                LogManager.ReconfigExistingLoggers();

            });
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutBox aboutbox = new AboutBox();
            aboutbox.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            DefaultConfigurator configurator = new DefaultConfigurator();
            configurator.Show();
        }
    }



}