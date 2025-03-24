using System;
using HseBank.Domain;
using HseBank.Factories;
using HseBank.Services;

namespace HseBank.Commands
{
    public class CreateOperationCommand : IFinancialCommand
    {
        private readonly IOperationService _operationService;
        private readonly OperationType _operationType;
        private readonly Guid _accountId;
        private readonly uint _amount;
        private readonly DateTime _date;
        private readonly string? _description;
        private readonly Guid _categoryId;

        public CreateOperationCommand(IOperationService operationService, OperationType operationType, Guid accountId,
            uint amount, DateTime date, string? description, Guid categoryId)
        {
            _operationService = operationService;
            _operationType = operationType;
            _accountId = accountId;
            _amount = amount;
            _date = date;
            _description = description;
            _categoryId = categoryId;
        }

        public object Execute()
        {
            var operation = OperationFactory.Create(_operationType, _accountId, _amount, _date, _description, _categoryId);
            _operationService.CreateOperation(operation);
            return operation;
        }
    }
}
