using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.DaoRef
{
    public class TestTableColumns: QueryFilter<TestTableColumns>, IFilterToken
    {
        public TestTableColumns() { }
        public TestTableColumns(string columnName)
            : base(columnName)
        { }
		
		public TestTableColumns KeyColumn
		{
			get
			{
				return new TestTableColumns("Id");
			}
		}	
				
        public TestTableColumns Id
        {
            get
            {
                return new TestTableColumns("Id");
            }
        }
        public TestTableColumns Name
        {
            get
            {
                return new TestTableColumns("Name");
            }
        }
        public TestTableColumns Description
        {
            get
            {
                return new TestTableColumns("Description");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}