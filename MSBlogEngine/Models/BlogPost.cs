﻿using System;

namespace MSBlogEngine.Models
{
    public class BlogPost
    {
        private string _title;

        public string Id { get; set; }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public DateTime CreateDate { get; set; }
        public string Body { get; set; }

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

    }
}
