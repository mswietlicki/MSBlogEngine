﻿using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MSBlogEngine.Models;
using MSBlogEngine.Providers;

namespace MSBlogEngine.Controllers
{
    public class BlogController : ApiController
    {
        private readonly IBlogProvider _blogProvider;

        public BlogController(IBlogProvider blogProvider)
        {
            _blogProvider = blogProvider;
        }

        public BlogPost Get(string id)
        {
            return _blogProvider.GetPost(id);
        }

        public string Put(BlogPost post)
        {
            return _blogProvider.AddPost(post); ;
        }

        public void Post(string id, BlogPost post)
        {
            _blogProvider.UpdatePost(id, post);
        }
    }
}
