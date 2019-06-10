using System.IO;
using System.Security.AccessControl;

// Copyright Hayk Jomardyan Beta relase

namespace SwiftNTFS
{
    /// <summary>
    /// Set directory permission
    /// </summary>
    internal class AddAccessControl
    {
        public void AddDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            DirectoryInfo dInfo = new DirectoryInfo(FileName);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account, Rights,
            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
            PropagationFlags.None,
            ControlType));

            dInfo.SetAccessControl(dSecurity);
        }
    }
}