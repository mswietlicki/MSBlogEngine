using System;
using MSBlogEngine.Models;
using MSBlogEngine.Render;
using SimpleInjector;
using Xunit;

namespace MSBlogEngine.UnitTests
{
    public class MarkdownRenderTests
    {
        [Fact]
        public void MarkdownRenderTest()
        {
            var blogPost = new BlogPost
                {
                    Title = "Test",
                    CreateDate = new DateTime(2013, 4, 4, 15, 30, 30),
                    Body = "Test body\r\n\r\nTest2"
                };

            var expected = "<p>Test body</p>\n\n<p>Test2</p>\n";

            var container = new Container();
            container.Register<IPostRenderEngine>(container.GetInstance<HtmlPostRenderEngine>);

            var renderer = container.GetInstance<IPostRenderEngine>();

            var html = renderer.Render(blogPost);

            Assert.Equal(expected, html);
        }
    }
}