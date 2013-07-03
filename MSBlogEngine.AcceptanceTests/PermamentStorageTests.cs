using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MSBlogEngine.Configuration;
using MSBlogEngine.Controllers;
using MSBlogEngine.Models;
using MSBlogEngine.Providers;
using MSBlogEngine.Providers.FileStorage;
using Newtonsoft.Json;
using SimpleInjector;
using Xunit;
using MSBlogEngine;

namespace MSBlogEngine.AcceptanceTests
{
    public class PermamentStorageTests
    {

#if !DEBUG
        [Fact]
#endif
        public void PutAndSaveBlogPost()
        {
            var blogPost = GetExampleBlogPost();

            string id;

            using (var web = new HttpClientFactory().Create())
            {
                var response = web.PutAsJsonAsync("", blogPost).Result;
                Assert.True(response.IsSuccessStatusCode, "Status code : " + response.StatusCode);

                id = response.Content.GetAndDeserializeJsonResult<string>();
            }

            Assert.Equal(Global.Container.GetInstance<IBlogProvider>().GetPost(id), blogPost);
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

#if !DEBUG
        [Fact]
#endif
        public void SavePostToDisk()
        {
            var post = GetExampleBlogPost();

            var container = new Container();
            container.Register<IBlogConfiguration>(() => new ConfigFileReader().GetBlogConfiguration());
            container.Register<IFileProvider>(container.GetInstance<LocalFileProvider>);
            container.Register<IBlogProvider>(container.GetInstance<XMLBlogProvider>);
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
