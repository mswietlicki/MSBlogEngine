using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSBlogEngine.Controllers;
using MSBlogEngine.Storage;
using MSBlogEngine.Storage.FileStorage;
using SimpleInjector;
using Xunit;
using Moq;

namespace MSBlogEngine.UnitTests
{
    public class BlogControllerTests
    {
        [Fact]
        public void GetPosts()
        {
            var container = new Container();
            Global.Container = container;
            var blogStorage = new Mock<IBlogStorage>();
            container.Register<IBlogStorage>(() => blogStorage.Object);

            var controller = Global.Container.GetInstance<BlogController>();
            var posts = controller.Get();

            Assert.NotNull(posts);
            blogStorage.Verify(o => o.GetPosts());
        }
    }
}
