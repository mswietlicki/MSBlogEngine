﻿using System;
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

                var result = response.Content.GetAndDeserializeJsonResult<IEnumerable<BlogPost>>();

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
                Guid id;
                {
                    var response = web.PutAsJsonAsync("", blogPost).Result;
                    Assert.True(response.IsSuccessStatusCode, "Status code : " + response.StatusCode);

                    id = response.Content.GetAndDeserializeJsonResult<Guid>();
                    Assert.True(id != Guid.Empty, "Result id is: " + id);
                }
                {
                    var response = web.GetAsync("Blog/" + id).Result;

                    Assert.True(response.IsSuccessStatusCode, "Status code : " + response.StatusCode);

                    var result = response.Content.GetAndDeserializeJsonResult<BlogPost>();

                    Assert.Equal(blogPost, result);
                }
                {
                    var response = web.GetAsync("Blog").Result;
                    var result = response.Content.GetAndDeserializeJsonResult<IEnumerable<BlogPost>>();

                    Assert.Contains(blogPost, result);
                }
            }
        }


    }

}
