using img.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace img.service
{
    public class ImageInfoServiceApi : IImageInfoService
    {
        public IEnumerable<ImageInfo> GetImageInfo(string url)
        {
            List<ImageInfo> imageInfo = null;
            WebClient webClient = new WebClient();

            try
            {
                string result = webClient.DownloadString(url);

                if (result != null)
                    imageInfo = new JavaScriptSerializer().Deserialize<List<ImageInfo>>(result);
            }
            catch{ }

            return imageInfo;
        }
    }
}
