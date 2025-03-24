using System;
using HseBank.Domain;

namespace HseBank.Factories
{
    public static class CategoryFactory
    {
        public static Category Create(CategoryType type, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя категории не может быть пустым.", nameof(name));
            return new Category(Guid.NewGuid(), type, name);
        }
    }
}
