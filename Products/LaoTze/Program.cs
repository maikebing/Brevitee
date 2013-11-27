using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Schema;
using Brevitee.Data.MsSql;
using Brevitee.Incubation;
using Brevitee.CommandLine;

using System.IO;
using System.CodeDom.Compiler;

namespace LaoTze
{
    class Program: CommandLineInterface
    {
        static TargetTableEventDelegate BeforeTableHandler = (ns, t) =>
        {
            OutFormat("Writing {0}.{1}", ConsoleColor.Yellow, ns, t.ClassName);
        };

        static TargetTableEventDelegate AfterTableHandler = (ns, t) =>
        {
            OutFormat("Done Writing {0}.{1}", ConsoleColor.Green, ns, t.ClassName);
        };

        static void Main(string[] args)
        {
            AddValidArgument("f", false); // the output schema filename
            AddValidArgument("conn", false); // the name of the connection in the config file to use
            AddValidArgument("gen", false); // the directory to write generated files to 
            AddValidArgument("ns", false); // namespace
            AddValidArgument("dll", false); // if specified the code will be compiled to the dll specified
            AddValidArgument("?", true); // help
            AddValidArgument("p", false); // partial folder for custom code
            AddValidArgument("pause", true);
            AddValidArgument("v", true);
            AddValidArgument("s", true);
            AddValidArgument("root", false);
            AddValidArgument("keep", true);

            ParseArgs(args);

            if (Arguments.Contains("?"))
            {
                Out("For extraction:\r\n");
                Out("LaoTze /f:<file> /conn:<connectionNameFromConfig> /gen:<dirPath> /ns:<defaultNamespace> /dll:<assemblyName> [/v|/s]");
                Out("\r\n or To generate from *.db.js\r\n");
                Out("LaoTze /root:<project_root_to_search_for_database.db.js>\r\n");
                return;
            }

            if (Arguments.Contains("pause"))
            {
                Pause("Press a key to continue...");
            }

            if (Arguments.Contains("root"))
            {
                DirectoryInfo rootDirectory = new DirectoryInfo(Arguments["root"]);
                if (!rootDirectory.Exists)
                {
                    OutFormat("Specified root directory does not exist: {0}", ConsoleColor.Red, rootDirectory.FullName);
                    Pause();
                    Environment.Exit(1);
                }

                FileInfo[] dbjs = rootDirectory.GetFiles("*.db.js", SearchOption.AllDirectories);
                if (dbjs.Length > 0)
                {
                    if (dbjs.Length > 1)
                    {
                        Out("Multiple database.js files found", ConsoleColor.Red);
                        OutFormat("{0}", ConsoleColor.Yellow, dbjs.ToDelimited<FileInfo>(f => f.FullName, "\r\n"));
                        string answer = Prompt("Process each? [y N]", ConsoleColor.Yellow);
                        if (!answer.ToLowerInvariant().Equals("y"))
                        {
                            Exit(1);
                        }
                    }

                    foreach (FileInfo file in dbjs)
                    {
                        try
                        {
                            OutFormat("Processing {0}...", ConsoleColor.Yellow, file.FullName);
                            SchemaManager manager = new SchemaManager();
                            manager.RootDir = rootDirectory.FullName;
                            DirectoryInfo fileParent = file.Directory;
                            string genTo = Arguments.Contains("gen") ? Arguments["gen"] : Path.Combine(file.Directory.FullName, "Generated");
                            if (Directory.Exists(genTo))
                            {
                                Directory.Move(genTo, Path.Combine(genTo, "{0}_{1}"._Format(genTo, DateTime.Now.ToJulianDate().ToString())));
                            }
                            DirectoryInfo genToDir = new DirectoryInfo(genTo);
                            bool keep = Arguments.Contains("keep");
                            bool compile = !keep;
                            Result result = manager.Generate(file, compile, keep, genToDir.FullName);
                            if (!result.Success)
                            {
                                throw new Exception(result.Message);
                            }
                            Out(result.Message, ConsoleColor.Green);
                        }
                        catch (Exception ex)
                        {
                            OutFormat("{0}\r\n\r\n***\r\n{1}", ConsoleColor.Red, ex.Message, ex.StackTrace ?? "");
                            Pause("Press enter to exit");
                            Exit(1);
                        }
                    }

                    Pause("Press enter to exit...");
                }
                else
                {
                    Out("No *.db.js files were found", ConsoleColor.Yellow);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Arguments["conn"]))
                {
                    Out("Please specify a connection name from the config or a directory to search", ConsoleColor.Yellow);
                    Pause();
                }
                else
                {
                    Extract();
                }
            }
        }

