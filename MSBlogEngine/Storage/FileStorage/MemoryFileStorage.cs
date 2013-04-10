using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MSBlogEngine.Storage.FileStorage
{
    public class MemoryFileStorage : IFileStorage
    {
        static readonly Dictionary<string, Stream> Files = new Dictionary<string, Stream>();

        public Stream GetFileStream(string path)
        {
            if (Files.ContainsKey(path))
            {
                var s = Files[path];
                s.Seek(0, SeekOrigin.Begin);
                return s;
            }

            var stream = new MemoryFileStream();
            Files.Add(path, stream);
            return stream;
        }

        public IEnumerable<string> GetFilesPaths(Func<string, bool> filter)
        {
            return Files.Keys.Where(filter);
        }
    }


    public class MemoryFileStream : Stream
    {
        private MemoryStream _memoryStream;

        public MemoryFileStream()
        {
            _memoryStream = new MemoryStream();
        }

        public override void Flush()
        {
            
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _memoryStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _memoryStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _memoryStream.Write(buffer, offset, count);
        }

        public override bool CanRead
        {
            get { return _memoryStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _memoryStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _memoryStream.CanWrite; }
        }

        public override long Length
        {
            get { return _memoryStream.Length; }
        }

        public override long Position { get { return _memoryStream.Position; } set { _memoryStream.Position = value; } }
    }
}