using System;
using System.Collections.Generic;
using System.IO;

namespace MSBlogEngine.Providers.FileStorage
{
    public interface IFileProvider
    {
        Stream GetFileStream(string path);
        IEnumerable<string> GetFilesPaths(Func<string, bool> filter);
    }
}