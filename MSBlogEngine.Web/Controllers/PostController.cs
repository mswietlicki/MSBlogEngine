using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MSBlogEngine.Web.Controllers
{
    public class PostController : Controller
    {
        public ActionResult Index()
        {
            return new ContentResult() { Content = "Welcome" };
        }

        public ActionResult Get(string id)
        {
            return new ContentResult() { Content = id };
        }
    }
}
