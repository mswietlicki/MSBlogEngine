using System.Linq;
using System.Text;
using System.Web.Http;
using MSBlogEngine.Storage;
using MSBlogEngine.Storage.FileStorage;
using SimpleInjector;
using SimpleInjector.Integration.Web.WebApi;

namespace MSBlogEngine
{
    public class Bootstrapper : IContainerBuilder
    {
        public void Configure(HttpConfiguration config, Container container)
        {
            config.Routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "{controller}/{id}",
                defaults: new
                    {
                        controller = "Blog",
                        id = RouteParameter.Optional
                    });
            config.DependencyResolver = new SimpleInjectorDependencyResolver(container);
        }

        public Container BuildContainer()
        {
            var container = new Container();

            container.RegisterSingle(() => this);
            container.RegisterSingle<IFileStorage>(() => new MemoryFileStorage());
            container.Register<IBlogStorage>(container.GetInstance<XMLBlogStorage>);

            return container;
        }
    }
}
