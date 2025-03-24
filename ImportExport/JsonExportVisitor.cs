using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using HseBank.Domain;

namespace HseBank.ImportExport
{
    public class JsonExportVisitor : IExportVisitor
    {
        private readonly List<Account> _accounts = new();
        private readonly List<Category> _categories = new();
        private readonly List<Operation> _operations = new();

        public void Visit(Account account)
        {
            _accounts.Add(account);
        }

        public void Visit(Category category)
        {
            _categories.Add(category);
        }

        public void Visit(Operation operation)
        {
            _operations.Add(operation);
        }

        // Сохраняет экспортированные данные в файл в формате JSON.
        public void SaveToFile(string filePath)
        {
            var exportData = new
            {
                Accounts = _accounts,
                Categories = _categories,
                Operations = _operations
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(exportData, options);
            File.WriteAllText(filePath, json);
        }
    }
}
