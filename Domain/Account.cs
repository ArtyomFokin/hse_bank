using System;
using System.Collections.Generic;

namespace HseBank.Domain
{
    public class Account
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public int Balance { get; private set; }
        private readonly List<Operation> _operations;

        public Account(Guid id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя счета не может быть пустым.", nameof(name));

            Id = id;
            Name = name;
            Balance = 0;
            _operations = new List<Operation>();
        }

        public IReadOnlyList<Operation> GetOperations() => _operations.AsReadOnly();

        // Регистрация операции и обновление баланса
        public void RegisterOperation(Operation op)
        {
            if (op == null)
                throw new ArgumentNullException(nameof(op));

            Balance += op.Type == OperationType.Income ? (int)op.Amount : -(int)op.Amount;
            _operations.Add(op);
        }

        public override string ToString() =>
            $"Account: {Name} (ID: {Id}), Balance: {Balance}";
    }
}
