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
        public void MarkdownDeserialiseTextTest()
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

        [Fact]
        public void MarkdownSerialiseTest()
        {
            var blogPost = new BlogPost
                {
                    Title = "Test", CreateDate = new DateTime(2013, 4, 4, 15, 30, 30), Body = "Test body"
                };

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Title:\tTest");
            stringBuilder.AppendLine("CreateDate:\t" + new DateTime(2013, 4, 4, 15, 30, 30));
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("Test body");

            var memoryStream = new MemoryStream();

            var serializer = new MarkdownSerializer<BlogPost>();
            serializer.Serialize(memoryStream, blogPost);

            memoryStream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(memoryStream))
            {
                var str = reader.ReadToEnd().Trim();
                var s = stringBuilder.ToString().Trim();
                Assert.Equal(str, s);
            }
            
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
