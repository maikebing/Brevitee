using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;
using Brevitee.Yaml;
using Brevitee.Configuration;
using Brevitee.Data;

namespace Brevitee.Server
{
    public class Conf
    {
        public Conf()
        {
            this.Fs = new Fs(".");
            this.DatabaseInitializer = typeof(SQLiteDatabaseInitializer).AssemblyQualifiedName;
        }

        public Conf(Fs fs)
        {
            this.Fs = fs;
        }

        protected Fs Fs
        {
            get;
            private set;
        }

        /// <summary>
        /// The AssemblyQualifiedName of the 
        /// </summary>
        public string DatabaseInitializer
        {
            get;
            set;
        }

        public string ConnectionName
        {
            get;
            set;
        }

        string _port;
        public string Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        string _maxThreads;
        public string MaxThreads
        {
            get
            {
                return _maxThreads;
            }
            set
            {
                _maxThreads = value;
            }
        }

        public string HostName
        {
            get;
            set;
        }

        string _applicationName;
        public string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationName))
                {
                    _applicationName = DefaultConfiguration.GetAppSetting("ApplicationName", typeof(AppServer).Name);
                }

                return _applicationName.PascalCase(true, " ", "_");
            }
            set
            {
                _applicationName = value.PascalCase(true, " ", "_");
            }
        }

        public void Save()
        {
            Fs.WriteFile(string.Format("~/{0}.yaml", typeof(Conf).Name), this.ToYaml());
        }
    }
}
