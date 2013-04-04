using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace MSBlogEngine
{
    public class Bootstrapper
    {
        public void Configure(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "{controller}/{id}",
                defaults: new
                    {
                        controller = "Blog",
                        id = RouteParameter.Optional
                    });
        }
    }
}
