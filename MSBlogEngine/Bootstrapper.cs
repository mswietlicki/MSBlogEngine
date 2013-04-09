using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.Dependencies;
using MSBlogEngine.Storage;
using MSBlogEngine.Storage.FileStorage;
using SimpleInjector;

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
            container.Register<IBlogStorage>(() => new XMLBlogStorage());

            return container;
        }
    }

    public class SimpleInjectorDependencyResolver : IDependencyResolver
    {
        public Container Container { get; private set; }

        public SimpleInjectorDependencyResolver(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.Container = container;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object GetService(Type serviceType)
        {
            return ((IServiceProvider)this.Container).GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.Container.GetAllInstances(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            throw new NotImplementedException();
        }
    }
}
