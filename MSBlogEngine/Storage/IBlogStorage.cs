using System;
using System.Collections.Generic;
using MSBlogEngine.Models;

namespace MSBlogEngine.Storage
{
    public interface IBlogStorage
    {
        BlogPost GetPost(Guid id);
        Guid AddPost(BlogPost post);
        IEnumerable<BlogPost> GetPosts();
        void UpdatePost(Guid id, BlogPost post);
    }
}
