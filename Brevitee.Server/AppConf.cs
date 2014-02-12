using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Brevitee.Server
{
    public class AppConf
    {
        public AppConf() { }
        public AppConf(string name)
        {
            this.DefaultLayout = "basic";
            this.Name = name;
            this.GenerateDao = true;
        }

        public AppConf(BreviteeConf serverConf, string name)
            : this(name)
        {
            this.BreviteeConf = serverConf;
        }

        internal BreviteeConf BreviteeConf
        {
            get;
            set;
        }

        Fs _appRoot;
        object _appRootLock = new object();
        internal Fs AppRoot
        {
            get
            {
                return _appRootLock.DoubleCheckLock(ref _appRoot, () =>
                {
                    return new Fs(Path.Combine(BreviteeConf.ContentRoot, "apps", Name));
                });
            }
        }

        public string Name { get; set; }
        
        public string DefaultLayout { get; set; }

        public bool GenerateDao { get; set; }

        public bool CheckDaoHashes { get; set; }

        public static string AppNameFromUri(Uri uri)
        {
            return uri.Authority.DelimitSplit(":")[0];
        }
    }
}
