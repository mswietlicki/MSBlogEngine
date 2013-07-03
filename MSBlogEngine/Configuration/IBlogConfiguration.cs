using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MSBlogEngine.Common;

namespace MSBlogEngine.Configuration
{
    public interface IBlogConfiguration
    {
        string BlogPostsDirectory { get; set; }
    }

    public class BlogConfiguration : IBlogConfiguration
    {
        public string BlogPostsDirectory { get; set; }
    }

    public class ConfigFileReader
    {
        public BlogConfiguration GetBlogConfiguration()
        {
            return GetBlogConfiguration(Assembly.GetExecutingAssembly().GetDirectory() + @"\Configuration\Blog.config");
        }

        public BlogConfiguration GetBlogConfiguration(string path)
        {
            FileStream fs = null;
            fs = new FileStream(path, FileMode.Open);

            try
            {
                var s = new XmlSerializer(typeof(BlogConfiguration));
                return s.Deserialize(fs) as BlogConfiguration;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }
    }
}
