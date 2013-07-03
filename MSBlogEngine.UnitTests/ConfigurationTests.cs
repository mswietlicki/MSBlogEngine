using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSBlogEngine.Configuration;
using SimpleInjector;
using Xunit;

namespace MSBlogEngine.UnitTests
{
    public class ConfigurationTests
    {

        [Fact]
        public void GetPostsDirectory()
        {
            //SETUP
            var container = new Container();
            container.Register<IBlogConfiguration>(() => new BlogConfiguration());

            var configuration = container.GetInstance<IBlogConfiguration>();

            //ACT
            var dir = configuration.BlogPostsDirectory;
            //VERIFY

        }
    }
}
