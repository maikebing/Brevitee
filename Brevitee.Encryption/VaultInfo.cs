using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Brevitee.Encryption
{
    public class VaultInfo
    {
        public VaultInfo() { }

        public VaultInfo(string name)
        {
            this.Name = name;
            this.FilePath = new FileInfo(".\\{0}.vault.sqlite"._Format(name)).FullName;
        }

        public string FilePath { get; set; }
        public string Name { get; set; }

        public Vault Load()
        {
            return Vault.Load(new FileInfo(FilePath), Name);
        }
    }
}
