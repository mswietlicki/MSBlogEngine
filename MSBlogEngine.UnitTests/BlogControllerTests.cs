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
            var id = Guid.Empty;
            var blogPost = new BlogPost();

            var container = new Container();
            var blogStorage = new Mock<IBlogStorage>();
            blogStorage.Setup(o => o.GetPost(id)).Returns(blogPost);
            container.Register<IBlogStorage>(() => blogStorage.Object);

            var controller = container.GetInstance<BlogController>();

            //ACT
            var post = controller.Get(id);

            //VERIFY
            blogStorage.Verify(o => o.GetPost(id));
            Assert.Same(post, blogPost);
        }
        
        [Fact]
        public void PutPost()
        {
            //SETUP
            var id = Guid.Empty;
            var blogPost = new BlogPost();

            var container = new Container();
            var blogStorage = new Mock<IBlogStorage>();
            blogStorage.Setup(o => o.AddPost(blogPost)).Returns(id);
            container.Register<IBlogStorage>(() => blogStorage.Object);

            var controller = container.GetInstance<BlogController>();

            //ACT
            var postId = controller.Put(blogPost);

            //VERIFY
            blogStorage.Verify(o => o.AddPost(blogPost), Times.Once());
            Assert.Equal(postId, id);
        }

        [Fact]
        public void PostPost()
        {
            //SETUP
            var blogPost = new BlogPost();
            var id = Guid.Empty;

            var container = new Container();
            var blogStorage = new Mock<IBlogStorage>();
            blogStorage.Setup(o => o.UpdatePost(id, blogPost));
            container.Register<IBlogStorage>(() => blogStorage.Object);

            var controller = container.GetInstance<BlogController>();

            //ACT
            controller.Post(id, blogPost);

            //VERIFY
            blogStorage.Verify(o => o.UpdatePost(id, blogPost), Times.Once());
            
        }


    }
}
