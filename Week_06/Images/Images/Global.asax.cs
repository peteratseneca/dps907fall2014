using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
// more...
using AutoMapper;

namespace Images
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Load the data store initializer (for simple projects)
            System.Data.Entity.Database.SetInitializer(new Models.StoreInitializer());

            // HTTP OPTIONS method handler
            GlobalConfiguration.Configuration.MessageHandlers.Add(new Handlers.HttpOptionsMethodHandler());

            // Image media type formatter
            GlobalConfiguration.Configuration.Formatters.Add(new Images.Formatters.ImageFormatter());

            // AutoMapper maps
            // These define mappings between 'design model' and 'resource model' classes
            // Remember, we never work with 'design model' classes in our controllers 
            // and end-user 'use cases'

            Mapper.CreateMap<Models.Book, Controllers.BookBase>();
            Mapper.CreateMap<Controllers.BookAdd, Models.Book>();
            Mapper.CreateMap<Controllers.BookEdit, Models.Book>();
            Mapper.CreateMap<Controllers.BookBase, Controllers.BookWithLink>();

            // Models to Controllers base
            // Controllers add to Models
            // Controllers base to Controllers with link

        }
    }
}
