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
        private static readonly List<BlogPost> BlogPosts;

        static BlogController()
        {
            BlogPosts = new List<BlogPost>();
        }

        public List<BlogPost> Get()
        {
            return BlogPosts;
        }

        public BlogPost Get(int id)
        {
            return BlogPosts.FirstOrDefault();
        }

        public int Put(BlogPost post)
        {
            BlogPosts.Add(post);
            return BlogPosts.Count - 1;
        }
    }
}
