# **Automatic Fileserver deployment tool  in Windows Active Directory environment** (Alpha Release) 

1. Fill domain information
2. Write Folder name (Active directory will use the same name to create AD Group) Scheme below
  1. --FS + Folder name + ACL &quot;FS Marketing RWX&quot;
3. Chose access Level
  1.-- R—Read
  2.-- RW—Read + Write
  3.-- RWX— Read + Write + Modify
4. Browse top level directory, typically it&#39;s the fileserver root directory.
5. Click Start Operation. Software will create New folder, AD Security Group and will assign Permission

Program requirements. 
1. Latest powershell version installed. 
2. .NET Framework 4.7.2
3. Administrative rights of Domain ans FileServer. 
