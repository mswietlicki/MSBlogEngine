using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MSBlogEngine.Models;
using MSBlogEngine.Providers;

namespace MSBlogEngine.Controllers
{
    public class BlogController : ApiController
    {
        private readonly IBlogProvider _blogProvider;

        public BlogController(IBlogProvider blogProvider)
        {
            _blogProvider = blogProvider;
        }

        public List<BlogPost> Get()
        {
            return _blogProvider.GetPosts().ToList();
        }

        public BlogPost Get(Guid id)
        {
            return _blogProvider.GetPost(id);
        }

        public Guid Put(BlogPost post)
        {
            return _blogProvider.AddPost(post); ;
        }

        public void Post(Guid id, BlogPost post)
        {
            _blogProvider.UpdatePost(id, post);
        }
    }
}
