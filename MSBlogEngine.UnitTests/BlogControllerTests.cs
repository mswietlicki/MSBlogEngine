using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSBlogEngine.Controllers;
using SimpleInjector;
using Xunit;

namespace MSBlogEngine.UnitTests
{
    public class BlogControllerTests
    {
        [Fact]
        public void GetPosts()
        {
            var controller = Global.Container.GetInstance<BlogController>();
            var posts = controller.Get();
            Assert.NotNull(posts);
        }
    }
}
