using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.BuildEngine;
using Microsoft.Build;
using Microsoft.Build.Framework;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Logging;
using System.IO;

namespace Brevitee.Automation.Build
{
    public static class Extensions
    {
        public static BuildResult Compile(this FileInfo solutionOrProject, string outputPath, ILogger logger = null)
        {
            Dictionary<string, string> properties = GetDefaultProperties();
            properties["OutputPath"] = outputPath;
            return Compile(solutionOrProject, properties, logger);
        }
        public static BuildResult Compile(this string solutionOrProjectPath, string outputPath, ILogger logger = null)
        {
            Dictionary<string, string> properties = GetDefaultProperties();
            properties["OutputPath"] = outputPath;
            properties["GenerateDocumentation"] = "true";
            return Compile(new FileInfo(solutionOrProjectPath), properties, logger);
        }

        public static BuildResult Compile(this FileInfo solutionOrProject, Dictionary<string, string> buildRequestProperties = null, ILogger logger = null)
        {
            if (!solutionOrProject.Exists)
            {
                throw new FileNotFoundException("The specified project or solution file ({0}) was not found"._Format(solutionOrProject.FullName));
            }

            if (buildRequestProperties == null)
            {
                buildRequestProperties = GetDefaultProperties();
            }

            ProjectCollection collection = new ProjectCollection();
            BuildParameters buildParameters = new BuildParameters(collection);
            buildParameters.Loggers = new[] { logger };
            BuildRequestData requestData = new BuildRequestData(solutionOrProject.FullName, buildRequestProperties, null, new string[] { "Build" }, null);

            return BuildManager.DefaultBuildManager.Build(buildParameters, requestData);
        }

        private static Dictionary<string, string> GetDefaultProperties()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("Configuration", "Debug");
            result.Add("Platform", "Any CPU");
            return result;
        }
    }
}
