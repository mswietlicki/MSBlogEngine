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
        private Regex _metadataRegex;

        public T Deserialize(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var t = Activator.CreateInstance<T>();
                string line;

                //METADATA
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    _metadataRegex = new Regex(@"^(?<name>\w+):[\t ]+(?<value>.+)$");

                    var lineMatch = _metadataRegex.Match(line);

                    SetValue(t, lineMatch.Groups["name"].Value, lineMatch.Groups["value"].Value);
                }

                SetValue(t, "Body", reader.ReadToEnd());

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
    }
}
