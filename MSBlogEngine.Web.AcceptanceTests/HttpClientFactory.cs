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
            var baseAddress = new Uri("http://localhost:8022");

            return new HttpClient() { BaseAddress = baseAddress };
        }
    }
}
