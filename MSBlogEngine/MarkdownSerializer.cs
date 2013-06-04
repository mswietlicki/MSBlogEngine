using System;
using System.IO;
using System.Linq;
using MarkdownSharp;

namespace MSBlogEngine
{
    public class MarkdownSerializer<T>
    {
        public T Deserialize(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var engine = new Markdown();
                var source = reader.ReadToEnd();
                var metadata = engine.GetMetadata(source);

                var t = Activator.CreateInstance<T>();

                foreach (var data in metadata)
                {
                    SetValue(t, data.Key, data.Value.FirstOrDefault());
                }

                return t;
            }
        }

        private static void SetValue(T t, string name, string value)
        {
            var propertyInfo = typeof(T).GetProperty(name);
            if (propertyInfo.PropertyType == typeof(string))
                propertyInfo.SetValue(t, value);
            else
            {
                var obj = Convert.ChangeType(value, propertyInfo.PropertyType);
                propertyInfo.SetValue(t, obj);
            }
        }

        public static G Parse<G>(object obj, string key)
        {
            G value;

            try
            {
                value = (G)Convert.ChangeType(obj, typeof(G));
            }
            catch
            {
                value = default(G);
            }

            return value;
        }

        public void Serialize(Stream stream, object o)
        {
            var streamWriter = new StreamWriter(stream);

            var properties = o.GetType().GetProperties();
            foreach (var propertyInfo in properties.Where(propertyInfo => propertyInfo.Name != "Id" && propertyInfo.Name != "Body"))
            {
                streamWriter.WriteLine("{0}:\t{1}", propertyInfo.Name, propertyInfo.GetValue(o));
            }

            {
                var propertyInfo = properties.First(p => p.Name == "Body");
                streamWriter.WriteLine();
                streamWriter.Write(propertyInfo.GetValue(o));
            }

            streamWriter.Flush();
        }
    }
}
