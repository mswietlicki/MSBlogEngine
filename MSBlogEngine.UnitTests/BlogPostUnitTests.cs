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
        public static IEnumerable<object[]> ExampleSamePostData
        {
            get
            {
                return new[]
                    {
                        new object[] { new BlogPost {Title = "Test", Body = "tet", CreateDate = new DateTime(2011, 1, 1)}, new BlogPost {Title = "Test", Body = "tet", CreateDate = new DateTime(2011, 1, 1) }},
                    };
            }
        }

        [Theory]
        [PropertyData("ExampleSamePostData")]
        public void TwoSamePostsEqual(BlogPost a, BlogPost b)
        {
            Assert.Equal(a, b);
        }

        [Theory]
        [PropertyData("ExampleSamePostData")]
        public void TwoSamePostsEqualHash(BlogPost a, BlogPost b)
        {
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


        public static IEnumerable<object[]> ExamplePostsData
        {
            get
            {
                return new[]
                    {
                        new object[] { new BlogPost {Title = "Testowy", Body = "tet", CreateDate = new DateTime(2011, 1, 1)}, "testowy" },
                        new object[] { new BlogPost {Title = "Zielona mila", Body = "tet", CreateDate = new DateTime(2011, 1, 1) } , "zielona_mila"},
                        new object[] { new BlogPost {Title = "NDC 2012 – Moje oceny", Body = "tet", CreateDate = new DateTime(2011, 1, 1)}, "ndc_2012_–_moje_oceny"},
                    };
            }
        }

        [Theory]
        [PropertyData("ExamplePostsData")]
        public void PostIdGreatedFromTitle(BlogPost a, string id)
        {
            Assert.Equal(a.Id, id);
        }

    }
}
