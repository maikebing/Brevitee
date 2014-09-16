using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;
using Brevitee.Data.Schema;
using Brevitee;
using System.Reflection;

namespace Brevitee.Data.Repositories
{
    public class PocoDaoGenerator
    {
        DaoGenerator _generator;
        UuidSchemaManager _schemaManager;
        public PocoDaoGenerator()
        {
            _generator = new DaoGenerator();
            _schemaManager = new UuidSchemaManager();
        }

        public bool AddAuditFields { get; set; }

        public bool IncludeModifiedBy { get; set; }

        //Boolean,
        //Int,
        //Long,
        //Decimal,
        //String,
        ///// <summary>
        ///// The field will be generated as a byte array (byte[])
        ///// </summary>
        //Byte,
        //DateTime

        public IRepository GetRepositoryForTypes(IEnumerable<Type> types)
        {
            if (AddAuditFields)
            {
                _schemaManager.PostColumnAugmentations.Add(new AddAuditColumnsAugmentation { IncludeModifiedBy = IncludeModifiedBy });
            }

            foreach (Type type in types)
            {
                string tableName = "{0}Dao"._Format(type.Name);
                foreach (PropertyInfo property in type.GetProperties())
                {
                    DataTypes dataType = DataTypes.String;
                    if (property.PropertyType == typeof(bool))
                    {
                        dataType = DataTypes.Boolean;
                    }
                    else if(property.PropertyType == typeof(int))
                    {
                        dataType = DataTypes.Int;
                    }
                    else if (property.PropertyType == typeof(long))
                    {
                        dataType = DataTypes.Long;
                    }
                    else if (property.PropertyType == typeof(decimal))
                    {
                        dataType = DataTypes.Decimal;
                    }
                    else if (property.PropertyType == typeof(byte[]))
                    {
                        dataType = DataTypes.Byte;
                    }
                    else if (property.PropertyType == typeof(DateTime))
                    {
                        dataType = DataTypes.DateTime;
                    }
                }
            }

            throw new NotImplementedException();
        }

        protected internal IEnumerable<TypeXref> GetTypeXrefsFor(Type type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the types for each IEnumerable property of the specified type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected internal IEnumerable<Type> GetOneToManyTypesFor(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
