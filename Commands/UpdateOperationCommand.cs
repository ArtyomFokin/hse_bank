using System;
using HseBank.Domain;
using HseBank.Services;
using HseBank.Factories;

namespace HseBank.Commands
{
    public class UpdateOperationCommand : IFinancialCommand
    {
        private readonly IOperationService _operationService;
        private readonly Guid _operationId;
        private readonly OperationType _newType;
        private readonly uint _newAmount;
        private readonly DateTime _newDate;
        private readonly string? _newDescription;
        private readonly Guid _newCategoryId;

        public UpdateOperationCommand(IOperationService operationService, Guid operationId,
            OperationType newType, uint newAmount, DateTime newDate, string? newDescription, Guid newCategoryId)
        {
            _operationService = operationService;
            _operationId = operationId;
            _newType = newType;
            _newAmount = newAmount;
            _newDate = newDate;
            _newDescription = newDescription;
            _newCategoryId = newCategoryId;
        }

        public object Execute()
        {
            var op = _operationService.GetOperation(_operationId);
            if (op == null)
                throw new InvalidOperationException("Операция не найдена.");

            _operationService.DeleteOperation(_operationId);
            var newOp = OperationFactory.Create(_newType, op.AccountId, _newAmount, _newDate, _newDescription, _newCategoryId);
            _operationService.CreateOperation(newOp);
            Console.WriteLine($"Операция обновлена. Новый ID: {newOp.Id}");
            return newOp;
        }
    }
}
