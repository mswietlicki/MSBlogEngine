using System;
using System.Collections.Generic;
using System.IO;
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
            using (var stream = _fileStorage.GetFileStream(string.Format("Posts\\{0}.xml", id)))
            {
                return new XmlSerializer(typeof(BlogPost)).Deserialize(stream) as BlogPost;
            }
        }
    }

    public interface IFileStorage
    {
        Stream GetFileStream(string format);
    }
}
