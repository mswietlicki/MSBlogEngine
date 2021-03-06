﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using NLog;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace MSBlogEngine.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class Global : System.Web.HttpApplication
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public static Container Container
        {
            get { return MSBlogEngine.Global.Container; }
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

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            if (Request.Url.AbsoluteUri.Contains("/files/")) return;

            var sb = new StringBuilder();
            sb.AppendLine("Request: " + Request.Url.AbsoluteUri);
            sb.AppendLine("UserAdress: " + Request.UserHostAddress);
            sb.AppendLine("UserAgent: " + Request.UserAgent);

            logger.Info(sb.ToString);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            var sb = new StringBuilder();
            sb.AppendLine("Error: " + exception.Message);
            sb.AppendLine("StackTrace: " + exception.StackTrace);
            
            logger.ErrorException(sb.ToString(), exception);
        }
    }
}