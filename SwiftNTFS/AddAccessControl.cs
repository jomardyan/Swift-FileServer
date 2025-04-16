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
        public readonly string name = "AddAccessControl";

        public void AddDirectorySecurity(string directoryPath, string account, FileSystemRights rights, AccessControlType controlType)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentException("Directory path cannot be null or empty.", nameof(directoryPath));

            if (string.IsNullOrWhiteSpace(account))
                throw new ArgumentException("Account cannot be null or empty.", nameof(account));

            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException($"The directory '{directoryPath}' does not exist.");

            DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();
            directorySecurity.AddAccessRule(new FileSystemAccessRule(account, rights,
                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.None,
                controlType));

            directoryInfo.SetAccessControl(directorySecurity);
        }
    }
}