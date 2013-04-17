using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSBlogEngine.Web.AcceptanceTests;
using Xunit;

namespace MSBlogEngine.AcceptanceTests
{
    public class WebpageTests
    {
        [Fact]
        public void IsWebpageAccesible()
        {
            using (var web = new HttpClientFactory().Create())
            {
                var response = web.GetAsync("").Result;

                Assert.True(response.IsSuccessStatusCode, "Status code : " + response.StatusCode);
            }

        }
    }
}
