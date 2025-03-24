using System;

namespace HseBank.Domain
{
    public enum OperationType
    {
        Income,
        Expense
    }

    public class Operation
    {
        public Guid Id { get; }
        public OperationType Type { get; }
        public Guid AccountId { get; }
        public uint Amount { get; }
        public DateTime Date { get; }
        public string? Description { get; }
        public Guid CategoryId { get; }

        public Operation(Guid id, OperationType type, Guid accountId, uint amount, DateTime date, string? description, Guid categoryId)
        {
            if (amount == 0)
                throw new ArgumentException("Сумма операции должна быть больше нуля.", nameof(amount));

            Id = id;
            Type = type;
            AccountId = accountId;
            Amount = amount;
            Date = date;
            Description = description;
            CategoryId = categoryId;
        }

        public override string ToString() =>
            $"Operation: {Type}, Amount: {Amount}, Date: {Date}, Category: {CategoryId}, ID: {Id}";
    }
}
