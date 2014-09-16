using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Data.MsSql
{
	public class SchemaExtractorEventArgs: EventArgs
	{
		public SchemaExtractorEventArgs() {}
		public Microsoft.SqlServer.Management.Smo.Table Table { get; set; }
		public Microsoft.SqlServer.Management.Smo.Column Column { get; set; }
	}
}
