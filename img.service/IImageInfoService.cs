using img.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace img.service
{
    public interface IImageInfoService
    {
        IEnumerable<ImageInfo> GetImageInfo(string url);
    }
}
