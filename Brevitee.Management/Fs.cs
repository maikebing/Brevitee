using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Brevitee;
using Brevitee.Configuration;
using Brevitee.Html;
using Brevitee.Data;
using Brevitee.ServiceProxy;
using System.Web.Mvc;
using System.Web;

namespace Brevitee.Management
{
    [Proxy("fs")]
    //[RoleRequired("Admin", "Administrator", "Root", "SuperUser")]
    public class Fs
    {
        public Fs(HttpServerUtilityBase server, string appName)
        {
            string root = server.MapPath("~/bam/apps/{appName}/".NamedFormat(new { appName = appName }));
            this.RootDir = new DirectoryInfo(root);
            this.AppName = appName;
        }

        public Fs(HttpServerUtility server, string appName)
        {
            string root = server.MapPath("~/bam/apps/{appName}/".NamedFormat(new { appName = appName }));
            this.RootDir = new DirectoryInfo(root);
            this.AppName = appName;
        }

        public Fs(string appName)
            : this(HttpContext.Current.Server, appName)
        {
        }

        public Fs(DirectoryInfo rootDir)
        {
            this.RootDir = rootDir;
        }

        public Fs(Controller controller, string appName)
            : this(controller.Server, appName)
        {
        }
     
        [Exclude]
        public static void RegisterProxy(string appName)
        {
            ServiceProxySystem.Register<Fs>(new Fs(appName));
        }

        [Exclude]
        public static void UnregisterProxy()
        {
            ServiceProxySystem.Unregister<Fs>();
        }
   
        [Exclude]
        public DirectoryInfo RootDir
        {
            get;
            set;
        }

        [Exclude]
        public string Root
        {
            get
            {
                string val = RootDir.FullName.Replace("\\", "/");
                if (!val.EndsWith("/"))
                {
                    val = string.Format("{0}/", val);
                }

                return val;
            }
        }

        string _currentDirectory;
        [Exclude]
        public string CurrentDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_currentDirectory))
                {
                    _currentDirectory = Root;
                }

                return _currentDirectory;
            }
            set
            {
                _currentDirectory = value;
            }
        }

        public string AppName { get; set; }
        
        public event FsEvent DirectoryCreated;
        private void OnDirectoryCreated(string path)
        {
            if (DirectoryCreated != null)
            {
                DirectoryCreated(path);
            }
        }

        public event FsEvent FileWritten;
        private void OnFileWritten(string path)
        {
            if (FileWritten != null)
            {
                FileWritten(path);
            }
        }

        public event FsEvent FileAppendedTo;
        private void OnFileAppendedTo(string path)
        {
            if (FileAppendedTo != null)
            {
                FileAppendedTo(path);
            }
        }

        public void CreateDirectory(string directory)
        {
            string path = GetAbsolutePath(directory);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                OnDirectoryCreated(path);
            }
        }

        public void Write(string relativeFilePath, string contentToWrite, bool overwrite = true)
        {
            string path = EnsurePath(relativeFilePath);

            path.SafeWriteFile(contentToWrite, overwrite);

            OnFileWritten(path);
        }

        private string EnsurePath(string relativeFilePath)
        {
            string path = GetAbsolutePath(EnsureRelative(relativeFilePath));
            FileInfo file = new FileInfo(path);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            return path;
        }

        public void Write(string relativeFilePath, byte[] contentToWrite)
        {
            string path = EnsurePath(relativeFilePath);
            File.WriteAllBytes(path, contentToWrite);
        }

        public byte[] ReadBytes(string relativeFilePath)
        {
            string path = GetAbsolutePath(EnsureRelative(relativeFilePath));
            return File.ReadAllBytes(path);
        }

        public string ReadAllText(string relativeFilePath)
        {
            string path = GetAbsolutePath(EnsureRelative(relativeFilePath));
            return File.ReadAllText(path);
        }

        public void Append(string relativeFilePath, string text)
        {
            string path = GetAbsolutePath(EnsureRelative(relativeFilePath));
            text.SafeAppendToFile(path);

            OnFileAppendedTo(path);
        }
        
        public bool FileExists(string relativePath)
        {
            relativePath = EnsureRelative(relativePath);

            return File.Exists(GetAbsolutePath(relativePath));
        }

        public void MoveFile(string src, string dest)
        {
            File.Move(src, dest);
        }

        public void MoveDirectory(string src, string dest)
        {
            Directory.Move(src, dest);
        }

        private static string EnsureRelative(string relativePath)
        {
            if (!relativePath.StartsWith("~"))
            {
                relativePath = string.Format("~{0}", relativePath);
            }
            return relativePath;
        }

        public bool DirectoryExists(string relativePath)
        {
            relativePath = EnsureRelative(relativePath);

            return Directory.Exists(GetAbsolutePath(relativePath));
        }

        public string[] GetDirectories(string relativePath)
        {
            relativePath = EnsureRelative(relativePath);

            DirectoryInfo dir = new DirectoryInfo(GetAbsolutePath(relativePath));
            return dir.GetDirectories().Select(d => d.Name).ToArray();
        }

        public string[] GetFiles(string relativePath, string searchPattern = "*")
        {
            relativePath = EnsureRelative(relativePath);

            DirectoryInfo dir = new DirectoryInfo(GetAbsolutePath(relativePath));
            return dir.GetFiles(searchPattern).Select(f => f.Name).ToArray();
        }

        [Exclude]
        public string GetAbsolutePath(string relativePath)
        {
            string result = "";
            if (relativePath.StartsWith("~"))
            {
                relativePath = relativePath.Substring(1, relativePath.Length - 1);
                if (relativePath.StartsWith("/"))
                {
                    relativePath = relativePath.Substring(1, relativePath.Length - 1);
                }
                string path = string.Format("{0}{1}", Root, relativePath);
                result = path;
            }
            else
            {
                result = relativePath;
            }

            return result;
        }
    }

}