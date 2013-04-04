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
using Xunit;
using MSBlogEngine;

namespace MSBlogEngine.AcceptanceTests
{
    public class ServiceBasicTests
    {
        [Fact]
        public void IsServiceAccesible()
        {
            var baseAddress = new Uri("http://localhost:8023");

            var configuration = new HttpSelfHostConfiguration(baseAddress);
            new Bootstrapper().Configure(configuration);
            var server = new HttpSelfHostServer(configuration);

            using (var web = new HttpClient(server) { BaseAddress = baseAddress })
            {
                var response = web.GetAsync("").Result;

                Assert.True(response.IsSuccessStatusCode, "Status code : " + response.StatusCode);
            }
        }
        [Fact]
        public void PutBlogPostToService()
        {
            var baseAddress = new Uri("http://localhost:8023");

            var configuration = new HttpSelfHostConfiguration(baseAddress);
            new Bootstrapper().Configure(configuration);
            var server = new HttpSelfHostServer(configuration);

            using (var web = new HttpClient(server) { BaseAddress = baseAddress })
            {
                var response = web.PutAsync("",
                    new BlogPost
                        {
                            Title = "Test",
                            CreateDate = new DateTime(2013, 4, 4, 15, 30, 30),
                            Body = "Test body"
                        }, new JsonMediaTypeFormatter()).Result;

                Assert.True(response.IsSuccessStatusCode, "Status code : " + response.StatusCode);
            }
        }
    }

}
