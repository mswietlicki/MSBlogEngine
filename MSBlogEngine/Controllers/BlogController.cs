using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MSBlogEngine.Controllers
{
    public class BlogController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

        public HttpResponseMessage Put()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
    }
}
