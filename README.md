# 

# **Automatic Fileserver deployment tool  in Windows Active Directory environment** (Alpha Release)

1.Enter domain information.
2. Write the folder name (Active Directory will use the same name to create an AD group). Enter the scheme below:
  \--FS + Folder name + ACL "FS Marketing RWX"
3. Choose access level:
- R — Read
- RW — Read + Write
- RWX — Read + Write + Modify
5. Browse the top-level directory, which is typically the file server's root directory.
Click 'Start Operation'. The software will create a new folder, AD Security Group, and assign permissions.

Program requirements:
- Latest version of PowerShell installed.
- .NET Framework 4.7.2.
- Administrative rights on both the domain and file server levels.
