using System;
using HseBank.Domain;

namespace HseBank.Factories
{
    public static class AccountFactory
    {
        public static Account Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя счета не может быть пустым.", nameof(name));
            return new Account(Guid.NewGuid(), name);
        }
    }
}
