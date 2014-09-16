using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;
using System.IO;

namespace laotzu
{
    public class GenConfig
    {
        public GenConfig()
        {
            _referenceAssemblies = new List<string>();
        }


        public string Name
        {
            get;
            set;
        }

        public string Namespace { get; set; }
        public string TargetDirectory { get; set; }
        public string CompileFileName { get; set; }
        public string PartialDirectory { get; set; }
        public string SchemaFileName { get; set; }

        public bool Extract { get; set; }

		public bool Verbose { get; set; }

        List<string> _referenceAssemblies;
        public string[] ReferenceAssemblies
        {
            get
            {
                return _referenceAssemblies.ToArray();
            }
            set
            {
                _referenceAssemblies.Clear();
                _referenceAssemblies.AddRange(value);
            }
        }

        public bool Compile
        {
            get
            {
                return !string.IsNullOrEmpty(this.CompileFileName);
            }
        }

        public bool IncludePartials
        {
            get
            {
                return !string.IsNullOrEmpty(this.PartialDirectory);
            }
        }

        public void AddAssemblyReference(string reference)
        {
            _referenceAssemblies.Add(reference);
        }

        public bool IsValid(out List<string> msgs)
        {
            bool result = true;
            msgs = new List<string>();

            if (Extract && string.IsNullOrEmpty(Name))
            {
                msgs.Add("Connection name must be specified");
                result = false;
            }

            if (string.IsNullOrEmpty(this.Namespace))
            {
                msgs.Add("Namespace must be specified");
                result = false;
            }

            if (string.IsNullOrEmpty(this.SchemaFileName))
            {
                msgs.Add("SchemaFileName must be specified");
                result = false;
            }

            if (string.IsNullOrEmpty(this.TargetDirectory))
            {
                msgs.Add("Target Directory must be specified");
                result = false;
            }
            else
            {
                DirectoryInfo dir = new DirectoryInfo(this.TargetDirectory);
                if (!dir.Exists)
                {
                    dir.Create();
                }
            }

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
