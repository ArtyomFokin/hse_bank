using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HseBank.Domain;

namespace HseBank.ImportExport
{
    public class CsvExportVisitor : IExportVisitor
    {
        private readonly StringBuilder _csvBuilder = new();

        public void Visit(Account account)
        {
            _csvBuilder.AppendLine($"{account.Id},{account.Name},{account.Balance}");
        }

        public void Visit(Category category)
        {
            _csvBuilder.AppendLine($"{category.Id},{category.Name},{category.Type}");
        }

        public void Visit(Operation operation)
        {
            _csvBuilder.AppendLine($"{operation.Id},{operation.Type},{operation.Amount},{operation.Date},{operation.CategoryId}");
        }

        public void SaveToFile(string filePath)
        {
            File.WriteAllText(filePath, _csvBuilder.ToString());
        }
    }
}
