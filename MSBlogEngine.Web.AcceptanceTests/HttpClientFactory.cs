using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using IISExpressAutomation;
using SimpleInjector;

namespace MSBlogEngine.Web.AcceptanceTests
{
    public class HttpClientFactory
    {
        public HttpClient Create()
        {
            var baseAddress = new Uri("http://localhost:8022");

            var path = Path.GetFullPath("..\\..\\..\\MSBlogEngine.Web");
            var port = baseAddress.Port;

            return new HttpClient(new IISExpressHttpHandler(path, port)) { BaseAddress = baseAddress };
        }

    }
    public class IISExpressHttpHandler : HttpClientHandler
    {
        private readonly IISExpress _iis;

        public IISExpressHttpHandler(string path, int port)
        {
            _iis = new IISExpress(new Parameters { Path = path, Port = port });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _iis.Dispose();
        }
    }
}
