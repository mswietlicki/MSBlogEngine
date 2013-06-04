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


        [Fact]
        public void IsGetAccesible()
        {
            using (var web = new HttpClientFactory().Create())
            {
                var response = web.GetAsync("Post/Get/zielona_mila").Result;

                Assert.True(response.IsSuccessStatusCode, "Status code : " + response.StatusCode);
            }
        }
    }
#endif
}
