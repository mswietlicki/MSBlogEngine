using Xunit;

namespace MSBlogEngine.Web.AcceptanceTests
{
    public class WebpageTests
    {
#if !DEBUG
        [Fact]
#endif
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
