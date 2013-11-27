using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.DaoRef
{
    public class DaoReferenceObjectColumns : QueryFilter<DaoReferenceObjectColumns>, IFilterToken
    {
        public DaoReferenceObjectColumns() { }
        public DaoReferenceObjectColumns(string columnName)
            : base(columnName)
        { }

        public DaoReferenceObjectColumns KeyColumn
        {
            get
            {
                return new DaoReferenceObjectColumns("Id");
            }
        }

        public DaoReferenceObjectColumns Id
        {
            get
            {
                return new DaoReferenceObjectColumns("Id");
            }
        }
        public DaoReferenceObjectColumns IntProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("IntProperty");
            }
        }
        public DaoReferenceObjectColumns DecimalProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("DecimalProperty");
            }
        }
        public DaoReferenceObjectColumns LongProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("LongProperty");
            }
        }
        public DaoReferenceObjectColumns DateTimeProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("DateTimeProperty");
            }
        }
        public DaoReferenceObjectColumns BoolProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("BoolProperty");
            }
        }
        public DaoReferenceObjectColumns GuidProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("GuidProperty");
            }
        }
        public DaoReferenceObjectColumns DoubleProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("DoubleProperty");
            }
        }
        public DaoReferenceObjectColumns ByteArrayProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("ByteArrayProperty");
            }
        }
        public DaoReferenceObjectColumns StringProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("StringProperty");
            }
        }


        public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
    }
}