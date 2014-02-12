using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Logging;
using Brevitee.CommandLine;
using System.IO;
using Brevitee;
using Brevitee.Yaml;
using Brevitee.Javascript;
using Brevitee.Configuration;

namespace Brevitee.Server
{
    /// <summary>
    /// The default BreviteeServerFactory.
    /// </summary>
    //public class DefaultBreviteeServerFactory: BreviteeServerFactory
    //{
    //    /// <summary>
    //    /// Searches the current directory of the BreviteeDaemon process for 
    //    /// a ServerConf file either with a json or yaml extension.  If either is
    //    /// found the contents are deserialized and returned with the json 
    //    /// file taking precendence.  If neither is found a ServerConf is 
    //    /// instantiated and its properties are set using 
    //    /// DefaultConfiguration.SetProperties
    //    /// </summary>
    //    /// <returns></returns>
    //    public override ServerConf LoadServerConfig()
    //    {
    //        ServerConf c = null;
    //        string jsonConfig = string.Format("./{0}.json", typeof(ServerConf).Name);

    //        if (File.Exists(jsonConfig))
    //        {
    //            c = jsonConfig.FromJsonFile<ServerConf>();
    //            c.LoadedFrom = new FileInfo(jsonConfig).FullName;
    //        }
            
    //        if (c == null)
    //        {
    //            string yamlConfig = string.Format("./{0}.yaml", typeof(ServerConf).Name);
    //            if (File.Exists(yamlConfig))
    //            {
    //                c = (ServerConf)(yamlConfig.FromYamlFile().FirstOrDefault());
    //                c.LoadedFrom = new FileInfo(yamlConfig).FullName;
    //            }
    //        }

    //        if (c == null)
    //        {
    //            c = new ServerConf();
    //            DefaultConfiguration.SetProperties(c);
    //        }

    //        return c;
    //    }

    //    /// <summary>
    //    /// Returns a MultiTargetLogger with no loggers added
    //    /// </summary>
    //    /// <returns></returns>
    //    public override Logging.ILogger GetLogger()
    //    {
    //        MultiTargetLogger logger = new MultiTargetLogger();
    //        logger.StartLoggingThread();
    //        return logger;
    //    }
    //}
}
