using img.api.Models;
using img.data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace img.api.Controllers
{
    public class ImageInfoController : ApiController
    {
        // Fields
        private readonly IImageInfo _imageInfo;

        // Properties
        private IImageInfo ImageInfo { get { return _imageInfo; } }

        public ImageInfoController(IImageInfo imageInfo)
        {
            this._imageInfo = imageInfo;
        }

        // GET: api/ImageInfo/url
        public HttpResponseMessage Get(string url)
        {
            try
            {
                IEnumerable<ImageInfo> imageInfo = ImageInfo.GetImageInfo(url);

                if (imageInfo != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, imageInfo);

                    return response;
                }
            }
            catch{ }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Unable to retrieve image information");
        }
    }
}
