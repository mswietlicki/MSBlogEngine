using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSBlogEngine.Models;
using Xunit;

namespace MSBlogEngine.UnitTests
{
    public class BlogPostUnitTests
    {
        [Fact]
        public void TwoSamePostsEqual()
        {
            var a = new BlogPost { Title = "Test", Body = "tet", CreateDate = new DateTime(2011, 1, 1) };
            var b = new BlogPost { Title = "Test", Body = "tet", CreateDate = new DateTime(2011, 1, 1) };

            Assert.Equal(a, b);
        }       
        
        [Fact]
        public void TwoNotSamePostsNotEqual()
        {
            {
                var a = new BlogPost { Title = "Test", Body = "tet", CreateDate = new DateTime(2011, 1, 1) };
                var b = new BlogPost { Title = "Tes", Body = "tet", CreateDate = new DateTime(2011, 1, 1) };

                Assert.NotEqual(a, b);
            }
            {
                var a = new BlogPost { Title = "Test", Body = "tet2", CreateDate = new DateTime(2011, 1, 1) };
                var b = new BlogPost { Title = "Test", Body = "tet", CreateDate = new DateTime(2011, 1, 1) };

                Assert.NotEqual(a, b);
            }
            {
                var a = new BlogPost { Title = "Test", Body = "tet", CreateDate = new DateTime(2011, 1, 1, 4, 23, 11) };
                var b = new BlogPost { Title = "Test", Body = "tet", CreateDate = new DateTime(2011, 1, 1) };

                Assert.NotEqual(a, b);
            }
        }        

    }
}
