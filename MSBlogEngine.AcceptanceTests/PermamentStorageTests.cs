using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MSBlogEngine.Models;
using Newtonsoft.Json;
using Xunit;
using MSBlogEngine;

namespace MSBlogEngine.AcceptanceTests
{
    public class PermamentStorageTests
    {

        [Fact]
        public void PutAndSaveBlogPost()
        {
            var blogPost = GetExampleBlogPost();

            int id;

            using (var web = new HttpClientFactory().Create())
            {
                var response = web.PutAsJsonAsync("", blogPost).Result;
                Assert.True(response.IsSuccessStatusCode, "Status code : " + response.StatusCode);

                id = response.Content.GetAndDeserializeJsonResult<int>();
            }

            Assert.Equal(Global.Container.GetInstance<IBlogStorage>().GetPost(id), blogPost);

        }

        private static BlogPost GetExampleBlogPost()
        {
            return new BlogPost
                {
                    Title = "Test",
                    CreateDate = new DateTime(2013, 4, 4, 15, 30, 30),
                    Body = "Test body"
                };
        }
    }
}
