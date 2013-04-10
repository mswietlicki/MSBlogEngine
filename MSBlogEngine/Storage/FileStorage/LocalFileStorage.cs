using System;
using System.Collections.Generic;
using System.IO;

namespace MSBlogEngine.Storage.FileStorage
{
    public class LocalFileStorage: IFileStorage
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
