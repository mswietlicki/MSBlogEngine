using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MSBlogEngine.Models;
using MSBlogEngine.Storage;

namespace MSBlogEngine.Controllers
{
    public class BlogController : ApiController
    {
        private readonly IBlogStorage _blogStorage;

        public BlogController(IBlogStorage blogStorage)
        {
            _blogStorage = blogStorage;
        }

        public List<BlogPost> Get()
        {
            return _blogStorage.GetPosts().ToList();
        }

        public BlogPost Get(Guid id)
        {
            return _blogStorage.GetPost(id);
        }

        public Guid Put(BlogPost post)
        {
            return _blogStorage.AddPost(post); ;
        }

        public void Post(Guid id, BlogPost post)
        {
            _blogStorage.UpdatePost(id, post);
        }
    }
}
