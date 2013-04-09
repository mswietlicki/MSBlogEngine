using System.Collections.Generic;
using MSBlogEngine.Models;

namespace MSBlogEngine.Storage
{
    public interface IBlogStorage
    {
        BlogPost GetPost(int id);
        int AddPost(BlogPost post);
        IEnumerable<BlogPost> GetPosts();
    }
}
