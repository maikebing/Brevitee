using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.CommandLine;

namespace Brevitee.Html 
{
    public class PdfOutput : ProcessOutput
    {
        public PdfOutput(FileInfo fileInfo) 
        {
            this.FileInfo = fileInfo;
        }

        public FileInfo FileInfo { get; set; }
    }
}
