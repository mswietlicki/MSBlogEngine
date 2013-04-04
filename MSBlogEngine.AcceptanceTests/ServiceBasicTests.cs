using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Xunit;

namespace MSBlogEngine.AcceptanceTests
{
    public class ServiceBasicTests
    {
        [Fact]
        public void IsServiceAccesible()
        {
            var baseAddress = new Uri("http://localhost:8023");

            var configuration = new HttpSelfHostConfiguration(baseAddress);
            new Bootstraper().Configure((HttpConfiguration) configuration);
            var server = new HttpSelfHostServer(configuration);
            
            using (var web = new HttpClient(server) { BaseAddress = baseAddress })
            {
                var response = web.GetAsync("").Result;

                Assert.True(response.IsSuccessStatusCode, "Status code : " + response.StatusCode);
            }
        }
    }
}
