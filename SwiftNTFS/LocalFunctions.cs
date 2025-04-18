﻿using System;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Management.Automation;
using System.Text;
using System.Windows;
using CommonFeatures;

// Copyright Hayk Jomardyan
namespace SwiftNTFS
{
    /// <summary>
    /// </summary>
    public class LocalFunctions
    {
        public event EventHandler<LoggerEventArgs> LoggerEvent;
        public readonly string name = "LocalFunctions";
        
        /// <summary>
        /// Get Active session user UPN (string), for future use
        /// </summary>
        private string LoadActiveDirectoryIdentity()
        {
            LoggerEventArgs loggerEventArgs = new LoggerEventArgs
            {
                OriginName = name,
                OriginMessage = "Log Event"
            };
            LoggerEvent?.Invoke(this, loggerEventArgs);
            try
            {
                var user = UserPrincipal.Current.UserPrincipalName;
                if (user == null)
                {
                    user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                }
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Create Active directory secuirity group using PS Session
        /// </summary>
        /// <param name="ADServer"></param>
        /// <param name="OUpath"></param>
        /// <param name="Name"></param>
        internal static void CreadeAdGroup(string ADServer, string OUpath, string Name, string Description, string Owner)
        {

            string OwnerSet()
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("\"@{info=\"");
                builder.Append("Owner- ");
                builder.Append(Owner);
                builder.Append("\"}");
                return builder.ToString();
            }

            Debug.WriteLine("Create AD Group Started");

            PowerShell ps = PowerShell.Create();

            ps.Commands.AddCommand("New-ADGroup");
            ps.AddParameter("Server", ADServer);
            ps.AddParameter("Path", OUpath);
            ps.AddParameter("Name", Name);
            ps.AddParameter("GroupScope", "Global");
            ps.AddParameter("GroupCategory", "Security");
            ps.AddParameter("Description", Description);
            ps.AddParameter("OtherAttributes", OwnerSet());

            try
            {
                ps.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while creating Active directory group: " + ex.Message);
            }
            if (ps != null)
            {
                ps.Dispose();
            }
        }

        public static string StBuilder(string Name, string Type)
        {
            if (Type == "R")
            {
                Type = " R--";
            }
            else if (Type == "RW")
            {
                Type = " RW-";
            }
            else if (Type == "RWX")
            {
                Type = " RWX";
            }
            StringBuilder Str = new StringBuilder();
            Str.Append("FS ");
            Str.Append(Name);
            Str.Append(Type);

            Debug.WriteLine(Str.ToString());
            return Str.ToString();
        }
    }
}
