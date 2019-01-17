using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace img.client.Models
{
    public class ImageInfoModel
    {
        [Display(Name ="Property")]
        public string Property { get; set; }

        [Display(Name = "Value")]
        public string Value { get; set; }
    }
}