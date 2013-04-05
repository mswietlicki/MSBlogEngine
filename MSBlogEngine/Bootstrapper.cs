using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using SimpleInjector;

namespace MSBlogEngine
{
    public class Bootstrapper : IContainerBuilder
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

        public Container BuildContainer()
        {
            var container = new Container();

            container.RegisterSingle(() => this);
            container.RegisterSingle<IFileStorage>(() => new MemoryFileStorage());
            container.Register<IBlogStorage>(() => new XMLBlogStorage());

            return container;
        }
    }
}