        private static void Extract()
        {
            Action<string> inspector = (s) => { };
            if (Arguments.Contains("v"))
            {
                inspector = (s) => { Out(s, ConsoleColor.Cyan); };
            }

            string connectionName = "Default";
            string filePath = "Schema.json";
            bool gen = !string.IsNullOrEmpty(Arguments["gen"]);
            bool compile = !string.IsNullOrEmpty(Arguments["dll"]);
            bool silent = Arguments.Contains("s");

            if (!string.IsNullOrEmpty(Arguments["f"]))
            {
                filePath = Arguments["f"];
            }

            if (!string.IsNullOrEmpty(Arguments["conn"]))
            {
                connectionName = Arguments["conn"];
            }

            OutFormat("Extracting schema using the connection ({0})", connectionName);
            SchemaDefinition schema = ExtractSchema(connectionName, filePath);
            Out("Extraction complete...");

            if (gen)
            {
                RazorParser<RazorBaseTemplate>.DefaultRazorInspector = inspector;
                OutFormat("Generating csharp for ({0})", schema.File);
                Generate(schema, inspector, silent);
                Out("Generation complete...");
                if (compile)
                {
                    DirectoryInfo dir = new DirectoryInfo(Arguments["gen"]);
                    List<DirectoryInfo> dirs = new List<DirectoryInfo>();
                    dirs.Add(dir);
                    if (!string.IsNullOrEmpty(Arguments["p"]))
                    {
                        dirs.Add(new DirectoryInfo(Arguments["p"]));
                    }

                    FileInfo file = new FileInfo(Arguments["dll"]);

                    OutFormat("Compiling sources in ({0})", dir.FullName);
                    Compile(dirs.ToArray(), file);
                    OutFormat("Compilation complete...");
                }
            }
        }

        private static void Compile(DirectoryInfo[] dirs, FileInfo file)
        {
            DaoGenerator generator = new DaoGenerator(GetNamespace());
            CompilerResults results = generator.Compile(dirs, file.FullName);
            OutputCompilerErrors(results);
        }

        private static void OutputCompilerErrors(CompilerResults results)
        {
            foreach (CompilerError error in results.Errors)
            {
                OutFormat("File=>{0}", ConsoleColor.Yellow, error.FileName);
                OutFormat("Line {0}, Column {1}::{2}", error.Line, error.Column, error.ErrorText);
                Out();
            }
        }

        private static void Generate(SchemaDefinition schema, Action<string> resultInspector = null, bool silent = false)
        {
            DirectoryInfo dir = new DirectoryInfo(Arguments["gen"]);
            if (!dir.Exists)
            {
                dir.Create();
            }

            string ns = GetNamespace();
            if (resultInspector == null)
            {
                resultInspector = (s) => { };
            }
            DaoGenerator generator = new DaoGenerator(ns, resultInspector);
            if (!silent)
            {
                generator.BeforeClassParse += BeforeTableHandler;
                generator.AfterClassParse += AfterTableHandler;
            }
            generator.Generate(schema, dir.FullName);
        }

        private static string GetNamespace()
        {
            string ns = Arguments["ns"];
            if (string.IsNullOrEmpty(ns))
            {
                ns = "Dao";
            }
            return ns;
        }

        private static SchemaDefinition ExtractSchema(string connectionName, string filePath)
        {
            ISchemaExtractor extractor = Incubator.Default.Get<ISchemaExtractor>(new SqlClientSchemaExtractor(connectionName));
            SchemaDefinition schema = extractor.Extract();
            schema.Save(filePath);
            return schema;
        }
    }
}
