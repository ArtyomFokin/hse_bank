using System;
using HseBank.Domain;
using HseBank.Services;

namespace HseBank.Commands
{
    public class DeleteOperationCommand : IFinancialCommand
    {
        private readonly IOperationService _operationService;
        private readonly Guid _operationId;

        public DeleteOperationCommand(IOperationService operationService, Guid operationId)
        {
            _operationService = operationService;
            _operationId = operationId;
        }

        public object Execute()
        {
            _operationService.DeleteOperation(_operationId);
            Console.WriteLine($"Операция с ID {_operationId} удалена.");
            return null;
        }
    }
}
