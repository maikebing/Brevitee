using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Data.Common;
using System.Data;
using System.Collections;

namespace Brevitee.Data
{
    public static class Extensions
    {
        public static object ToJsonSafe(this object obj)
        {
            Type jsonSafeType = obj.CreateDynamicType<ColumnAttribute>(false);// CreateDynamicType<DaoColumn>(daoObject, false);
            ConstructorInfo ctor = jsonSafeType.GetConstructor(new Type[] { });
            object jsonSafeInstance = ctor.Invoke(null);//Activator.CreateInstance(jsonSafeType);
            jsonSafeInstance.CopyProperties(obj);
            return jsonSafeInstance;
        }

        public static object[] ToJsonSafe(this IEnumerable e)
        {
            List<object> returnValues = new List<object>();
            foreach (object o in e)
            {
                returnValues.Add(o.ToJsonSafe());
            }

            return returnValues.ToArray();
        }
    }
}
