using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MSBlogEngine.Configuration
{
    public interface IBlogConfiguration : IConfiguration
    {
        string BlogPostsDirectory { get; set; }
    }

    public class BlogConfiguration : IBlogConfiguration
    {
        public BlogConfiguration(ConfigFileReader config)
        {
            
        }
        public string BlogPostsDirectory
        {
            get { return GetValue(() => BlogPostsDirectory).ToString(); }
            set
            {
                SetValue(() => BlogPostsDirectory, value);
            }
        }

        private object GetValue<TProperty>(Expression<Func<TProperty>> projection)
        {
            var propertyName = ((MemberExpression)projection.Body).Member.Name;
            return GetValue(propertyName);
        }

        private void SetValue<TProperty>(Expression<Func<TProperty>> projection, object value)
        {
            var propertyName = ((MemberExpression)projection.Body).Member.Name;
            SetValue(propertyName, value);
        }

        public object GetValue(string name)
        {
            return "";
        }

        public void SetValue(string name, object value)
        {
            
        }
    }

    public class ConfigFileReader
    {
        public ConfigFileReader()
        {
            
        }

        public InfoliniaConfiguration GetConfiguration(string path)
        {
            FileStream fs = null;
            fs = new FileStream(FileName, FileMode.Open);

            try
            {
                XmlSerializer s = new XmlSerializer(typeof(InfoliniaConfiguration));
                return s.Deserialize(fs) as InfoliniaConfiguration;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }
    }
}
