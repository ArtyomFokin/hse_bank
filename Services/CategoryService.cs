using System;
using System.Collections.Generic;
using System.Linq;
using HseBank.Domain;
using HseBank.Services;

namespace HseBank.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly List<Category> _categories = new();

        public void CreateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            if (_categories.Any(c => c.Id == category.Id))
                throw new InvalidOperationException("Категория с таким ID уже существует.");

            _categories.Add(category);
        }

        public void DeleteCategory(Guid categoryId)
        {
            var category = _categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
                throw new InvalidOperationException("Категория не найдена.");

            _categories.Remove(category);
        }

        public Category GetCategory(Guid categoryId)
        {
            var category = _categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
                throw new InvalidOperationException("Категория не найдена.");

            return category;
        }

        public IEnumerable<Category> GetAllCategories() => _categories;
    }
}
