using System;
using System.Collections.Generic;
using MSBlogEngine.Models;
using MSBlogEngine.Providers.FileStorage;

namespace MSBlogEngine.Providers
{
    public class MarkdownBlogProvider : IBlogProvider
    {
        private readonly IFileProvider _fileProvider;

        public MarkdownBlogProvider(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public BlogPost GetPost(Guid id)
        {
            return GetPost(string.Format("Posts\\{0}.md", id));
        }

        private BlogPost GetPost(string path)
        {
            using (var stream = _fileProvider.GetFileStream(path))
            {
                return new BlogPost();
            }
        }
        public Guid AddPost(BlogPost post)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogPost> GetPosts()
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(Guid id, BlogPost post)
        {
            throw new NotImplementedException();
        }
    }
}