using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using MSBlogEngine.Models;
using MSBlogEngine.Providers.FileStorage;

namespace MSBlogEngine.Providers
{
    public class XMLBlogProvider : IBlogProvider
    {
        private readonly IFileProvider _fileProvider;

        public XMLBlogProvider(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public BlogPost GetPost(Guid id)
        {
            return GetPost(string.Format("Posts\\{0}.xml", id));
        }

        private BlogPost GetPost(string path)
        {
            using (var stream = _fileProvider.GetFileStream(path))
            {
                return new XmlSerializer(typeof(BlogPost)).Deserialize(stream) as BlogPost;
            }
        }

        public Guid AddPost(BlogPost post)
        {
            using (var stream = _fileProvider.GetFileStream(string.Format("Posts\\{0}.xml", post.Id)))
            {
                new XmlSerializer(typeof(BlogPost)).Serialize(stream, post);
                return post.Id;
            }
        }

        public IEnumerable<BlogPost> GetPosts()
        {
            return _fileProvider.GetFilesPaths(f => f.StartsWith("Posts")).Select(GetPost);
        }

        public void UpdatePost(Guid id, BlogPost post)
        {
            using (var stream = _fileProvider.GetFileStream(string.Format("Posts\\{0}.xml", id)))
            {
                new XmlSerializer(typeof(BlogPost)).Serialize(stream, post);
            }
        }
    }
}
