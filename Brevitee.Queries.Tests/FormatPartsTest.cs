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
    public class FormatPartsTest: CommandLineTestInterface
    {
        [UnitTest]
        public static void FormatPartsShouldHaveValidStartAndEnd()
        {
            SetFormat setFormat = new SetFormat();
            setFormat.AddAssignment("monkey", "bananas");
            Expect.AreEqual(1, setFormat.StartNumber);
            Expect.AreEqual(2, setFormat.NextNumber);

            Out(setFormat.Parse());
        }

        [UnitTest]
        public static void FormatPartsShouldTrackParameters()
        {
            SetFormat setFormat = new SetFormat();
            setFormat.AddAssignment("monkey", "bananas");
            setFormat.AddAssignment("blah", "kasdfl");
            Expect.AreEqual(1, setFormat.StartNumber);
            Expect.AreEqual(3, setFormat.NextNumber);

            Out(setFormat.Parse());
        }

    }
}
