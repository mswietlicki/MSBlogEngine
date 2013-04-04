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
        private static BlogPost _blogPost;

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _blogPost);
        }

        public HttpResponseMessage Put(BlogPost post)
        {
            _blogPost = post;
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
    }
}
