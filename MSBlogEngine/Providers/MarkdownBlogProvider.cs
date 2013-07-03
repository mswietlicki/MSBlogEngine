using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Management;
using MSBlogEngine.Configuration;
using MSBlogEngine.Models;
using MSBlogEngine.Providers.FileStorage;

namespace MSBlogEngine.Providers
{
    public class MarkdownBlogProvider : IBlogProvider
    {
        private readonly IFileProvider _fileProvider;
        private readonly MarkdownSerializer<BlogPost> _markdownSerializer;
        private readonly IBlogConfiguration _configuration;

        public MarkdownBlogProvider(IFileProvider fileProvider, MarkdownSerializer<BlogPost> markdownSerializer, IBlogConfiguration configuration)
        {
            _fileProvider = fileProvider;
            _markdownSerializer = markdownSerializer;
            _configuration = configuration;
        }

        public BlogPost GetPost(string id)
        {
            return GetPostByPath(string.Format("{1}\\{0}.md", id, _configuration.BlogPostsDirectory));
        }

        private BlogPost GetPostByPath(string path)
        {
            try
            {

                using (var stream = _fileProvider.GetFileStream(path, false))
                {
                    var post = _markdownSerializer.Deserialize(stream);
                    post.Id = new FileInfo(path).Name.Replace(".md", "");
                    return post;
                }
            }
            catch (Exception ex)
            {
                //LOG
                return null;
            }
        }
        public string AddPost(BlogPost post)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogPost> GetPosts()
        {
            return _fileProvider.GetFilesPaths(f => f.Contains(".md")).Select(GetPostByPath);
        }

        public void UpdatePost(string id, BlogPost post)
        {
            throw new NotImplementedException();
        }
    }
}