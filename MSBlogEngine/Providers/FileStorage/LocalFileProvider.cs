using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MSBlogEngine.Providers.FileStorage
{
    public class LocalFileProvider : IFileProvider
    {

        public Stream GetFileStream(string path)
        {
            var directory = new DirectoryInfo(Path.GetDirectoryName(path));
            if (!directory.Exists) Directory.CreateDirectory(directory.FullName);

            return new FileStream(path, FileMode.OpenOrCreate);
        }

        public IEnumerable<string> GetFilesPaths(Func<string, bool> filter)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var directoryInfo = new DirectoryInfo(currentDirectory);
            var files = directoryInfo.GetFiles("*", SearchOption.AllDirectories).Select(f => f.FullName).ToList();
            return files.Where(filter);
        }

        public string GetFileString(string path)
        {
            throw new NotImplementedException();
        }
    }
}
