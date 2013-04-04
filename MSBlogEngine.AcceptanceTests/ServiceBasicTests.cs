using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using MSBlogEngine.Models;
using Newtonsoft.Json;
using Xunit;
using MSBlogEngine;

namespace MSBlogEngine.AcceptanceTests
{
    public class ServiceBasicTests
    {
        [Fact]
        public void IsServiceAccesible()
        {
            using (var web = new HttpClientFactory().Create())
            {
                var response = web.GetAsync("").Result;

                Assert.True(response.IsSuccessStatusCode, "Status code : " + response.StatusCode);
            }
        }

        [Fact]
        public void GetBlogPostsCollection()
        {
            using (var web = new HttpClientFactory().Create())
            {
                var response = web.GetAsync("Blog").Result;

                Assert.True(response.IsSuccessStatusCode, "Status code : " + response.StatusCode);

                var result = response.Content.ReadAsStringAsync()
                            .ContinueWith(t => JsonConvert.DeserializeObject<IEnumerable<BlogPost>>(t.Result)).Result;

                Assert.NotNull(result);
            }
        }

        [Fact]
        public void PutBlogPostToService()
        {
            var blogPost = new BlogPost
            {
                Title = "Test",
                CreateDate = new DateTime(2013, 4, 4, 15, 30, 30),
                Body = "Test body"
            };

            using (var web = new HttpClientFactory().Create())
            {
                var response = web.PutAsJsonAsync("", blogPost).Result;

                Assert.True(response.IsSuccessStatusCode, "Status code : " + response.StatusCode);

                response = web.GetAsync("Blog/1").Result;

                Assert.True(response.IsSuccessStatusCode, "Status code : " + response.StatusCode);

                var result = response.Content.ReadAsStringAsync().Result;
                var expected = JsonConvert.SerializeObject(blogPost);

                Assert.True(result.Equals(expected), "Result equal: " + result);
            }
        }


    }

}
