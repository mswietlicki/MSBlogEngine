using System;
using System.Collections.Generic;
using System.IO;

namespace MSBlogEngine.Providers.FileStorage
{
    public class LocalFileProvider: IFileProvider
    {

        public Stream GetFileStream(string path)
        {
            var directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName)) Directory.CreateDirectory(directoryName);

            return new FileStream(path, FileMode.OpenOrCreate);
        }

        public IEnumerable<string> GetFilesPaths(Func<string, bool> filter)
        {
            throw new NotImplementedException();
        }
    }
}
