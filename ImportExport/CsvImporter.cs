using System;
using System.Collections.Generic;
using System.Linq;
using HseBank.Domain;

namespace HseBank.ImportExport
{
    public class CsvImporter : ImporterBase
    {
        protected override IEnumerable<T> ParseContent<T>(string content)
        {
            var list = new List<T>();
            var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                // Пример для импорта Account: первая часть – ID, вторая – Name.
                if (typeof(T) == typeof(Account) && parts.Length >= 2)
                {
                    if (Guid.TryParse(parts[0], out Guid id))
                    {
                        var account = (T)(object)new Account(id, parts[1]);
                        list.Add(account);
                    }
                }
                // Можно добавить обработку для других типов, если потребуется.
            }

            return list;
        }
    }
}
