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


        //Notice
        /// <summary>
        /// </summary>
        /// <param name="ts">List of PermissionsEngine object</param>
        /// <param name="SearchIndex">PermissionsEngine Object Index</param>
        public static void DeleteListItem(List<PermissionsEngine> ts, int SearchIndex)
        {
            LoggerEventArgs eventArgs = new LoggerEventArgs();
            eventArgs.OriginName = name;
            eventArgs.LogMessage = "DeleteListItem operations Started";
            LoggerEvent?.Invoke(null, eventArgs);

            foreach (var item in ts)
            {
                if ((int)item.Index == SearchIndex)
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
            LoggerEventArgs eventArgs = new LoggerEventArgs();
            eventArgs.OriginName = name;
            eventArgs.LogMessage = "DeleteListItemByQuery operations Started";
            LoggerEvent?.Invoke(null, eventArgs);
            var quesry = from PermissionsEngine a in ts
                         where a.Index == SearchIndex
                         select a;
            ts.Remove(quesry.FirstOrDefault());
        }

        /// <summary>
        /// </summary>
        /// <param name="FolderName">Folder name</param>
        /// <param name="Location">Top level folder.</param>
        public static void CreateFolder(string Location)
        {
            LoggerEventArgs eventArgs = new LoggerEventArgs();
            eventArgs.OriginName = name;
            eventArgs.LogMessage = "CreateFolder operations Started";
            LoggerEvent?.Invoke(null, eventArgs);

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
        /// <returns></returns>
        public static string BulldDir(string FolderName, string Location)
        {
            LoggerEventArgs eventArgs = new LoggerEventArgs();
            eventArgs.OriginName = name;
            eventArgs.LogMessage = "BulldDir";
            LoggerEvent?.Invoke(null, eventArgs);

            return System.IO.Path.Combine(Location, FolderName);
        }
    }
}