using System;
using HseBank.Domain;
using HseBank.Services;

namespace HseBank.Commands
{
    public class DeleteCategoryCommand : IFinancialCommand
    {
        private readonly ICategoryService _categoryService;
        private readonly Guid _categoryId;

        public DeleteCategoryCommand(ICategoryService categoryService, Guid categoryId)
        {
            _categoryService = categoryService;
            _categoryId = categoryId;
        }

        public object Execute()
        {
            _categoryService.DeleteCategory(_categoryId);
            Console.WriteLine($"Категория с ID {_categoryId} удалена.");
            return null;
        }
    }
}
