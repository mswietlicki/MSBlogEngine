using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace MSBlogEngine
{
    public static class Global
    {
        private static Container _container;
        public static Container Container
        {
            get { return _container ?? (_container = new Bootstrapper().BuildContainer()); }
        }
    }
}
