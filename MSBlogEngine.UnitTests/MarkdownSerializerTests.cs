using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSBlogEngine.Models;
using Xunit;
using MSBlogEngine;

namespace MSBlogEngine.UnitTests
{
    public class MarkdownSerializerTests
    {
        [Fact]
        public void DeserialiseTextTest()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Title:\tTest");
            stringBuilder.AppendLine("CreateDate:\t" + new DateTime(2013, 04, 18, 17, 50, 10));
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("TestBody");

            var serializer = new MarkdownSerializer<BlogPost>();
            var post = serializer.Deserialize(StringBuilderToStream(stringBuilder));

            Assert.NotNull(post);
            Assert.Equal(post.Body.Trim(), "TestBody");
            Assert.Equal(post.Title, "Test");
        }

        public static Stream StringBuilderToStream(StringBuilder stringBuilder)
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
