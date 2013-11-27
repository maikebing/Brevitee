using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Brevitee.Incubation;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Brevitee.ServiceProxy
{
    public class CsvResult: ActionResult
    {
        List<PropertyInfo> propertyInfos;
        public CsvResult()
        {
            this.propertyInfos = new List<PropertyInfo>();
        }

        public CsvResult(object data, string fileName)
            : this()
        {
            this.Data = data;
            this.FileName = fileName;
            this.InitPropertyInfos();
        }

        public string FileName { get; set; }
        public object Data { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            object[] toBeWritten = new object[] { Data };
            if (Data is Array)
            {
                toBeWritten = (object[])Data;
            }

            context.HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName + ".csv");
            context.HttpContext.Response.AddHeader("Content-Type", "application/vnd.ms-excel");

            TextWriter writer = new StreamWriter(context.HttpContext.Response.OutputStream);
            WriteHeaders(writer);

            foreach (object toWrite in toBeWritten)
            {
                WriteCsvLine(toWrite, writer);
            }

            writer.Flush();
        }

        private void WriteHeaders(TextWriter writer)
        {
            bool first = true;
            foreach (PropertyInfo prop in propertyInfos)
            {
                if (!first)
                {
                    writer.Write(",");
                }

                writer.Write(prop.Name.PascalSplit(" "));
                
                first = false;
            }

            writer.WriteLine();
        }

        private void WriteCsvLine(object data, TextWriter writer)
        {
            bool first = true;
            foreach (PropertyInfo prop in propertyInfos)
            {
                string value = prop.GetValue(data, null).ToString();
                string format = value.Contains(',') ? "\"{0}\"" : "{0}";
                bool replaceQuotes = value.Contains('"');
                if (replaceQuotes)
                {
                    value = value.Replace("\"", "\"\"");
                }

                if (!first)
                {
                    writer.Write(",");
                }

                writer.Write(string.Format(format, value));
                
                first = false;
            }
            writer.WriteLine();
        }
        
        private void InitPropertyInfos()
        {
            Type type = Data.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Type propertyType = property.PropertyType;
                if (propertyType.IsEnum || propertyType == typeof(string) || propertyType == typeof(int) || propertyType == typeof(long))
                {
                    propertyInfos.Add(property);
                }
            }
        }

        
    }
}
