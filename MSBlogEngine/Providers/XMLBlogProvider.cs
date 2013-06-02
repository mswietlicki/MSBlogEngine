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

        public BlogPost GetPost(string id)
        {
            return GetPostByPath(string.Format("Posts\\{0}.xml", id));
        }

        private BlogPost GetPostByPath(string path)
        {
            using (var stream = _fileProvider.GetFileStream(path))
            {
                return new XmlSerializer(typeof(BlogPost)).Deserialize(stream) as BlogPost;
            }
        }

        public string AddPost(BlogPost post)
        {
            using (var stream = _fileProvider.GetFileStream(string.Format("Posts\\{0}.xml", post.Id)))
            {
                new XmlSerializer(typeof(BlogPost)).Serialize(stream, post);
                return post.Id;
            }
        }

        public IEnumerable<BlogPost> GetPosts()
        {
            return _fileProvider.GetFilesPaths(f => f.StartsWith("Posts")).Select(GetPostByPath);
        }

        public void UpdatePost(string id, BlogPost post)
        {
            using (var stream = _fileProvider.GetFileStream(string.Format("Posts\\{0}.xml", id)))
            {
                new XmlSerializer(typeof(BlogPost)).Serialize(stream, post);
            }
        }
    }
}
