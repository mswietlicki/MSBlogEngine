using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MSBlogEngine.Controllers;
using MSBlogEngine.Models;
using MSBlogEngine.Storage;
using MSBlogEngine.Storage.FileStorage;
using Newtonsoft.Json;
using SimpleInjector;
using Xunit;
using MSBlogEngine;

namespace MSBlogEngine.AcceptanceTests
{
    public class PermamentStorageTests
    {

        [Fact]
        public void PutAndSaveBlogPost()
        {
            var blogPost = GetExampleBlogPost();

            int id;

            using (var web = new HttpClientFactory().Create())
            {
                var response = web.PutAsJsonAsync("", blogPost).Result;
                Assert.True(response.IsSuccessStatusCode, "Status code : " + response.StatusCode);

                id = response.Content.GetAndDeserializeJsonResult<int>();
            }

            Assert.Equal(Global.Container.GetInstance<IBlogStorage>().GetPost(id), blogPost);
        }

        private static BlogPost GetExampleBlogPost()
        {
            return new BlogPost
                {
                    Title = "Test",
                    CreateDate = new DateTime(2013, 4, 4, 15, 30, 30),
                    Body = "Test body"
                };
        }

        [Fact]
        public void SavePostToDisk()
        {
            var post = GetExampleBlogPost();

            var container = new Container();
            container.Register<IFileStorage>(() => new LocalFileStorage());
            container.Register<IBlogStorage>(container.GetInstance<XMLBlogStorage>);
            var blogController = container.GetInstance<BlogController>();

            //ACT
            var id = blogController.Put(post);
            var result = blogController.Get(id);

            //VERIFY
            var path = string.Format("Posts\\{0}.xml", id);
            Assert.True(File.Exists(path));

            Assert.Equal(post, result);

            //CleanUp
            File.Delete(path);
        }
    }
}
