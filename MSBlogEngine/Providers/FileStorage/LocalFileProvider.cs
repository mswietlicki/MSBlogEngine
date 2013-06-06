using System;
using System.Collections.Generic;
using System.IO;

namespace MSBlogEngine.Providers.FileStorage
{
    public class LocalFileProvider: IFileProvider
    {

        public Stream GetFileStream(string path)
        {
            var directory = new DirectoryInfo(Path.GetDirectoryName(path));
            if (!directory.Exists) Directory.CreateDirectory(directory.FullName);

            return new FileStream(path, FileMode.OpenOrCreate);
        }

        public IEnumerable<string> GetFilesPaths(Func<string, bool> filter)
        {
            throw new NotImplementedException();
        }

        public string GetFileString(string path)
        {
            throw new NotImplementedException();
        }
    }
}
