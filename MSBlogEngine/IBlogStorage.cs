using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSBlogEngine.Models;

namespace MSBlogEngine
{
    public interface IBlogStorage
    {
        BlogPost GetPost(int id);
        int AddPost(BlogPost post);
        IEnumerable<BlogPost> GetPosts();
    }
}
