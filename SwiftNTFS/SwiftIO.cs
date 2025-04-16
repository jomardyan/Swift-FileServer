using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Diagnostics;
using System.Linq;
using CommonFeatures;
namespace SwiftNTFS
{
    public static class SwiftIO
    {
        public static readonly string name = "SwiftIO";
        public static event EventHandler<LoggerEventArgs> LoggerEvent;

        private static void LogEvent(string originName, string message)
        {
            LoggerEventArgs eventArgs = new LoggerEventArgs
            {
                OriginName = originName,
                LogMessage = message
            };
            LoggerEvent?.Invoke(null, eventArgs);
        }

        //Notice
        /// <summary>
        /// </summary>
        /// <param name="ts">List of PermissionsEngine object</param>
        /// <param name="SearchIndex">PermissionsEngine Object Index</param>
        public static void DeleteListItem(List<PermissionsEngine> ts, int SearchIndex)
        {
            LogEvent(name, "DeleteListItem operations Started");

            foreach (var item in ts)
            {
                if (item.Index == SearchIndex)
                {
                   
                    ts.Remove(item);
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts">List of PermissionsEngine object</param>
        /// <param name="SearchIndex">PermissionsEngine Object Index</param>
        public static void DeleteListItemByQuery(List<PermissionsEngine> ts, int SearchIndex)
        {
            LogEvent(name, "DeleteListItemByQuery operations Started");
            var query = ts.FirstOrDefault(a => a.Index == SearchIndex);
            if (query != null)
            {
                ts.Remove(query);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="FolderName">Folder name</param>
        /// <param name="Location">Top level folder.</param>
        public static void CreateFolder(string Location)
        {
            LogEvent(name, "CreateFolder operations Started");

            Debug.WriteLine("Create Folder Started");
          
            try
            {
                System.IO.Directory.CreateDirectory(Location);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            Debug.WriteLine("Create folder Finished");
        }

        /// <summary>
        /// Return IO Path of Location and folder name combination
        /// </summary>
        /// <param name="FolderName">Folder name</param>
        /// <param name="Location">Top level folder.</param>
        public static string BuildDir(string FolderName, string Location)
        {
            LogEvent(name, "BuildDir");

            return System.IO.Path.Combine(Location, FolderName);
        }
    }
}
