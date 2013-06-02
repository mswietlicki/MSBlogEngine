using System;
using System.Collections.Generic;
using MSBlogEngine.Models;

namespace MSBlogEngine.Providers
{
    public interface IBlogProvider
    {
        BlogPost GetPost(string id);
        string AddPost(BlogPost post);
        IEnumerable<BlogPost> GetPosts();
        void UpdatePost(string id, BlogPost post);
    }
}
