using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSBlogEngine.Models;
using Xunit;
using Xunit.Extensions;

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
        public void TwoSamePostsEqualHash()
        {
            var a = new BlogPost { Title = "Test", Body = "tet", CreateDate = new DateTime(2011, 1, 1) };
            var b = new BlogPost { Title = "Test", Body = "tet", CreateDate = new DateTime(2011, 1, 1) };

            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }


        public static IEnumerable<object[]> ExampleNotSamePostData
        {
            get
            {
                return new[]
                    {
                        new object[] { new BlogPost {Title = "Test", Body = "tet", CreateDate = new DateTime(2011, 1, 1)}, new BlogPost {Title = "Tes", Body = "tet", CreateDate = new DateTime(2011, 1, 1) }},
                        new object[] { new BlogPost {Title = "Test", Body = "tet", CreateDate = new DateTime(2011, 1, 1)}, new BlogPost {Title = "Test", Body = "tet2", CreateDate = new DateTime(2011, 1, 1) }},
                        new object[] { new BlogPost {Title = "Test", Body = "tet", CreateDate = new DateTime(2011, 1, 1)}, new BlogPost {Title = "Test", Body = "tet", CreateDate = new DateTime(2011, 1, 1, 4,23,11) }}
                    };
            }
        }

        [Theory]
        [PropertyData("ExampleNotSamePostData")]
        public void TwoNotSamePostsNotEqual(BlogPost a, BlogPost b)
        {
            Assert.NotEqual(a, b);
        }

        [Theory]
        [PropertyData("ExampleNotSamePostData")]
        public void TwoNotSamePostsNotSameHash(BlogPost a, BlogPost b)
        {
            Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        }

    }
}
