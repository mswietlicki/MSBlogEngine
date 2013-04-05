using System.IO;

namespace MSBlogEngine
{
    public interface IFileStorage
    {
        Stream GetFileStream(string path);
    }
}