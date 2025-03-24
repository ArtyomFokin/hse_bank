using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using HseBank.Domain;

namespace HseBank.ImportExport
{
    public class YamlExportVisitor : IExportVisitor
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

        public void SaveToFile(string filePath)
        {
            var serializer = new SerializerBuilder().Build();
            var exportData = new { Accounts = _accounts, Categories = _categories, Operations = _operations };
            string yaml = serializer.Serialize(exportData);
            File.WriteAllText(filePath, yaml);
        }
    }
}
