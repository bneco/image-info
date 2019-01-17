using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace img.client.Models
{
    public class ImageModel
    {
        public ImageModel()
        {
            infoModels = new List<ImageInfoModel>();
        }

        [Required]
        [Url(ErrorMessage="Please enter a valid image url")]
        [Display(Name="Image url")]
        public string Url { get; set; }

        public bool IsInformationAvailable { get; set; } = false;

        public List<ImageInfoModel> infoModels { get; set; }
    }
}