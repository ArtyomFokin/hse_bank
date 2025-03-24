using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HseBank.Domain;
using HseBank.Services;
using Newtonsoft.Json;

namespace HseBank.Proxies
{
    public class OperationServiceProxy : IOperationService
    {
        private readonly IOperationService _realService;
        private readonly string _logFilePath;

        public OperationServiceProxy(IOperationService realService, string logFilePath)
        {
            _realService = realService;
            _logFilePath = logFilePath;
        }

        private void Log(string message)
        {
            string logMessage = $"{DateTime.Now}: {message}";
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        }

        public void CreateOperation(Operation operation)
        {
            Log($"Создание операции: {JsonConvert.SerializeObject(operation)}");
            _realService.CreateOperation(operation);
        }

        public void DeleteOperation(Guid operationId)
        {
            Log($"Удаление операции с ID: {operationId}");
            _realService.DeleteOperation(operationId);
        }

        public Operation GetOperation(Guid operationId)
        {
            Log($"Получение операции с ID: {operationId}");
            return _realService.GetOperation(operationId);
        }

        public IEnumerable<Operation> GetAllOperations()
        {
            Log("Получение всех операций");
            var operations = _realService.GetAllOperations();
            Log($"Найдено операций: {operations.Count()}");
            return operations;
        }
    }
}
