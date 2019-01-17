using AutoMapper;
using img.client.Models;
using img.data;
using img.service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace img.client.Controllers
{
    public class HomeController : Controller
    {
        // Fields
        private readonly IImageInfoService _imageInfoService;

        // Properties
        private IImageInfoService ImageInfoService { get { return _imageInfoService;  } }

        // Constructor
        public HomeController(IImageInfoService imageInfoService)
        {
            this._imageInfoService = imageInfoService;
        }

        /// <summary>
        /// Displays a page that allows a user to enter an image url and displays information about that image if present
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(TempData["model"] ?? new ImageModel());
        }

        /// <summary>
        /// Attempts to retrieve information about the image at the specified url
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Url")]ImageModel model)
        {
            string genericErrorMsg = "We were unable to retrieve image information for the url you entered.";

            if (ModelState.IsValid)
            {
                if (ImageInfoService != null)
                {
                    var mapper = AutomapperConfig.MapperConfiguration.CreateMapper();

                    if (mapper != null)
                    {
                        string url = model.Url;
                        string apiCallUrl = GetApiCallUrl(url);

                        if (apiCallUrl != null)
                        {
                            // Call to retrieve image information as a list of property/value pairs
                            var imageInfo = ImageInfoService.GetImageInfo(apiCallUrl);

                            if (imageInfo != null)
                            {
                                foreach (var item in imageInfo)
                                {
                                    var infoModel = mapper.Map<ImageInfoModel>(item);

                                    if (infoModel != null)
                                        model.infoModels.Add(infoModel);
                                }

                                // Tell the view there is data to display
                                model.IsInformationAvailable = true;
                                model.Url = url;

                                TempData["model"] = model;

                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
            }

            ModelState.AddModelError(String.Empty, genericErrorMsg);

            return View(model);
        }

        /// <summary>
        /// Builds api call url
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        private string GetApiCallUrl(string imgUrl)
        {
            if (imgUrl == null)
                throw new ArgumentNullException("image url must be specified");

            imgUrl.TrimStart(new char[] { '?' });
            imgUrl = Url.Encode(imgUrl);

            string result = null;

            try
            {
                string apiEndpoint = ConfigurationManager.AppSettings["ApiEndpoint"];

                if (apiEndpoint != null)
                {
                    apiEndpoint = apiEndpoint.TrimEnd(new char[] { '?' });
                    result = $"{apiEndpoint}?url={imgUrl}";
                }
            }
            catch { }

            return result;
        }
    }
}