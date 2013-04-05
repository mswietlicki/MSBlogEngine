using System.Collections.Generic;
using System.IO;

namespace MSBlogEngine
{
    public class MemoryFileStorage : IFileStorage
    {
        readonly Dictionary<string, Stream> _files = new Dictionary<string, Stream>();

        public Stream GetFileStream(string path)
        {
            if (_files.ContainsKey(path)) return _files[path];

            var stream = new MemoryStream();
            _files.Add(path, stream);
            return stream;
        }
    }
}