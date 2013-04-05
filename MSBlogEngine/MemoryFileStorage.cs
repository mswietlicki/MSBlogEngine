using System.Collections.Generic;
using System.IO;

namespace MSBlogEngine
{
    public class MemoryFileStorage : IFileStorage
    {
        static readonly Dictionary<string, Stream> _files = new Dictionary<string, Stream>();

        public Stream GetFileStream(string path)
        {
            if (_files.ContainsKey(path))
            {
                var s = _files[path];
                s.Seek(0, SeekOrigin.Begin);
                return s;
            }

            var stream = new MemoryStream();
            _files.Add(path, stream);
            return stream;
        }
    }
}