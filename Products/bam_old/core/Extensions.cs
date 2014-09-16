using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Brevitee;
using Newtonsoft.Json;
using Brevitee.Drawing;

namespace Bam.core
{
    public static class Extensions
    {

        public static void Save(this ColorScheme scheme, Fs fs, bool overwrite = false)
        {
            scheme.Save(fs.GetAbsolutePath("~/ColorScheme.yaml"), overwrite);
        }
    }
}
