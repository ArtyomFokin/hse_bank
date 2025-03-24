using System;
using HseBank.Domain;

namespace HseBank.Factories
{
    public static class OperationFactory
    {
        public static Operation Create(OperationType type, Guid accountId, uint amount, DateTime date, string? description, Guid categoryId)
        {
            if (amount == 0)
                throw new ArgumentException("Сумма операции должна быть больше нуля.", nameof(amount));
            return new Operation(Guid.NewGuid(), type, accountId, amount, date, description, categoryId);
        }
    }
}
