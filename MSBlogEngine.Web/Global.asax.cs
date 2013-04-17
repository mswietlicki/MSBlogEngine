using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace MSBlogEngine.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class Global : System.Web.HttpApplication
    {
        private static Container _container;
        public static Container Container
        {
            get { return _container ?? (_container = BuildContainer()); }
        }

        private static Container BuildContainer()
        {
            var container = new Container();


            return container;
        }

        protected void Application_Start()
        {
            Container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            Container.RegisterMvcAttributeFilterProvider();
            Container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(Container));

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}