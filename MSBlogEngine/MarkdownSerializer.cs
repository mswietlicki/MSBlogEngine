using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using MSBlogEngine.Models;

namespace MSBlogEngine
{
    public class MarkdownSerializer<T>
    {
        private static readonly Regex _metadataRegex = new Regex(@"^(?<name>\w+):[\t ]+(?<value>.+)$");

        public T Deserialize(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var t = Activator.CreateInstance<T>();

                GetMetadata(reader, t);

                SetValue(t, "Body", reader.ReadToEnd());

                return t;
            }
        }

        private static void GetMetadata(StreamReader reader, T t)
        {
            string line;
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                GetAndInsertMetadata(line, t);
            }
        }

        private static void GetAndInsertMetadata(string line, T t)
        {
            var lineMatch = _metadataRegex.Match(line);

            SetValue(t, lineMatch.Groups["name"].Value, lineMatch.Groups["value"].Value);
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
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.Name == "Id") continue;
                if (propertyInfo.Name != "Body") streamWriter.WriteLine("{0}:\t{1}", propertyInfo.Name, propertyInfo.GetValue(o));
                else
                {
                    streamWriter.WriteLine();
                    streamWriter.Write(propertyInfo.GetValue(o));
                }
            }
            streamWriter.Flush();
        }
    }
}
