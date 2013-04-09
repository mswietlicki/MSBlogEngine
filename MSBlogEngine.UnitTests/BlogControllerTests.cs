using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSBlogEngine.Controllers;
using MSBlogEngine.Models;
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
            var blogStorage = new Mock<IBlogStorage>();
            container.Register<IBlogStorage>(() => blogStorage.Object);

            var controller = container.GetInstance<BlogController>();
            var posts = controller.Get();

            Assert.NotNull(posts);
            blogStorage.Verify(o => o.GetPosts(), Times.Once());
        }

        [Fact]
        public void GetPost()
        {
            //SETUP
            var blogPost = new BlogPost();

            var container = new Container();
            var blogStorage = new Mock<IBlogStorage>();
            blogStorage.Setup(o => o.GetPost(0)).Returns(blogPost);
            container.Register<IBlogStorage>(() => blogStorage.Object);

            var controller = container.GetInstance<BlogController>();

            //ACT
            var post = controller.Get(0);

            //VERIFY
            blogStorage.Verify(o => o.GetPost(0));
            Assert.Same(post, blogPost);
        }
    }
}
