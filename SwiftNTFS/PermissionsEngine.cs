//MIT License

//Copyright(c) 2019 Hayk Jomardyan

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.


using CommonFeatures;
using System;
using System.Diagnostics;

namespace SwiftNTFS
{
    

    //SetAllPermissions
    /// <summary>
    /// </summary>
    public class PermissionsEngine
    {
        public readonly string name = "Engine";
        public event EventHandler<EngineEventArgs> DataSetStarted;
        public event EventHandler<EngineEventArgs> DataSetFinished;

        public PermissionsEngine()
        {
            
                gADServer = null;
                gOUpath = null;
                gFolderNamewWithFlag = null;
                @gfsloc = null;
                Index = null;
            


        }

        private string gADServer, gOUpath, gFolderNamewWithFlag, @gfsloc, OriginalFolderName;
        public string Accessflag { get; set; }
        public bool? NTFSFlag = true;
        public int? Index;

        private string TBNameBuilder(string name, string r)
        {
            string FullName = LocalFunctions.StBuilder(name, r);
            return FullName;
        }
        /// <summary>
        /// Start Giving permission operation
        /// </summary>
        /// <param name="fsloc">File Server root Folder.</param>
        /// <param name="FolderName">
        /// Folder neme, whinch will be used as a Folder name and on AD SG Name.
        /// </param>
        /// <param name="checkBox">System.Windows.Controls.CheckBox Name</param>
        /// <param name="ADuser">AD user or group whom will be granted access to FS Filder</param>
        /// <param name="ADServer">Active Directory domain name.</param>
        /// <param name="OUpath">Active directory Organisational Unit Path</param>

        //create new ACL
        private ACL acl = new ACL();

        /// <summary>
        /// Auto AD Grpup
        /// </summary>
        /// <param name="FileServerRootDirectory"></param>
        /// <param name="FolderName"></param>
        /// <param name="ADServer"></param>
        /// <param name="ActiveDirectoryOUPath"></param>

        public void DataSet(string FileServerRootDirectory, string FolderName, string ADServer, string ActiveDirectoryOUPath)
        {
            #region StartEvents
            EngineEventArgs engineEventArgs = new EngineEventArgs
            {
                OriginName = name,
                OriginMessage = "DataSet Operation Started"
            };
            DataSetStarted?.Invoke(this, engineEventArgs);
            #endregion
            //
            // Set access
            acl.Access = true;
            string FolderNamewWithFlag = "";
            if (Accessflag == "R")
            {
                acl.Read = true;
                FolderNamewWithFlag = TBNameBuilder(FolderName, "R");
            }
            else if (Accessflag == "RW")
            {
                acl.Write = true;

                FolderNamewWithFlag = TBNameBuilder(FolderName, "RW");
            }
            else if (Accessflag == "RWX")
            {
                acl.Modify = true;
                FolderNamewWithFlag = TBNameBuilder(FolderName, "RWX");
            }

            gADServer = ADServer;
            gOUpath = ActiveDirectoryOUPath;
            gFolderNamewWithFlag = FolderNamewWithFlag;
            @gfsloc = SwiftIO.BulldDir(FolderName, FileServerRootDirectory);
            OriginalFolderName = FolderName;
            engineEventArgs.OriginMessage = "DataSet Operation Finished";
            DataSetFinished?.Invoke(this, engineEventArgs);
        }

        public void Start()
        {
            
            Debug.WriteLine("Main Operation Started");
            LocalFunctions.CreadeAdGroup(gADServer, gOUpath, gFolderNamewWithFlag, @gfsloc);
            SwiftIO.CreateFolder(@gfsloc);
            acl.Set(@gfsloc, gFolderNamewWithFlag);
            Debug.WriteLine("Main Operation Finished");
            

        }
    }
}