using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MSBlogEngine.Configuration;

namespace MSBlogEngine.Providers.FileStorage
{
    public class LocalFileProvider : IFileProvider
    {
        private readonly IBlogConfiguration _configuration;

        public LocalFileProvider(IBlogConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Stream GetFileStream(string path)
        {
            var directory = new DirectoryInfo(Path.GetDirectoryName(path));
            if (!directory.Exists) Directory.CreateDirectory(directory.FullName);

            return new FileStream(path, FileMode.OpenOrCreate);
        }

        public IEnumerable<string> GetFilesPaths(Func<string, bool> filter, bool recursive = false)
        {
            var currentDirectory = _configuration.BlogPostsDirectory;
            var directoryInfo = new DirectoryInfo(currentDirectory);
            var files = directoryInfo.GetFiles("*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).Select(f => f.FullName).ToList();
            return files.Where(filter);
        }

        public string GetFileString(string path)
        {
            throw new NotImplementedException();
        }
    }
}
