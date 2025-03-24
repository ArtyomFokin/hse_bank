using System;
using HseBank.Domain;
using HseBank.Services;

namespace HseBank.Commands
{
    public class UpdateCategoryCommand : IFinancialCommand
    {
        private readonly ICategoryService _categoryService;
        private readonly Guid _categoryId;
        private readonly string _newName;

        public UpdateCategoryCommand(ICategoryService categoryService, Guid categoryId, string newName)
        {
            _categoryService = categoryService;
            _categoryId = categoryId;
            _newName = newName;
        }

        public object Execute()
        {
            var category = _categoryService.GetCategory(_categoryId);
            if (category == null)
                throw new InvalidOperationException("Категория не найдена.");
            category.Name = _newName;
            Console.WriteLine($"Категория обновлена. Новое имя: {category.Name}");
            return category;
        }
    }
}
