using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSBlogEngine.Common
{
    public static class StringBuilderExtentions
    {
        public static Stream ToStream(this StringBuilder stringBuilder)
        {
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(stringBuilder.ToString());
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}
