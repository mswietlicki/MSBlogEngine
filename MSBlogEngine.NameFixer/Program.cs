using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSBlogEngine.NameFixer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Any())
            {
                Console.Error.WriteLine("Directory needed!");
            }

            var dir = new DirectoryInfo(args[0]);

            foreach (var file in dir.GetFiles())
            {
                var name = file.Name;

                name = name.ToLower();

                name = RemoveAccent(name);
                name = name.Replace(" ", "-");
                file.MoveTo(dir.FullName + "\\" + name);
            }
        }

        public static string RemoveAccent(string txt)
        {
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(txt);

            return Encoding.ASCII.GetString(bytes);

        }
    }
}
