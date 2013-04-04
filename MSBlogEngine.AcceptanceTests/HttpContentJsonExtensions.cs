using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MSBlogEngine.Models;
using Newtonsoft.Json;

namespace Newtonsoft.Json
{
    public static class HttpContentJsonExtensions
    {
        public static T GetAndDeserializeJsonResult<T>(this HttpContent content)
        {
            return content.ReadAsStringAsync().ContinueWith(t => JsonConvert.DeserializeObject<T>(t.Result)).Result;
        }
    }
}
