using System;
using System.Collections.Generic;
using System.IO;
using HseBank.Domain;
using HseBank.Services;
using Newtonsoft.Json;

namespace HseBank.Proxies
{
    public class CategoryServiceProxy : ICategoryService
    {
        private readonly ICategoryService _realService;
        private readonly string _logFilePath;

        public CategoryServiceProxy(ICategoryService realService, string logFilePath)
        {
            _realService = realService;
            _logFilePath = logFilePath;
        }

        private void Log(string message)
        {
            string logMessage = $"{DateTime.Now}: {message}";
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        }

        public void CreateCategory(Category category)
        {
            Log($"Создание категории: {JsonConvert.SerializeObject(category)}");
            _realService.CreateCategory(category);
        }

        public void DeleteCategory(Guid categoryId)
        {
            Log($"Удаление категории с ID: {categoryId}");
            _realService.DeleteCategory(categoryId);
        }

        public Category GetCategory(Guid categoryId)
        {
            Log($"Получение категории с ID: {categoryId}");
            return _realService.GetCategory(categoryId);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            Log("Получение всех категорий");
            return _realService.GetAllCategories();
        }
    }
}
