using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;
using Brevitee.Data;

namespace Brevitee.Data.Schema
{
    public class ForeignKeyColumn: Column
    {
        /// <summary>
        /// Empty constructor provided for deserialization
        /// </summary>
        public ForeignKeyColumn()
        {
            this.ReferencedTable = string.Empty;
            this.DbDataType = string.Empty;
        }
        
        public ForeignKeyColumn(Column column, string referencedTable)
            : base(column.TableName)
        {
            this.AllowNull = column.AllowNull;
            this.Key = column.Key;
            this.Name = column.Name;
            this.Type = column.Type;
            this.ReferencedTable = referencedTable;
            this.DbDataType = column.DbDataType;
        }

        public ForeignKeyColumn(string name, string tableName, string referencedTable)
            : this(new Column(name, tableName), referencedTable)
        {

        }

        string referenceName;
        public string ReferenceName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(referenceName))
                {
                    return string.Format("FK_{0}_{1}", this.TableName, ReferencedTable);
                }
                else
                {
                    return referenceName;
                }
            }
            set
            {
                referenceName = value;
            }
        }

        public string ReferenceNameSuffix
        {
            get;
            private set;
        }

        public string ReferencedKey { get; set; }
        public string ReferencedTable { get; set; }

        public string ReferencedClass
        {
            get
            {
                return ReferencedTable.PascalCase(true, " ", "_").DropLeadingNonLetters();
            }
        }

        public string ReferencingClass
        {
            get
            {
                return TableName.PascalCase(true, " ", "_").DropLeadingNonLetters();
            }
        }

        public override string ToString()
        {
            return this.ReferenceName;
        }

        protected internal string RenderClassProperty(int i = -1, string ns = "")
        {
            if (string.IsNullOrEmpty(ReferencedClass.Trim()))
            {
                throw new InvalidOperationException("ReferencedClass cannot be null");
            }
            if (i > 0)
            {
                ReferenceNameSuffix = i.ToString();
            }
            return Column.Render<ForeignKeyColumn>(this, "ForeignKeyProperty.tmpl", new { Model = this, Namespace = ns });
        }

        protected internal string RenderListProperty()
        {
            return Column.Render<ForeignKeyColumn>(this, "DaoCollectionProperty.tmpl");
        }

        protected internal string RenderAddToChildDaoCollection()
        {
            return Column.Render<ForeignKeyColumn>(this, "ChildDaoCollectionAdd.tmpl");
        }
    }
}
