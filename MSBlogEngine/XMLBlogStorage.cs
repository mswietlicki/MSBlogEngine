using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using MSBlogEngine.Models;

namespace MSBlogEngine
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
            var stream = _fileStorage.GetFileStream(string.Format("Posts\\{0}.xml", id));
            {
                return new XmlSerializer(typeof(BlogPost)).Deserialize(stream) as BlogPost;
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
    }
}
