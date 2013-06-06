using System;
using System.Collections.Generic;
using MSBlogEngine.Models;
using MSBlogEngine.Providers.FileStorage;

namespace MSBlogEngine.Providers
{
    public class MarkdownBlogProvider : IBlogProvider
    {
        private readonly IFileProvider _fileProvider;
        private readonly MarkdownSerializer<BlogPost> _markdownSerializer;

        public MarkdownBlogProvider(IFileProvider fileProvider, MarkdownSerializer<BlogPost> markdownSerializer)
        {
            _fileProvider = fileProvider;
            _markdownSerializer = markdownSerializer;
        }

        public BlogPost GetPost(string id)
        {
            return GetPostByPath(string.Format("Posts\\{0}.md", id));
        }

        private BlogPost GetPostByPath(string path)
        {
            using (var stream = _fileProvider.GetFileStream(path))
            {
                return _markdownSerializer.Deserialize(stream);
            }
        }
        public string AddPost(BlogPost post)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogPost> GetPosts()
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(string id, BlogPost post)
        {
            throw new NotImplementedException();
        }
    }
}