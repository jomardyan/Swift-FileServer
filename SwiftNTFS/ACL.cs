using CommonFeatures;
using System;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Windows;

namespace SwiftNTFS
{
    public class ACL
    {
        public event EventHandler<LoggerEventArgs> LoggerEvent;

        public readonly string Name = "ACL";

        private AccessControlType _fsAccess;

        /// <summary>
        /// True => Allow, False => Deny
        /// </summary>
        public bool Access { get; set; }

        public bool Modify { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ACL acl &&
                   _fsAccess == acl._fsAccess &&
                   Read == acl.Read &&
                   Write == acl.Write &&
                   Modify == acl.Modify &&
                   Access == acl.Access;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_fsAccess, Read, Write, Modify, Access);
        }

        /// <summary>
        /// Sets permissions on a directory for a specified user.
        /// </summary>
        /// <param name="dir">Directory path</param>
        /// <param name="user">Username or Active Directory Security Group Name</param>
        public void Set(string dir, string user)
        {
            LogEvent("Set Event started");

            try
            {
                Debug.WriteLine("AclSet Started");

                _fsAccess = Access ? AccessControlType.Allow : AccessControlType.Deny;

                AddAccessControl permissions = new AddAccessControl();

                if (Read)
                {
                    permissions.AddDirectorySecurity(dir, user, FileSystemRights.ReadAndExecute, _fsAccess);
                }

                if (Write)
                {
                    permissions.AddDirectorySecurity(dir, user, FileSystemRights.Write, _fsAccess);
                }

                if (Modify)
                {
                    permissions.AddDirectorySecurity(dir, user, FileSystemRights.Modify, _fsAccess);
                }

                Debug.WriteLine("AclSet Finished");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Logs an event using the LoggerEvent.
        /// </summary>
        /// <param name="message">Message to log</param>
        private void LogEvent(string message)
        {
            LoggerEventArgs loggerEventArgs = new LoggerEventArgs
            {
                OriginName = Name,
                OriginMessage = message
            };
            LoggerEvent?.Invoke(this, loggerEventArgs);
        }
    }
}
