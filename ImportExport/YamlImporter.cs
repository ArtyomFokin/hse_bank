using System.Collections.Generic;
using YamlDotNet.Serialization;
using HseBank.Domain;

namespace HseBank.ImportExport
{
    public class YamlImporter : ImporterBase
    {
        protected override IEnumerable<T> ParseContent<T>(string content)
        {
            var deserializer = new DeserializerBuilder().Build();
            var data = deserializer.Deserialize<IEnumerable<T>>(content);
            return data ?? new List<T>();
        }
    }
}
