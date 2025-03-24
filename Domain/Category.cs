using System;

namespace HseBank.Domain
{
    public enum CategoryType
    {
        Income,
        Expense
    }

    public class Category
    {
        public Guid Id { get; }
        public CategoryType Type { get; }
        public string Name { get; set; }

        public Category(Guid id, CategoryType type, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя категории не может быть пустым.", nameof(name));

            Id = id;
            Type = type;
            Name = name;
        }

        public override string ToString() =>
            $"Category: {Name} (ID: {Id}), Type: {Type}";
    }
}
