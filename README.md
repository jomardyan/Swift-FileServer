# 
[![Build Status](https://jomardyan.visualstudio.com/SwiftFileServerGithub/_apis/build/status/jomardyan.Swift-FileServer?branchName=master)](https://jomardyan.visualstudio.com/SwiftFileServerGithub/_build/latest?definitionId=4&branchName=master)
![Github Action](https://github.com/jomardyan/Swift-FileServer/actions/workflows/dotnet-desktop.yml/badge.svg)
# **Automatic Fileserver Deployment Tool in Windows Active Directory Environment** (Alpha Release)

This tool simplifies the process of deploying a file server in a Windows Active Directory environment by automating folder creation, security group setup, and permission assignment.

## Features

1. **Domain Information Input**: Provide the necessary domain details.
2. **Folder Creation**: Specify the folder name using the following format:
  - `FS + Folder Name + ACL` (e.g., `FS Marketing RWX`).
  - The tool will use this name to create an Active Directory (AD) group.
3. **Access Level Selection**:
  - `R` — Read
  - `RW` — Read + Write
  - `RWX` — Read + Write + Modify
4. **Directory Browsing**: Select the top-level directory, typically the root directory of the file server.
5. **Automated Operations**: Click 'Start Operation' to:
  - Create a new folder.
  - Set up an AD Security Group.
  - Assign the appropriate permissions.

## Requirements

- **PowerShell**: Ensure the latest version is installed.
- **.NET Framework**: Version 4.7.2 or higher.
- **Administrative Rights**:
  - On the domain level.
  - On the file server level.

This tool is designed to streamline file server deployment, making it faster and more efficient for administrators.
