using System;
using System.Collections.Generic;
using System.Linq;
using HseBank.Domain;
using HseBank.Services;

namespace HseBank.Services.Implementations
{
    public class OperationService : IOperationService
    {
        private readonly List<Operation> _operations = new();

        public void CreateOperation(Operation operation)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));

            if (_operations.Any(o => o.Id == operation.Id))
                throw new InvalidOperationException("Операция с таким ID уже существует.");

            _operations.Add(operation);
        }

        public void DeleteOperation(Guid operationId)
        {
            var op = _operations.FirstOrDefault(o => o.Id == operationId);
            if (op == null)
                throw new InvalidOperationException("Операция не найдена.");

            _operations.Remove(op);
        }

        public Operation GetOperation(Guid operationId)
        {
            var op = _operations.FirstOrDefault(o => o.Id == operationId);
            if (op == null)
                throw new InvalidOperationException("Операция не найдена.");

            return op;
        }

        public IEnumerable<Operation> GetAllOperations() => _operations;
    }
}
