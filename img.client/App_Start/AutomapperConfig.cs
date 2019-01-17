using AutoMapper;
using img.client.Models;
using img.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace img.client
{
    public class AutomapperConfig
    {
        // Fields
        private static MapperConfiguration _mapperConfiguration;

        // Properties
        public static MapperConfiguration MapperConfiguration { get { return _mapperConfiguration; } private set { _mapperConfiguration = value; } }

        public static void Config()
        {
            if (_mapperConfiguration == null )
                _mapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<ImageInfo, ImageInfoModel>().ReverseMap();
            });
        }
    }
}