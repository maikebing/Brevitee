using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data;
using Brevitee.Configuration;
using System.Configuration;

public class DefaultBamConnectionStringResolver: IConnectionStringResolver
{
    public DefaultBamConnectionStringResolver(Fs fs)
    {
        this.Fs = fs;
    }

    public Fs Fs { get; private set; }
    #region IConnectionStringResolver Members

    public ConnectionStringSettings Resolve(string connectionName)
    {
        ConnectionStringSettings s = new ConnectionStringSettings();
        s.ProviderName = SQLiteRegistrar.SQLiteAssemblyQualifiedName();//"System.Data.SQLite.SQLiteFactory, System.Data.SQLite, Version=1.0.82.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139";
        string dbFile = Fs.GetAbsolutePath(string.Format("~/app_data/{0}.sqlite", connectionName));
        s.ConnectionString = string.Format("Data Source={0};Version=3;", dbFile);

        return s;
    }

    #endregion
}

