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

        public ActionResult Posts(string tag = "blog", int page = 0)
        {
            var postModels = _blogController
                .Get()
                .Where(p => 
                    string.IsNullOrWhiteSpace(tag) || 
                    p.Tags.Any(t=>t.Equals(tag, StringComparison.InvariantCultureIgnoreCase)))
                .Select(post => new PostModel(post, _renderEngine.Render(post)));

            return View(postModels);
        }

        public ActionResult Post(string id)
        {
            var post = _blogController.Get(id);
            var html = _renderEngine.Render(post);

            var postModel = new PostModel(post, html);

            return View(postModel);
        }
    }
}
