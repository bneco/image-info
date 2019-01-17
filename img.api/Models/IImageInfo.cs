using img.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace img.api.Models
{
    /// <summary>
    /// Interface to retrieve image information for url
    /// </summary>
    public interface IImageInfo
    {
        IEnumerable<ImageInfo> GetImageInfo(string url);
    }
}