using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using SimpleInjector;

namespace SimpleInjector.Integration.Web.WebApi
{
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
            return this;
        }
    }
}