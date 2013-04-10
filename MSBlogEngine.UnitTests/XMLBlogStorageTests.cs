using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSBlogEngine.Models;
using MSBlogEngine.Storage;
using MSBlogEngine.Storage.FileStorage;
using Moq;
using SimpleInjector;
using Xunit;

namespace MSBlogEngine.UnitTests
{
    public class XMLBlogStorageTests
    {
        [Fact]
        public void UpdatePost()
        {
            var container = new Container();
            var mock = new Mock<IFileStorage>();
            mock.Setup(o => o.GetFileStream(It.IsAny<string>())).Returns(new MemoryStream());
            container.Register<IFileStorage>(() => mock.Object);

            var storage = container.GetInstance<XMLBlogStorage>();


            storage.UpdatePost(0, new BlogPost());

            mock.Verify(o => o.GetFileStream(It.IsAny<string>()), Times.Once());
        }
    }
}
