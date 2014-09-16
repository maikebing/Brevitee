using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Brevitee;
using Brevitee.CommandLine;

namespace Brevitee.Html
{
    public class Pdf
    {
        public static PdfOutput FromHtml(string filePath, PdfOrientation orientation = PdfOrientation.Landscape)
        {
            return FromHtml(new FileInfo(filePath), "-s Letter ", orientation);
        }

        public static PdfOutput FromHtml(FileInfo inputFile, string wkhtmlOptions, PdfOrientation orientation = PdfOrientation.Landscape)
        {
            string fileName = Path.GetFileNameWithoutExtension(inputFile.Name);
            FileInfo output = new FileInfo("{0}.pdf"._Format(fileName));

            ProcessOutput processOutput = "wkhtml.exe -O {0} {1} {2}"._Format(orientation.ToString(), wkhtmlOptions, output.Name).Run();

            return new PdfOutput(output);
        }
    }
}
