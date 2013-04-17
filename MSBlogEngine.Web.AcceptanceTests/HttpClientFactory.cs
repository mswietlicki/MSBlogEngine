using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using SimpleInjector;

namespace MSBlogEngine.Web.AcceptanceTests
{
    public class HttpClientFactory
    {
        public HttpClient Create()
        {
            var baseAddress = new Uri("http://localhost:8023");

            var configuration = new HttpSelfHostConfiguration(baseAddress);
            Global.Container.GetInstance<Bootstrapper>().Configure(configuration, Global.Container);
            var server = new HttpSelfHostServer(configuration);

            return new HttpClient(server) {BaseAddress = baseAddress};
        }
    }
}
