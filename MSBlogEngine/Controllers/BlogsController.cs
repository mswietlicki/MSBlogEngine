using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MSBlogEngine.Models;
using MSBlogEngine.Providers;

namespace MSBlogEngine.Controllers
{
    public class BlogsController : ApiController
    {
        private readonly IBlogProvider _blogProvider;

        public BlogsController(IBlogProvider blogProvider)
        {
            _blogProvider = blogProvider;
        }

        public IEnumerable<BlogPost> Get()
        {
            return GetPosts().ToList();
        }

        public IEnumerable<BlogPost> Get(string tag)
        {
            return GetPosts()
                .Where(p =>
                       string.IsNullOrWhiteSpace(tag) ||
                       p.Tags.Any(t => t.Equals(tag, StringComparison.InvariantCultureIgnoreCase)))
                .ToList();
        }

        private IEnumerable<BlogPost> GetPosts()
        {
            return _blogProvider.GetPosts().Where(post => !post.Hidden).OrderByDescending(post => post.CreateDate);
        }
    }
}