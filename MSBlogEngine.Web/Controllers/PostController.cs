using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSBlogEngine.Controllers;
using MSBlogEngine.Render;
using MSBlogEngine.Web.Models;

namespace MSBlogEngine.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRenderEngine _renderEngine;
        private readonly BlogController _blogController;

        public PostController(IPostRenderEngine renderEngine, BlogController blogController)
        {
            _renderEngine = renderEngine;
            _blogController = blogController;
        }

        public ActionResult Index()
        {
            return new ContentResult() { Content = "Welcome" };
        }

        public ActionResult Get(string id)
        {
            var post = _blogController.Get(id);
            var html = _renderEngine.Render(post);

            var postModel = new PostModel(post, html);

            return View(postModel);
        }
    }
}
