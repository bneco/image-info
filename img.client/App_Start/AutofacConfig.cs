using Autofac;
using Autofac.Integration.Mvc;
using img.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace img.client
{
    public class AutofacConfig
    {
        public static void Config()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ImageInfoServiceApi>().As<IImageInfoService>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}