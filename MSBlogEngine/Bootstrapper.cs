using System.Linq;
using System.Text;
using System.Web.Http;
using MSBlogEngine.Configuration;
using MSBlogEngine.Providers;
using MSBlogEngine.Providers.FileStorage;
using MSBlogEngine.Render;
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

            container.Register<IBlogConfiguration>(() => new ConfigFileReader().GetBlogConfiguration());
            container.RegisterSingle<IFileProvider>(container.GetInstance<LocalFileProvider>);
            container.Register<IBlogProvider>(container.GetInstance<MarkdownBlogProvider>);
            container.Register<IPostRenderEngine>(container.GetInstance<HtmlPostRenderEngine>);
            container.RegisterSingle(() => this);

            return container;
        }
    }
}
