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
            gFolderNamewWithFlag = string.Empty;
            gADServer = string.Empty;
            gOUpath =   string.Empty;
            @gfsloc =   string.Empty;
            gOwner =    string.Empty;
            Index =     0; 
            


        }

        private string gADServer = string.Empty;
        private string gOUpath = string.Empty;
        private string gFolderNamewWithFlag = string.Empty;
        private string @gfsloc = string.Empty;
        private string OriginalFolderName = string.Empty;
        private string gOwner = string.Empty;

        public string Accessflag { get; set; } = string.Empty;
        public bool? NTFSFlag { get; set; } = true;
        public int? Index { get; set; } = 0;


        private string TBNameBuilder(string name, string r)
        {
            string FullName = LocalFunctions.StBuilder(name, r);
            return FullName;
        }

        private void SetAccessFlags(string accessFlag, string folderName)
        {
            acl.Access = true;

            switch (accessFlag)
            {
                case "R":
                    acl.Read = true;
                    gFolderNamewWithFlag = TBNameBuilder(folderName, "R");
                    break;
                case "RW":
                    acl.Write = true;
                    gFolderNamewWithFlag = TBNameBuilder(folderName, "RW");
                    break;
                case "RWX":
                    acl.Modify = true;
                    gFolderNamewWithFlag = TBNameBuilder(folderName, "RWX");
                    break;
            }
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
        /// <summary>
        /// Auto AD Group
        /// </summary>
        //create new ACL
        private ACL acl = new ACL();

        /// <summary>
        /// Auto AD Grpup
        /// </summary>
        /// <param name="FileServerRootDirectory"></param>
        /// <param name="FolderName"></param>
        /// <param name="ADServer"></param>
        /// <param name="ActiveDirectoryOUPath"></param>

        public void DataSet(string FileServerRootDirectory, string FolderName, string ADServer, string ActiveDirectoryOUPath, string Owner)
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
            SetAccessFlags(Accessflag, FolderName);

            gADServer = ADServer;
            gOUpath = ActiveDirectoryOUPath;
            @gfsloc = SwiftIO.BulldDir(FolderName, FileServerRootDirectory);
            gOwner = Owner;
            OriginalFolderName = FolderName;
            engineEventArgs.OriginMessage = "DataSet Operation Finished";
            DataSetFinished?.Invoke(this, engineEventArgs);
            LocalFunctions.CreateAdGroup(gADServer, gOUpath, gFolderNamewWithFlag, @gfsloc, string.Empty);

            acl.SetPermissions(@gfsloc, gFolderNamewWithFlag);
        {
            
            Debug.WriteLine("Main Operation Started");
            LocalFunctions.CreadeAdGroup(gADServer, gOUpath, gFolderNamewWithFlag, @gfsloc, string.Empty);
            SwiftIO.CreateFolder(@gfsloc);
            acl.Set(@gfsloc, gFolderNamewWithFlag);
            Debug.WriteLine("Main Operation Finished");
            

        }
    }
}