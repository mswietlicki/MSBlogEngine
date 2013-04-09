using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using MSBlogEngine.Models;
using MSBlogEngine.Storage.FileStorage;

namespace MSBlogEngine.Storage
{
    public class XMLBlogStorage: IBlogStorage
    {
        private readonly IFileStorage _fileStorage;

        public XMLBlogStorage()
        {
            _fileStorage = Global.Container.GetInstance<IFileStorage>();
        }

        public BlogPost GetPost(int id)
        {
            return GetPost(string.Format("Posts\\{0}.xml", id));
        }

        private BlogPost GetPost(string path)
        {
            var stream = _fileStorage.GetFileStream(path);
            {
                return new XmlSerializer(typeof (BlogPost)).Deserialize(stream) as BlogPost;
            }
        }

        public int AddPost(BlogPost post)
        {
            var id = 0;
            var stream = _fileStorage.GetFileStream(string.Format("Posts\\{0}.xml", id));
            {
                new XmlSerializer(typeof (BlogPost)).Serialize(stream, post);
                return id;
            }
        }

        public IEnumerable<BlogPost> GetPosts()
        {
            return _fileStorage.GetFilesPaths(f => f.StartsWith("Posts")).Select(f => GetPost(f));
        }
    }
}
