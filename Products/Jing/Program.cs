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
using Brevitee.Data;

namespace jing
{
    [Serializable]
    class Program : CommandLineInterface
    {
        static void Main(string[] args)
        {
            AddValidArgument("root", false, "The root directory to search for dao assemblies", "/root:<path_to_dao_root_typically_bin");
            AddValidArgument("out", false, "The output directory", "/out:<path_to_output_directory");
			AddValidArgument("viewType", false, "dao || method", "/viewType:dao");
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
				Dictionary<string, Action<Type, string>> actions = new Dictionary<string, Action<Type, string>>();
				actions.Add("dao", WritePartialView);
				actions.Add("method", WriteMethodForms);

				string outputPath = Arguments["out"];
				string dllRoot = Arguments["root"];
	            string viewType = Arguments.Contains("viewType") ? Arguments["viewType"] : "dao";

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
						if (viewType.Equals("dao")) 
						{
							ForEachTypeAddornedWith<TableAttribute>(file, outputDir, actions[viewType]);
						}
						else 
						{
							ForEachTypeAddornedWith<ProxyAttribute>(file, outputDir, actions[viewType]);
						}
						
					}
					catch (Exception ex)
					{
						OutFormat("{0}::{1}", ConsoleColor.Red, file.Name, ex.Message);
					}
				}
            }
        }

	    private static void ForEachTypeAddornedWith<T>(FileInfo file, DirectoryInfo outputDir, Action<Type, string> doAction)  where T: Attribute
		{
		    Assembly assembly = Assembly.LoadFrom(file.FullName);
		    Type[] addornedTypes = (from type in assembly.GetTypes()
			    where type.HasCustomAttributeOfType<T>()
			    select type).ToArray();

		    if (addornedTypes.Length == 0) 
			{
			    OutFormat("No dao types were found in ({0})", ConsoleColor.Yellow, assembly.FullName);
		    } 
			else 
			{
			    for (int i = 0; i < addornedTypes.Length; i++) 
				{
				    doAction(addornedTypes[i], outputDir.FullName); //WritePartialView(addornedTypes[i], outputDir.FullName);
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
                string backup = Path.Combine(outputDir, string.Format("{0}_{1}.dust", Path.GetFileNameWithoutExtension(fileName), DateTime.Now.ToJulianDate().ToString()));
                File.Move(fileName, backup);
            }

            OutFormat("Writing {0}", ConsoleColor.Green, fileName);
            htm.SafeWriteToFile(fileName, overwrite);
        }

		private static void WriteMethodForms(Type serviceProxyType, string outputDir = ".")
		{
			MethodInfo[] proxiedMethods = serviceProxyType.GetMethods().Where(m => !m.HasCustomAttributeOfType<ExcludeAttribute>()).ToArray();
			foreach (MethodInfo method in proxiedMethods)
			{
				string htm = MethodForm(serviceProxyType, method.Name);
				FileInfo outputFile = new FileInfo(Path.Combine(outputDir, string.Format("{0}.inc", method.Name)));
				string fileName = outputFile.FullName;
				bool overwrite = false;
				if(outputFile.Exists)
				{
					string backup = Path.Combine(outputDir, string.Format("{0}_{1}.inc", Path.GetFileNameWithoutExtension(fileName), DateTime.Now.ToJulianDate().ToString()));
					File.Move(fileName, backup);
				}

				OutFormat("Writing {0}", ConsoleColor.Cyan, fileName);
				htm.SafeWriteToFile(fileName, overwrite);
			}
		}

        public static string InputFor(Type type, object instance)
        {
            InputFormBuilder builder = new InputFormBuilder();
            return builder.FieldsetFor(type, instance, null).ToString();
        }

		public static string MethodForm(Type type, string methodName)
		{
			InputFormBuilder builder = new InputFormBuilder();
			int ignore = -1;
			return builder.MethodForm(type, "fieldset", methodName, null, out ignore).ToString();
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
