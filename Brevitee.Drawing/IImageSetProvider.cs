using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KLGates.Images
{
    public interface IImageSetProvider
    {
        IImageSet GetImageSet(ImageSetParameters parameters);
    }
}
