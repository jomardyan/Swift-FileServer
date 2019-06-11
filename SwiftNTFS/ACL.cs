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

        public readonly string name = "ACL";

        private AccessControlType FSAccess;

        /// <summary>
        /// True =&gt; Allow, False =&gt; Denay
        /// </summary>
        public bool Access;

        public bool Modify { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ACL aCL &&
                   FSAccess == aCL.FSAccess &&
                   Read == aCL.Read &&
                   Write == aCL.Write &&
                   Modify == aCL.Modify &&
                   Access == aCL.Access;
        }

        public override int GetHashCode()
        {
            var hashCode = 1083378608;
            hashCode = hashCode * -1521134295 + FSAccess.GetHashCode();
            hashCode = hashCode * -1521134295 + Read.GetHashCode();
            hashCode = hashCode * -1521134295 + Write.GetHashCode();
            hashCode = hashCode * -1521134295 + Modify.GetHashCode();
            hashCode = hashCode * -1521134295 + Access.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Set Permissions into onto directory
        /// </summary>
        /// <param name="dir">Directory</param>
        /// <param name="user">Username Or Active Directory SG Name</param>
        //OROGINAL
        public void Set(string dir, string user)
        {
            LoggerEventArgs loggerEventArgs = new LoggerEventArgs
            {
                OriginName = name,
                OriginMessage = "Set Event started"
            };
            LoggerEvent?.Invoke(this, loggerEventArgs);
            try
            {
                Debug.WriteLine("AclSet Started");
                AddAccessControl per = new AddAccessControl();
                if (Access)
                {
                    FSAccess = AccessControlType.Allow;
                }
                else
                {
                    FSAccess = AccessControlType.Deny;
                }

                if (Read)
                {
                    per.AddDirectorySecurity(dir, user, FileSystemRights.ReadAndExecute, FSAccess);
                }
                else if (Write)
                {
                    per.AddDirectorySecurity(dir, user, FileSystemRights.ReadAndExecute, FSAccess);
                    per.AddDirectorySecurity(dir, user, FileSystemRights.Write, FSAccess);
                }
                else if (Modify)
                {
                    per.AddDirectorySecurity(dir, user, FileSystemRights.Modify, FSAccess);
                }

                Debug.WriteLine("AclSet Finished");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}