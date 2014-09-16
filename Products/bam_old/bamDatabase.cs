using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.CodeDom.Compiler;

using Brevitee.CommandLine;
using Brevitee;
using Brevitee.Testing;
using Brevitee.Data;
using Brevitee.Data.Schema;
using Brevitee.Encryption;
using Brevitee.Yaml;
using Brevitee.Javascript;
using Brevitee.Configuration;

using EcmaScript.NET;
using EcmaScript.NET.Types.Cli;
using EcmaScript.NET.Types;


public partial class bam
{
    static DefaultBamConnectionStringResolver _connectionStringResolver;

    private DefaultBamConnectionStringResolver ConnectionStringResolver
    {
        get
        {
            if (_connectionStringResolver == null)
            {
                _connectionStringResolver = new DefaultBamConnectionStringResolver(this.Fs);
            }

            return _connectionStringResolver;
        }
    }

    private void WriteDatabaseSchema(string conx)
    {
        ConnectionStringResolvers.AddResolver(ConnectionStringResolver);
        SetDatabaseInitializer();

        //string conx = GetArgVal("connection");
        conx = string.IsNullOrEmpty(conx) ? "Default" : conx;
        DirectoryInfo daoDir = new DirectoryInfo(GetAbsolutePath("~/models/dao/dll"));

        FileInfo[] dlls = daoDir.GetFiles("*.dll");
        List<Type> analyzedTypes = new List<Type>();
        List<string> doneConnectionNames = new List<string>();
        
        int l = dlls.Length;
        for (int i = 0; i < l; i++)
        {
            FileInfo file = dlls[i];
            OutFormat("Analyzing {0} for dao types...", ConsoleColor.Green, file.FullName);
            Assembly assembly = Assembly.LoadFrom(file.FullName);
            Type[] types = assembly.GetTypes();
            int il = types.Length;
            for (int ii = 0; ii < il; ii++)
            {
                Type type = types[ii];
                if (!analyzedTypes.Contains(type))
                {
                    analyzedTypes.Add(type);
                    TableAttribute tableAttr = null;
                    if (type.HasCustomAttributeOfType<TableAttribute>(out tableAttr))
                    {
                        if (!doneConnectionNames.Contains(tableAttr.ConnectionName) && tableAttr.ConnectionName.Equals(conx))
                        {                            
                            doneConnectionNames.Add(conx);
                            OutFormat("Writing schema for {0}", ConsoleColor.Yellow, conx);
                            Db.EnsureSchema(type);
                        }
                    }
                }
            }
        }
    }
}

