using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Naizari.Extensions;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
//using Naizari.Testing;
using System.IO;
//using Naizari.Data;
//using Naizari.Helpers;
using Brevitee.Data;
using Brevitee;
using Brevitee.Testing;

namespace Brevitee.Data.Tests
{
    public class BookTests: CommandLineTestInterface
    {
        [UnitTest]
        public static void DaoCollectionShouldBeEnumarable()
        {
            DaoCollection<TableColumns, TestDao> collection = TestProgram.CreateTestDaoCollection();
            Expect.AreEqual(3, collection.PageCount);
            int count = 0;
            foreach (TestDao dao in collection)
            {
                Expect.IsNotNullOrEmpty(dao.GetCurrentValue("Name").ToString());
                count++;
            }

            Expect.AreEqual(25, count);

            //foreach(List<TestDao> page in collection[
        }

       
    }
}
