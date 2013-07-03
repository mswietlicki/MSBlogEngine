using System;
using System.Collections.Generic;
using System.IO;

namespace MSBlogEngine.Providers.FileStorage
{
    public interface IFileProvider
    {
        Stream GetFileStream(string path, bool create);
        IEnumerable<string> GetFilesPaths(Func<string, bool> filter, bool recursive = false);
        string GetFileString(string path);
    }
}