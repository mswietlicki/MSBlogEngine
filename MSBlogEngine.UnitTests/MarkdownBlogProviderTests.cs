using System;
using System.IO;
using System.Text;
using MSBlogEngine.Common;
using MSBlogEngine.Configuration;
using MSBlogEngine.Providers;
using MSBlogEngine.Providers.FileStorage;
using Moq;
using SimpleInjector;
using Xunit;

namespace MSBlogEngine.UnitTests
{
    public class MarkdownBlogProviderTests
    {
        [Fact]
        public void GetPostFromMarkdownFormat()
        {
            var container = new Container();
            container.Register<IBlogConfiguration>(() => new Mock<IBlogConfiguration>().Object);
            var mock = new Mock<IFileProvider>();

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Title:\tTest");
            stringBuilder.AppendLine("CreateDate:\t" + new DateTime(2013, 04, 18, 17, 50, 10));
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("TestBody");


            mock.Setup(o => o.GetFileStream(It.IsAny<string>(), false)).Returns(stringBuilder.ToStream());
            container.Register<IFileProvider>(() => mock.Object);

            var storage = container.GetInstance<MarkdownBlogProvider>();

            var post = storage.GetPost("");

            mock.Verify(o => o.GetFileStream(It.IsAny<string>(), false), Times.Once());

            Assert.True(post.Title == "Test");
            Assert.True(post.CreateDate == new DateTime(2013, 04, 18, 17, 50, 10));
            Assert.True(post.Body.Trim() == "TestBody");
        }

        [Fact]
        public void GetPosts()
        {
            //SETUP
            var container = new Container();
            var filePrivider = new Mock<IFileProvider>();

            container.Register<IBlogConfiguration>(() => new Mock<IBlogConfiguration>().Object);
            container.Register<IFileProvider>(() => filePrivider.Object);

            var storage = container.GetInstance<MarkdownBlogProvider>();
            //ACT
            var posts = storage.GetPosts();
            //VERIFY
            Assert.NotNull(posts);

        }
    }
}