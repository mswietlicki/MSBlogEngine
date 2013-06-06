
using System.Text;
using System.Web.Mvc;
using MSBlogEngine.Common;
using MSBlogEngine.Providers;
using MSBlogEngine.Providers.FileStorage;
using MSBlogEngine.Render;
using MSBlogEngine.Web.Controllers;
using MSBlogEngine.Web.Models;
using Moq;
using SimpleInjector;
using Xunit;

namespace MSBlogEngine.Web.UnitTests
{
    public class PostTests
    {
        [Fact]
        public void GetPostHtmlByControlerTest()
        {
            //Setup
            var container = new Container();
            container.Register<IPostRenderEngine>(container.GetInstance<HtmlPostRenderEngine>);
            container.Register<IBlogProvider>(container.GetInstance<MarkdownBlogProvider>);
            var mockFileProvider = new Mock<IFileProvider>();

            mockFileProvider.Setup(o => o.GetFileStream(It.IsAny<string>())).Returns(GetTestMarkdownPage().ToStream());
            container.Register<IFileProvider>(() => mockFileProvider.Object);
            
            var controller = container.GetInstance<PostController>();

            //Act
            var result = controller.Get("test") as ViewResult;
            var html = (result.Model as PostModel).Html;

            //Assert
            Assert.Contains("To jest pierwsza strona", html);
        }


        StringBuilder GetTestMarkdownPage()
        {
            return new StringBuilder().AppendLine("Title: Strona testowa").AppendLine().AppendLine("To jest pierwsza strona... Ciekawe :)");
        }
    }
}
