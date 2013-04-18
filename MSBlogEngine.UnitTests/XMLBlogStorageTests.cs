﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSBlogEngine.Models;
using MSBlogEngine.Providers;
using MSBlogEngine.Providers.FileStorage;
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
            var mock = new Mock<IFileProvider>();
            mock.Setup(o => o.GetFileStream(It.IsAny<string>())).Returns(new MemoryStream());
            container.Register<IFileProvider>(() => mock.Object);

            var storage = container.GetInstance<XMLBlogProvider>();

            storage.UpdatePost(Guid.Empty, new BlogPost());

            mock.Verify(o => o.GetFileStream(It.IsAny<string>()), Times.Once());
        }
    }
}
