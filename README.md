# **Automatic Fileserver deployment tool  in Windows Active Directory environment** (Alpha Release) 

[![Build Status](https://jomardyan.visualstudio.com/SwiftFileServerGithub/_apis/build/status/jomardyan.Swift-FileServer?branchName=master)](https://jomardyan.visualstudio.com/SwiftFileServerGithub/_build/latest?definitionId=4&branchName=master)

1. Enter domain information.
2. Write the folder name (Active Directory will use the same name to create an AD group). Enter the scheme below:
  1. --FS + Folder name + ACL &quot;FS Marketing RWX&quot;
3. Chose access Level
  1.-- R—Read
  2.-- RW—Read + Write
  3.-- RWX— Read + Write + Modify
4. Browse the top-level directory, which is typically the file server's root directory.
Click 'Start Operation'. The software will create a new folder, AD Security Group, and assign permissions.

Program requirements
1. Latest powershell  installed
2. .NET Framework 4.7.2
3. Administrative rights on Domain and FileServer level
