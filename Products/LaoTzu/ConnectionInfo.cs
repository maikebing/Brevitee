using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LaoTzu
{
    public class ConnectionInfo
    {
        public ConnectionInfo(ConnectionStringSettings setting)
        {
            this.Name = setting.Name;
            this.ConnectionString = setting.ConnectionString;
        }

        public string Name { get; set; }
        public string ConnectionString { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
