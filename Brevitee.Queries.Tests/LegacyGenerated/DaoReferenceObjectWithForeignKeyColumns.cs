using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;
using Brevitee.Data;

namespace Brevitee.DaoReferenceObjects.Data
{
    public class DaoReferenceObjectWithForeignKeyColumns: QueryFilter<DaoReferenceObjectWithForeignKeyColumns>, IFilterToken
    {
        public DaoReferenceObjectWithForeignKeyColumns() { }
        public DaoReferenceObjectWithForeignKeyColumns(string columnName)
            : base(columnName)
        { }

        public DaoReferenceObjectWithForeignKeyColumns Id
        {
            get
            {
                return new DaoReferenceObjectWithForeignKeyColumns("Id");
            }
        }
        public DaoReferenceObjectWithForeignKeyColumns Name
        {
            get
            {
                return new DaoReferenceObjectWithForeignKeyColumns("Name");
            }
        }

        public DaoReferenceObjectWithForeignKeyColumns DaoReferenceObjectId
        {
            get
            {
                return new DaoReferenceObjectWithForeignKeyColumns("DaoReferenceObjectId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.Operator;
        }
	}
}