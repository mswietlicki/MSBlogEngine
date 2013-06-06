using MSBlogEngine.Models;

namespace MSBlogEngine.Web.Models
{
    public class PostModel
    {
        public BlogPost Post { get; set; }
        public string Html { get; set; }

        public PostModel(BlogPost post, string html)
        {
            Post = post;
            Html = html;
        }
    }
}