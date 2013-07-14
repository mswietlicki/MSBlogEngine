using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSBlogEngine.Configuration;

namespace MSBlogEngine.Web.Controllers
{
    public class OldController : Controller
    {
        private readonly IBlogConfiguration _configuration;

        public OldController(IBlogConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult Translate(string name)
        {
            var map = _configuration.OldUrlMap.FirstOrDefault(m => m.Key == name);

            if (map == null) return RedirectToAction("Posts", "Post");

            return RedirectToAction("Post", "Post", new { id = map.Value });
        }

        public ActionResult Rss()
        {
            return RedirectToAction("Rss", "Post");
        }
    }
}
