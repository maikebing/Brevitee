using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.DaoRef
{
    public class TestFkTableColumns: QueryFilter<TestFkTableColumns>, IFilterToken
    {
        public TestFkTableColumns() { }
        public TestFkTableColumns(string columnName)
            : base(columnName)
        { }
		
		public TestFkTableColumns KeyColumn
		{
			get
			{
				return new TestFkTableColumns("Id");
			}
		}	
				
        public TestFkTableColumns Id
        {
            get
            {
                return new TestFkTableColumns("Id");
            }
        }
        public TestFkTableColumns Name
        {
            get
            {
                return new TestFkTableColumns("Name");
            }
        }

        public TestFkTableColumns TestTableId
        {
            get
            {
                return new TestFkTableColumns("TestTableId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}