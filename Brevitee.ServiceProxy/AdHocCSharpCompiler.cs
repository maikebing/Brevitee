using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace Brevitee.ServiceProxy
{
    public class AdHocCSharpCompiler
    {
        public static CompilerResults CompileDirectories(DirectoryInfo[] directories, string assemblyFileName, string[] referenceAssemblies, bool executable)
        {

            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters parameters = GetCompilerParameters(assemblyFileName, referenceAssemblies, executable);

            List<string> fileNames = new List<string>();

            foreach (DirectoryInfo directory in directories)
            {
                foreach (FileInfo fileInfo in directory.GetFiles("*.cs", SearchOption.AllDirectories))
                {
                    if (!fileNames.Contains(fileInfo.FullName))
                    {
                        fileNames.Add(fileInfo.FullName);
                    }
                }
            }
            return codeProvider.CompileAssemblyFromFile(parameters, fileNames.ToArray());
        }

        public static CompilerParameters GetCompilerParameters(string assemblyFileName, string[] referenceAssemblies, bool executable)
        {
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = executable;
            parameters.OutputAssembly = assemblyFileName;

            SetCompilerOptions(referenceAssemblies, parameters);
            return parameters;
        }

        public static void SetCompilerOptions(string[] referenceAssemblies, CompilerParameters parameters)
        {
            StringBuilder compilerOptions = new StringBuilder();

            foreach (string referenceAssembly in referenceAssemblies)
            {
                compilerOptions.AppendFormat("/reference:{0} ", referenceAssembly);
            }
            parameters.CompilerOptions = compilerOptions.ToString();
        }
    }
}
