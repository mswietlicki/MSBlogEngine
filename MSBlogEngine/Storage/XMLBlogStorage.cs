using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using MSBlogEngine.Models;
using MSBlogEngine.Storage.FileStorage;

namespace MSBlogEngine.Storage
{
    public class XMLBlogStorage : IBlogStorage
    {
        private readonly IFileStorage _fileStorage;

        public XMLBlogStorage(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public BlogPost GetPost(Guid id)
        {
            return GetPost(string.Format("Posts\\{0}.xml", id));
        }

        private BlogPost GetPost(string path)
        {
            using (var stream = _fileStorage.GetFileStream(path))
            {
                return new XmlSerializer(typeof(BlogPost)).Deserialize(stream) as BlogPost;
            }
        }

        public Guid AddPost(BlogPost post)
        {
            using (var stream = _fileStorage.GetFileStream(string.Format("Posts\\{0}.xml", post.Id)))
            {
                new XmlSerializer(typeof(BlogPost)).Serialize(stream, post);
                return post.Id;
            }
        }

        public IEnumerable<BlogPost> GetPosts()
        {
            return _fileStorage.GetFilesPaths(f => f.StartsWith("Posts")).Select(GetPost);
        }

        public void UpdatePost(Guid id, BlogPost post)
        {
            using (var stream = _fileStorage.GetFileStream(string.Format("Posts\\{0}.xml", id)))
            {
                new XmlSerializer(typeof(BlogPost)).Serialize(stream, post);
            }
        }
    }
}
