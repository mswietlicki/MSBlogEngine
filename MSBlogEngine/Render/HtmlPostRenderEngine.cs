using MSBlogEngine.Models;
using MarkdownSharp;

namespace MSBlogEngine.Render
{
    public class HtmlPostRenderEngine: IPostRenderEngine
    {
        public string Render(BlogPost post)
        {
            var engine = new Markdown();
            return engine.Transform(post.Body);
        }
    }
}