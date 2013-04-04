using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;

namespace MSBlogEngine.AcceptanceTests
{
    public class HttpClientFactory
    {
        public HttpClient Create()
        {
            var baseAddress = new Uri("http://localhost:8023");

            var configuration = new HttpSelfHostConfiguration(baseAddress);
            new Bootstrapper().Configure(configuration);
            var server = new HttpSelfHostServer(configuration);

            return new HttpClient(server) {BaseAddress = baseAddress};
        }
    }
}
