using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Brevitee.Logging;

namespace Brevitee.Server
{
    public class ContentLocator
    {
        const string FileName = "contentLocator.json";
        private ContentLocator()
        {
            this._searchRules = new List<SearchRule>();
        }

        private ContentLocator(Fs rootToCheck)
            : this()
        {
            this.RootToCheck = rootToCheck;
        }

        public bool Locate(string path, out string outPath, out string[] checkedPaths)
        {
            string ext = Path.GetExtension(path).ToLowerInvariant();
            string foundPath = string.Empty;
            string checkNext = "~" + path;
            Fs fs = RootToCheck;
            List<string> pathsChecked = new List<string>();
            pathsChecked.Add(checkNext);

            if(fs.FileExists(checkNext))
            {
                foundPath = checkNext;
            }
            else
            {
                SearchRule[] extRules = _searchRules.Where(sr => sr.Ext.ToLowerInvariant().Equals(ext)).ToArray();
                extRules.Each(rule =>
                {
                    rule.SearchDirectories.Each(dir =>
                    {
                        string subPath = path.StartsWith("/") ? path.TruncateFront(1) : path;
                        checkNext = Path.Combine(dir, subPath).Replace("\\", "/");
                        pathsChecked.Add(checkNext);

                        if (fs.FileExists(checkNext))
                        {
                            foundPath = checkNext;                            
                            return false; // stop the each loop
                        }

                        return true; // continue the each loop
                    });

                    if (!string.IsNullOrEmpty(foundPath))
                    {
                        return false; // stop the each loop
                    }

                    return true; // continue the each loop
                });
            }
            
            checkedPaths = pathsChecked.ToArray();
            outPath = foundPath;
            return !string.IsNullOrEmpty(foundPath);
        }

        protected Fs RootToCheck
        {
            get;
            set;
        }

        public void Save()
        {
            string json = this.ToJson(true);
            RootToCheck.WriteFile(FileName, json);
        }

        public static ContentLocator Load(AppContentResponder appContent)
        {
            ContentLocator locator = Load(appContent.AppRoot);
            locator.AppName = appContent.AppConf.Name;
            return locator;
        }

        public static ContentLocator Load(Fs rootToCheck)
        {            
            ContentLocator locator = null;
            if (rootToCheck.FileExists(FileName))
            {
                FileInfo file = rootToCheck.GetFile(FileName);//new FileInfo(filePath);
                locator = file.FromJsonFile<ContentLocator>();
            }
            else
            {
                locator = new ContentLocator(rootToCheck);
                string[] imageDirs = new string[] { "~/img", "~/image" };
                locator.AddSearchRule(".png", imageDirs);
                locator.AddSearchRule(".gif", imageDirs);
                locator.AddSearchRule(".jpg", imageDirs);

                string[] pageDirs = new string[] { "~/pages" };
                locator.AddSearchRule(".htm", pageDirs);
                locator.AddSearchRule(".html", pageDirs);

                string[] cssDirs = new string[] { "~/css" };
                locator.AddSearchRule(".css", cssDirs);

                locator.Save();
            }

            locator.RootToCheck = rootToCheck;
            return locator;
        }

        public string AppName
        {
            get;
            private set;
        }

        public void AddSearchRule(string ext, params string[] relativeRootDirsToSearch)
        {
            SearchDirectory[] searchDirs = new SearchDirectory[relativeRootDirsToSearch.Length];
            relativeRootDirsToSearch.Each((dir, i) =>
            {
                searchDirs[i] = new SearchDirectory(i + 1, dir);
            });

            AddSearchRule(new SearchRule(ext, searchDirs));
        }
        public void AddSearchRule(SearchRule rule)
        {
            _searchRules.Add(rule);
            Save();
        }

        List<SearchRule> _searchRules;
        public SearchRule[] SearchRules
        {
            get
            {
                return _searchRules.ToArray();
            }
            set
            {
                _searchRules = new List<SearchRule>(value);
            }
        }
    }
}
