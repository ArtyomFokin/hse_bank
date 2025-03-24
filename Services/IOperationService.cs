using System;
using System.Collections.Generic;
using HseBank.Domain;

namespace HseBank.Services
{
    public interface IOperationService
    {
        void CreateOperation(Operation operation);
        void DeleteOperation(Guid operationId);
        Operation GetOperation(Guid operationId);
        IEnumerable<Operation> GetAllOperations();
    }
}
