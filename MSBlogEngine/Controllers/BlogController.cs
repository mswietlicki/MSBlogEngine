using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MSBlogEngine.Models;

namespace MSBlogEngine.Controllers
{
    public class BlogController : ApiController
    {
        private readonly IBlogStorage _getInstance = Global.Container.GetInstance<IBlogStorage>();

        public List<BlogPost> Get()
        {
            return _getInstance.GetPosts().ToList();
        }

        public BlogPost Get(int id)
        {
            return _getInstance.GetPost(id);
        }

        public int Put(BlogPost post)
        {
            return _getInstance.AddPost(post); ;
        }
    }
}
