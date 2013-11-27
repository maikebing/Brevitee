using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Bam.core
{
    public interface IRequestRenderer
    {
        ExecutionRequest Request { get; set; }
        void Render(object value, Stream output);
    }
}
