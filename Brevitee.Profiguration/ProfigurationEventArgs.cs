using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Brevitee.Profiguration
{
    public class ProfigurationEventArgs: EventArgs
    {
        public ProfigurationEventArgs(DirectoryInfo rootDir)
        {
            this.RootDirectory = rootDir;
        }

        public DirectoryInfo RootDirectory { get; set; }
    }
}
