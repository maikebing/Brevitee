using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Brevitee;
using Brevitee.Configuration;
using Brevitee.Html;
using System.Web.Mvc;

namespace Brevitee.Server
{
    public class Fs
    {
        public Fs(string root)
        {
            this.RootDir = new DirectoryInfo(root);
        }

        public DirectoryInfo RootDir
        {
            get;
            set;
        }

        /// <summary>
        /// The full physical path to the root of the current Fs (FileSystem)
        /// </summary>
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
            set
            {
                this.RootDir = new DirectoryInfo(value);
            }
        }

        public event FsEvent DirectoryExists;
        private void OnDirectoryExists(string path)
        {
            if (DirectoryExists != null)
            {
                DirectoryExists(path);
            }
        }

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
            else
            {
                OnDirectoryExists(path);
            }
        }

        public void createDirectory(string directory)
        {
            CreateDirectory(directory);
        }

        public void md(string directory)
        {
            CreateDirectory(directory);
        }

        public void mkdir(string directory)
        {
            CreateDirectory(directory);
        }

        public void WriteFile(string relativeFilePath, string contentToWrite, bool overwrite = true)
        {
            string path = GetAbsolutePath(EnsureRelative(relativeFilePath));
            FileInfo file = new FileInfo(path);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            path.SafeWriteFile(contentToWrite, overwrite);

            OnFileWritten(path);
        }

        public byte[] ReadBytes(params string[] pathSegments)
        {
            return ReadBytes(Path.Combine(pathSegments));
        }

        public byte[] ReadBytes(string relativeFilePath)
        {
            string path = GetAbsolutePath(EnsureRelative(relativeFilePath));
            return File.ReadAllBytes(path);
        }

        public string ReadAllText(params string[] pathSegments)
        {
            return ReadAllText(Path.Combine(pathSegments));
        }

        public string ReadAllText(string relativeFilePath)
        {
            string path = GetAbsolutePath(EnsureRelative(relativeFilePath));
            return File.ReadAllText(path);
        }

        public string readAllText(string relativeFilePath)
        {
            return ReadAllText(relativeFilePath);
        }

        public void write(string relativeFilePath, string text)
        {
            WriteFile(relativeFilePath, text);
        }

        public void AppendToFile(string relativeFilePath, string text)
        {
            string path = GetAbsolutePath(relativeFilePath);
            text.SafeAppendToFile(path);

            OnFileAppendedTo(path);
        }

        public void append(string relativeFilePath, string text)
        {
            AppendToFile(relativeFilePath, text);
        }

        public bool FileExists(params string[] pathSegmentsToCombine)
        {
            return FileExists(Path.Combine(pathSegmentsToCombine));
        }

        /// <summary>
        /// Determines if the specified relativePath exists
        /// in the current Fs.  If the specified path is not
        /// prefixed by a ~ it will be adjusted prior to 
        /// checking for the existence of the file
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public bool FileExists(string relativePath)
        {
            relativePath = EnsureRelative(relativePath);

            return File.Exists(GetAbsolutePath(relativePath));
        }

        private static string EnsureRelative(string relativePath)
        {
            if (!relativePath.StartsWith("~"))
            {
                relativePath = string.Format("~{0}", relativePath);
            }
            return relativePath;
        }

        public bool DirExists(string relativePath)
        {
            relativePath = EnsureRelative(relativePath);

            return Directory.Exists(GetAbsolutePath(relativePath));
        }

        public DirectoryInfo GetDirectory(string relativePath)
        {
            relativePath = EnsureRelative(relativePath);

            return new DirectoryInfo(GetAbsolutePath(relativePath));
        }

        public FileInfo GetFile(string relativePath)
        {
            relativePath = EnsureRelative(relativePath);

            return new FileInfo(GetAbsolutePath(relativePath));
        }

        public FileInfo[] GetFiles(string relativePath, string searchPattern = "*")
        {
            relativePath = EnsureRelative(relativePath);

            DirectoryInfo dir = new DirectoryInfo(GetAbsolutePath(relativePath));
            if(dir.Exists)
            {
                return dir.GetFiles(searchPattern);
            }
            else
            {
                return new FileInfo[] { };
            }
        }

        public string GetAbsolutePath(params string[] segments)
        {
            return GetAbsolutePath(Path.Combine(segments));
        }

        public string GetAbsolutePath(string relativePath)
        {
            string result = "";
            if (relativePath.StartsWith("~"))
            {
                relativePath = relativePath.Substring(1, relativePath.Length - 1);
                if (relativePath.StartsWith("/") ||
                    relativePath.StartsWith("\\"))
                {
                    relativePath = relativePath.Substring(1, relativePath.Length - 1);
                }
                string path = string.Format("{0}{1}", Root, relativePath).Replace("\\", "/");
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