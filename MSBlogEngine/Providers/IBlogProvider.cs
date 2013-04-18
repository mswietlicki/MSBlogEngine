using System;
using System.Collections.Generic;
using MSBlogEngine.Models;

namespace MSBlogEngine.Providers
{
    public interface IBlogProvider
    {
        BlogPost GetPost(Guid id);
        Guid AddPost(BlogPost post);
        IEnumerable<BlogPost> GetPosts();
        void UpdatePost(Guid id, BlogPost post);
    }
}
