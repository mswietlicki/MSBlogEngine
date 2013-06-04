using Xunit;

namespace MSBlogEngine.Web.AcceptanceTests
{
#if !DEBUG
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
#endif
}
