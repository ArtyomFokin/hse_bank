using System;
using System.Collections.Generic;
using HseBank.Domain;

namespace HseBank.Services
{
    public interface ICategoryService
    {
        void CreateCategory(Category category);
        void DeleteCategory(Guid categoryId);
        Category GetCategory(Guid categoryId);
        IEnumerable<Category> GetAllCategories();
    }
}
