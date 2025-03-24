using System.Collections.Generic;
using System.IO;
using HseBank.Domain;

namespace HseBank.ImportExport
{
    public abstract class ImporterBase
    {
        // Импортирует данные из файла и парсит их в объекты типа T.
        public IEnumerable<T> Import<T>(string filePath) where T : class
        {
            string content = File.ReadAllText(filePath);
            return ParseContent<T>(content);
        }

        // Абстрактный метод для парсинга содержимого файла.
        protected abstract IEnumerable<T> ParseContent<T>(string content);
    }
}
