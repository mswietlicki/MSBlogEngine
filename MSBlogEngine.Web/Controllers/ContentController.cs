using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSBlogEngine.Configuration;

namespace MSBlogEngine.Web.Controllers
{
    public class ContentController : Controller
    {
        private IBlogConfiguration _configuration;

        public ContentController(IBlogConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult Download(string id)
        {
            var directory = _configuration.BlogPostsDirectory + "\\Files";
            var contentType = "image/png";

            var file = Path.Combine(directory, id);

            if (System.IO.File.Exists(file))
                return File(directory + "\\" + id, contentType, id);

            return new HttpNotFoundResult();
        }
    }
}
