using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Diagnostics;
using System.Linq;

namespace SwiftNTFS
{
    public static class SwiftIO
    {
        /// <summary>
        /// </summary>
        /// <param name="ts">List of PermissionsEngine object</param>
        /// <param name="SearchIndex">PermissionsEngine Object Index</param>
        public static void DeleteListItem(List<PermissionsEngine> ts, int SearchIndex)
        {
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
            return System.IO.Path.Combine(Location, FolderName);
        }
    }
}