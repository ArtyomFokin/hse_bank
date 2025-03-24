using System.Collections.Generic;
using System.Text.Json;
using HseBank.Domain;

namespace HseBank.ImportExport
{
    public class JsonImporter : ImporterBase
    {
        protected override IEnumerable<T> ParseContent<T>(string content)
        {
            var data = JsonSerializer.Deserialize<IEnumerable<T>>(content);
            return data ?? new List<T>();
        }
    }
}
