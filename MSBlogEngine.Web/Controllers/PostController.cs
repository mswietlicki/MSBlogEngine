﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
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
            var postModels = GetPostModels(tag);

            return View(postModels);
        }

        private IEnumerable<PostModel> GetPostModels(string tag = "blog")
        {
            var postModels = _blogController
                .Get()
                .Where(p =>
                    string.IsNullOrWhiteSpace(tag) ||
                    p.Tags.Any(t => t.Equals(tag, StringComparison.InvariantCultureIgnoreCase)))
                .Select(post => new PostModel(post, _renderEngine.Render(post)));

            ViewBag.Tag = tag;
            return postModels;
        }

        public ActionResult Post(string id)
        {
            var post = _blogController.Get(id);
            var html = _renderEngine.Render(post);

            var postModel = new PostModel(post, html);

            return View(postModel);
        }

        public ActionResult Rss(string tag)
        {
            var posts = GetPostModels(tag);

            var postItems = posts.Select(p =>
                    new SyndicationItem(p.Post.Title, p.Html, new Uri(Url.Action("Post", "Post", new { id = p.Post.Id }), UriKind.Relative))
                );

            var feed = new SyndicationFeed("Mateusz Świetlicki: Blog programisty", "Blog techniczno informatyczny prowadzony przez programistę .NET Mateusza Świetlickiego", new Uri(Url.Action("Posts"), UriKind.Relative), postItems)
            {
                Copyright = new TextSyndicationContent("Mateusz Świetlicki 2013"),
                Language = "pl-PL"
            };

            return new FeedResult(new Rss20FeedFormatter(feed));
        }
    }

    public class FeedResult : ActionResult
    {
        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }

        private readonly SyndicationFeedFormatter feed;
        public SyndicationFeedFormatter Feed
        {
            get { return feed; }
        }

        public FeedResult(SyndicationFeedFormatter feed)
        {
            this.feed = feed;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/rss+xml";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (feed != null)
                using (var xmlWriter = new XmlTextWriter(response.Output))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    feed.WriteTo(xmlWriter);
                }
        }
    }
}

