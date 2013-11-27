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
using Brevitee.Html;

namespace Jing
{
    [Serializable]
    class Program : CommandLineInterface
    {
        static void Main(string[] args)
        {
            AddValidArgument("root", false, "The root directory to search for dao assemblies", "/root:<path_to_dao_root_typically_bin");
            AddValidArgument("out", false, "The output directory", "/out:<path_to_output_directory");
            AddValidArgument("?", true);

            ParseArgs(args);

            if (Arguments.Contains("?"))
            {
                Out("Generate views for dao\r\n");
                Out("Jing /root:path_to_dao_assembly /out:.\\dust");
                Out();
            }
            else
            {
                string outputPath = Arguments["out"];
                string dllRoot = Arguments["root"];

                if (string.IsNullOrEmpty(outputPath))
                {
                    outputPath = Path.Combine(dllRoot, "dust");
                }
                if (string.IsNullOrEmpty(dllRoot))
                {
                    Out("dll root must be specified");
                    Environment.Exit(1);
                }

                DirectoryInfo outputDir = new DirectoryInfo(outputPath);

                DirectoryInfo root = new DirectoryInfo(dllRoot);
                FileInfo[] dlls = root.GetFiles("*.dll");
                foreach (FileInfo dll in dlls)
                {   
                    string dllPath = dll.FullName;
                    FileInfo file = new FileInfo(dllPath);
                    try
                    {
                        Assembly daoAssembly = Assembly.LoadFrom(file.FullName);
                        Type[] daoTypes = (from type in daoAssembly.GetTypes()
                                            where type.HasCustomAttributeOfType<Brevitee.Data.TableAttribute>()
                                            select type).ToArray();

                        if (daoTypes.Length == 0)
                        {
                            OutFormat("No dao types were found in ({0})", ConsoleColor.Yellow, daoAssembly.FullName);
                        }
                        else
                        {
                            for (int i = 0; i < daoTypes.Length; i++)
                            {
                                WritePartialView(daoTypes[i], outputDir.FullName);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        OutFormat("{0}::{1}", ConsoleColor.Red, file.Name, ex.Message);
                    }
                }
            }
        }

        private static void WritePartialView(Type daoType, string outputDir = ".")
        {
            Type safeType = daoType.CreateDynamicType<Brevitee.Data.ColumnAttribute>();
            object instance = ConstructAndSetTemplateProperties(safeType);
            string htm = InputFor(safeType, instance).XmlToHumanReadable();
            FileInfo file = new FileInfo(Path.Combine(outputDir, string.Format("{0}.dust", daoType.Name)));
            string fileName = file.FullName;
            bool overwrite = false;
            if (file.Exists)
            {
                string backup = Path.Combine(outputDir, string.Format("{0}_{1}.dust", Path.GetFileNameWithoutExtension(file.FullName), DateTime.Now.ToJulianDate().ToString()));
                File.Move(file.FullName, backup);
            }

            OutFormat("Writing {0}", ConsoleColor.Green, fileName);
            htm.SafeWriteToFile(fileName, overwrite);
        }

        public static string InputFor(Type type, object defaults = null, string name = null)
        {
            InputFormBuilder builder = new InputFormBuilder();
            return builder.FieldsetFor(type, defaults, name).ToString();
        }


        private static object ConstructAndSetTemplateProperties(Type type)
        {
            object o = type.Construct();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                if (prop.PropertyType == typeof(string) ||
                    prop.PropertyType == typeof(int) ||
                    prop.PropertyType == typeof(long))
                {
                    prop.SetValue(o, "{" + prop.Name + "}", null);
                }
            }

            return o;
        }
    }

}
