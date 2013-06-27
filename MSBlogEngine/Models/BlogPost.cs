using System;
using System.IO;
using System.Linq;

namespace MSBlogEngine.Models
{
    public class BlogPost
    {
        private string _title;

        public string Id { get; set; }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                Id = GetIdFromTitle(Title);
            }
        }

        public DateTime CreateDate { get; set; }
        public string Body { get; set; }

        public string Summary { get; set; }

        public BlogPost()
        {
            Id = Guid.NewGuid().ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals((BlogPost)obj);
        }

        protected bool Equals(BlogPost other)
        {
            return string.Equals(Title, other.Title) && CreateDate.Equals(other.CreateDate) && string.Equals(Body, other.Body);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Title != null ? Title.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ CreateDate.GetHashCode();
                hashCode = (hashCode * 397) ^ (Body != null ? Body.GetHashCode() : 0);
                return hashCode;
            }
        }


        private string GetIdFromTitle(string title)
        {
            return CleanFileName(title.ToLower().Replace(" ", "_"));
        }

        private static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }

    }
}
