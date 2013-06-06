using System;
using System.IO;
using System.Text;
using MSBlogEngine.Common;
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
            var mock = new Mock<IFileProvider>();

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Title:\tTest");
            stringBuilder.AppendLine("CreateDate:\t" + new DateTime(2013, 04, 18, 17, 50, 10));
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("TestBody");


            mock.Setup(o => o.GetFileStream(It.IsAny<string>())).Returns(stringBuilder.ToStream());
            container.Register<IFileProvider>(() => mock.Object);

            var storage = container.GetInstance<MarkdownBlogProvider>();

            var post = storage.GetPost("");

            mock.Verify(o => o.GetFileStream(It.IsAny<string>()), Times.Once());

            Assert.True(post.Title == "Test");
            Assert.True(post.CreateDate == new DateTime(2013, 04, 18, 17, 50, 10));
            Assert.True(post.Body.Trim() == "TestBody");
        }


    }
}