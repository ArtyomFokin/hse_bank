using System;
using System.Linq;
using HseBank.Domain;
using HseBank.Services;

namespace HseBank.Services.Implementations
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IOperationService _operationService;

        public AnalyticsService(IOperationService operationService)
        {
            _operationService = operationService;
        }

        public int CalculateNetIncome(DateTime start, DateTime end)
        {
            var operations = _operationService.GetAllOperations()
                .Where(op => op.Date >= start && op.Date <= end);

            int income = operations.Where(op => op.Type == OperationType.Income)
                                   .Sum(op => (int)op.Amount);
            int expense = operations.Where(op => op.Type == OperationType.Expense)
                                    .Sum(op => (int)op.Amount);
            return income - expense;
        }
    }
}
