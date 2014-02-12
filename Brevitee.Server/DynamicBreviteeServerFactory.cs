using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Server
{
    /// <summary>
    /// Represents a server factory that can have the configuration and logger
    /// retrieved the speicified GetConfigFunction and GetLoggerFunction
    /// </summary>
    //public class DynamicBreviteeServerFactory: BreviteeServerFactory
    //{
    //    /// <summary>
    //    /// The function used to retrieve the Conf(iguration)
    //    /// </summary>
    //    public Func<ServerConf> GetConfigFunction
    //    {
    //        get;
    //        set;
    //    }

    //    public override ServerConf LoadServerConfig()
    //    {
    //        return GetConfigFunction();
    //    }

    //    /// <summary>
    //    /// The function used to retrieve the Logger
    //    /// </summary>
    //    public Func<Logging.ILogger> GetLoggerFunction
    //    {
    //        get;
    //        set;
    //    }

    //    public override Logging.ILogger GetLogger()
    //    {
    //        return GetLoggerFunction();
    //    }
    //}
}
