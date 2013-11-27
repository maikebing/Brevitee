using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Html;
using System.Text.RegularExpressions;
using Brevitee;
using Brevitee.Data;
using Brevitee.ServiceProxy;
using Brevitee.ServiceProxy.Js;

namespace Brevitee.Data.Schema
{
    public class Column
    {
        public Column()
        {
        }

        /// <summary>
        /// Instantiate a column where Type = Long, AllowNull = false, Key = true
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tableName"></param>
        public Column(string name, string tableName)
        {
            this.Name = name;
            this.TableName = tableName;
            this.Type = DataTypes.Long;
            this.AllowNull = false;
            this.Key = true;
        }

        public Column(string name, DataTypes type, bool allowNull = true, string maxLength = "")
        {
            this.Name = name;
            this.Type = type;
            this.AllowNull = allowNull;
            this.MaxLength = maxLength;
        }

        internal Column(string tableName)
        {
            this.TableName = tableName;
        }

        [Exclude]
        public string TableName { get; set; }
        
        [Exclude]
        public string TableClassName
        {
            get
            {
                string val = TableName;
                if (!string.IsNullOrEmpty(TableName))
                {
                    val = TableName.PascalCase(true, " ", "_").DropLeadingNonLetters();
                }

                return val;
            }
        }

        string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = Regex.Replace(value, @"\s", string.Empty);
            }
        }

        /// <summary>
        /// Gets the value of the PropertyName this Column
        /// will be converted to during code generation
        /// </summary>
        public string PropertyName
        {
            get
            {
                return Name.PascalCase(true, " ", "_").DropLeadingNonLetters();
            }
        }

        [DropDown(typeof(DataTypes))]
        public DataTypes Type { get; set; }

        public string MaxLength { get; set; }

        public string NativeType
        {
            get
            {
                switch (Type)
                {
                    case DataTypes.Boolean:
                        return "bool?";
                    case DataTypes.Int:
                        return "int?";
                    case DataTypes.Long:
                        return "long?";
                    case DataTypes.Decimal:
                        return "decimal?";
                    case DataTypes.String:
                        return "string";
                    case DataTypes.Byte:
                        return "byte[]";
                    case DataTypes.DateTime:
                        return "DateTime?";
                    default:
                        return "string";
                }
            }            
        }

        string _dbDataType;
        public string DbDataType
        {
            get
            {
                if (string.IsNullOrEmpty(_dbDataType))
                {
                    switch (Type)
                    {
                        case DataTypes.SelectDataType:
                            SetDbDataype("VarChar", "4000");
                            break;
                        case DataTypes.Boolean:
                            SetDbDataype("Bit", "1");
                            break;
                        case DataTypes.Int:
                            SetDbDataype("Int", "4");
                            break;
                        case DataTypes.Long:
                            SetDbDataype("BigInt", "8");
                            break;
                        case DataTypes.Decimal:
                            SetDbDataype("Decimal", "28");
                            break;
                        case DataTypes.String:
                            SetDbDataype("VarChar", "4000");
                            break;
                        case DataTypes.Byte:
                            SetDbDataype("VarBinary", "8000");
                            break;
                        case DataTypes.DateTime:
                            SetDbDataype("DateTime", "8");
                            break;
                        default:
                            SetDbDataype("VarChar", "4000");
                            break;
                    }
                }

                return _dbDataType;
            }
            set
            {
                _dbDataType = value;
            }
        }

        private void SetDbDataype(string dbDataType, string max = null)
        {
            _dbDataType = dbDataType;
            if (string.IsNullOrEmpty(MaxLength) && !string.IsNullOrEmpty(max))
            {
                MaxLength = max;
            }
        }

        public virtual bool AllowNull
        {
            get;
            set;
        }

        [Exclude]
        public virtual bool Key
        {
            get;
            set;
        }

        internal static string Render<T>(T column, string templateName, object options = null)
        {
            if (options == null)
            {
                options = new { Model = column };
            }
            RazorParser<DaoRazorTemplate<T>> razorParser = new RazorParser<DaoRazorTemplate<T>>();
            return razorParser.ExecuteResource(templateName, options);
        }

        protected internal virtual string RenderClassProperty()
        {
            if (this.Key)
            {
                return Render<Column>(this, "KeyProperty.tmpl");
            }
            else
            {
                return Render<Column>(this, "Property.tmpl");
            }
        }

        protected internal virtual string RenderQiClassProeprty()
        {
            return Render<Column>(this, "QiProperty.tmpl");
        }

        protected internal virtual string RenderColumnsClassProperty()
        {
            return Render<Column>(this, "ColumnsProperty.tmpl");
        }

        public override int GetHashCode()
        {
            return string.Format("{0}.{1}", this.TableName, this.Name).ToLowerInvariant().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Column col = obj as Column;
            if (col != null)
            {
                return col.GetHashCode() == this.GetHashCode();
            }
            else
            {
                return base.Equals(obj);
            }
        }
    }
}
