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

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, BlogPosts);
        }

        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, BlogPosts.FirstOrDefault());
        }

        public HttpResponseMessage Put(BlogPost post)
        {
            BlogPosts.Add(post);
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
    }
}
