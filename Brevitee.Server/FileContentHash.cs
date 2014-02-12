using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using System.IO;

namespace Brevitee.Server
{
    /// <summary>
    /// Data Access Object DataBase JavaScript Hash
    /// </summary>
    public class FileContentHash
    {
        public FileContentHash() { }
        public FileContentHash(string filePath)
        {
            this.FilePath = filePath;
            this.Sha1 = new FileInfo(filePath).Sha1();
        }

        public string FilePath { get; set; }
        public string Sha1 { get; set; }

        public override int GetHashCode()
        {
            return Sha1.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            FileContentHash hash = obj as FileContentHash;
            if (hash != null)
            {
                return hash.FilePath.Equals(this.FilePath) && hash.Sha1.Equals(this.Sha1);
            }
            else
            {
                return base.Equals(obj);
            }
        }
    }
}
