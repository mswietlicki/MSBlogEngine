using System;
using System.Collections.Generic;
using System.IO;

namespace MSBlogEngine.Storage.FileStorage
{
    public interface IFileStorage
    {
        Stream GetFileStream(string path);
        IEnumerable<string> GetFilesPaths(Func<string, bool> filter);
    }
}