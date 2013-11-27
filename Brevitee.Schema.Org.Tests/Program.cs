using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Brevitee.CommandLine;
using Brevitee;
using Brevitee.Testing;
using Brevitee.Encryption;
using System.Net;
using CsQuery;
using Sdo = Brevitee.Schema.Org;
using System.Xml;

namespace Brevitee.Schema.Org.Tests
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            PreInit();
            Initialize(args);
        }

        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true then only the name is necessary.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion
        }

        Queue<SpecificType> types;
        [ConsoleAction("Generate Schema.org.cs files")]
        public void Generate()
        {
            if (!Directory.Exists("C:\\Schema.org"))
            {
                Directory.CreateDirectory("C:\\Schema.org");
            }

            types = new Queue<SpecificType>();
            currentType = new SpecificType { TypeName = "Thing", Extends = "DataType" };
            types.Enqueue(currentType);

            while (types.Count > 0)
            {
                currentType = types.Dequeue();
                string content = GetCsCode();
                Write(content);
                foreach (SpecificType specific in GetSpecificTypes(currentType.TypeName))
                {
                    types.Enqueue(specific);
                }
            }
        }

        private void Write(string content)
        {
            File.WriteAllText("c:\\Schema.org\\" + currentType.TypeName + ".cs", content);
        }

        static SpecificType currentType;
        private static string GetCsCode()
        {
            if (string.IsNullOrEmpty(currentType.TypeName))
            {
                return string.Empty;
            }

            Property[] properties = GetProperties(currentType.TypeName);

            StringBuilder result = new StringBuilder();
            result.AppendLine("using System;");
            result.AppendLine();
            result.AppendLine("namespace Brevitee.Schema.Org");
            result.AppendLine("{");
            result.AppendFormat("\t///<summary>{0}</summary>\r\n", GetTypeDescription(currentType.TypeName));
            result.AppendFormat("\tpublic class {0}", currentType.TypeName);
            if (!string.IsNullOrEmpty(currentType.Extends))
            {
                result.AppendFormat(": {0}", currentType.Extends);
            }
            result.AppendLine();
            result.AppendLine("\t{");
            foreach (Property prop in properties)
            {
                result.AppendFormat("\t\t///<summary>{0}</summary>\r\n", prop.Description);
                result.AppendFormat("\t\tpublic {0} {1} {{get; set;}}\r\n", prop.ExpectedType, prop.Name.PascalCase());
            }
            result.AppendLine("\t}");
            result.AppendLine("}");

            return result.ToString();
        }

        private static string GetTypeDescription(string typeName)
        {
            string html = GetHtml(typeName);
            CQ cq = CQ.Create(html);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(string.Format("<main>{0}</main>", cq["body"].Html().Replace("<br>", "").Replace("<br/>", "").Replace("<br />", "").Replace("<hr>", "").Replace("<hr/>", "").Replace("<hr />", "")));

            string result = string.Empty;
            bool found = false;
            foreach (XmlNode node in xmlDoc.ChildNodes)
            {
                foreach (XmlNode _node in node.ChildNodes)
                {
                    if (!found && _node.Attributes["id"].Value.Equals("mainContent"))
                    {
                        foreach (XmlNode __node in _node.ChildNodes)
                        {
                            if (__node.NodeType == XmlNodeType.Text)
                            {
                                result = __node.Value;
                                found = true;
                                break;
                            }
                        }
                    }

                    if (found)
                    {
                        break;
                    }
                }
            }

            return result.Trim().Replace("\n", "\n///");
        }

        private static IEnumerable<XmlNode> NextChild(XmlNode node)
        {
            XmlNodeList list = node.ChildNodes;

            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                yield return node.ChildNodes[i];
            }
        }

        private static Property[] GetProperties(string typeName)
        {
            string html = GetHtml(typeName);
            CQ cq = CQ.Create(html);
            CQ propBody = cq[string.Format(".definition-table .supertype-name a[href={0}]", typeName)].First().ParentsUntil(".definition-table").Next();
            List<Property> properties = new List<Property>();
            cq["tr", propBody].Each((row) =>
            {
                string propName = cq[".prop-nam", row].Children().First().Text();
                string expectedType = cq[".prop-ect", row].Text().Trim();
                string description = cq[".prop-desc", row].Text().Trim();
                properties.Add(new Property { Name = propName.PascalCase(), ExpectedType = expectedType, Description = description });
            });

            return properties.ToArray();
        }

        private static string GetHtml(string typeName)
        {
            string baseUrl = "http://schema.org/";
            WebClient client = new WebClient();
            string html = client.DownloadString(string.Format("{0}{1}", baseUrl, typeName));
            return html;
        }

        public class SpecificType
        {
            public string TypeName { get; set; }
            public string Extends { get; set; }
        }

        /// <summary>
        /// Gets the extenders of the specified typeName
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static SpecificType[] GetSpecificTypes(string typeName)
        {
            string html = GetHtml(typeName);
            CQ cq = CQ.Create(html);
            List<SpecificType> result = new List<SpecificType>();
            cq["h3"].Next().First().Children().Each((li) =>
            {
                result.Add(new SpecificType { Extends = typeName, TypeName = cq["a", li].Attr("href") });
            });

            return result.ToArray();
        }

        [UnitTest("DataType.GetDataType should get expected Types")]
        public void DataTypeGetDataType()
        {
            string[] types = new string[] { "Boolean", "Date", "Number", "Text", "Time", "Url" };
            foreach (string typeName in types)
            {
                object dataType = DataType.GetDataType(typeName);
                Type sysType = DataType.GetTypeOfDataType(typeName);
                Expect.AreSame(sysType, dataType.GetType());
            }            
        }

        private int AddTest(int one, int two)
        {
            return one + two;
        }

        [UnitTest("Schema Integer should be implicitly int")]
        public void SchemaInteger()
        {
            Integer three = new Integer(3);
            Integer four = new Integer(4);
            int result = AddTest(three, four);
            Expect.IsTrue(result == 7);
        }

        [UnitTest("DataType.GetDataType should get expected generic types")]
        public void DataTypeGetGenericDataType()
        {
            Boolean b = DataType.GetDataType<Boolean>("Boolean");
            Expect.IsNotNull(b);

            Date d = DataType.GetDataType<Date>("Date");
            Expect.IsNotNull(d);

            Number n = DataType.GetDataType<Number>("Number");
            Expect.IsNotNull(n);

            Text t = DataType.GetDataType<Text>("Text");
            Expect.IsNotNull(t);

            URL u = DataType.GetDataType<URL>("Url");
            Expect.IsNotNull(u);
        }

    }

}
