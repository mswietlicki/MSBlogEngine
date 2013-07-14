using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Routing;

namespace MSBlogEngine.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "OldRouting",
                url: "post/{y}/{m}/{d}/{name}",
                defaults: new { controller = "Old", action = "Translate", name = "" }
            );

            routes.MapRoute(
                name: "ContentRouting",
                url: "files/{id}",
                defaults: new { controller = "Content", action = "Download", id = "" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{action}/{id}",
                defaults: new { controller = "Post", action = "Posts", id = UrlParameter.Optional }
            );


        }
    }
}