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
        public List<BlogPost> Get()
        {
            return Global.Container.GetInstance<IBlogStorage>().GetPosts().ToList();
        }

        public BlogPost Get(int id)
        {
            return Global.Container.GetInstance<IBlogStorage>().GetPost(id);
        }

        public int Put(BlogPost post)
        {
            return Global.Container.GetInstance<IBlogStorage>().AddPost(post); ;
        }
    }
}
