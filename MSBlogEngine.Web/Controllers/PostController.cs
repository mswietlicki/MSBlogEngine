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

        public ActionResult Index(int page = 0)
        {
            var posts = _blogController.Get().Skip(page * 10).Take(10);

            return View(posts);
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
