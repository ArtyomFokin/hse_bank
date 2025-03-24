using System;
using System.Collections.Generic;
using System.IO;
using HseBank.Domain;
using HseBank.Services;
using Newtonsoft.Json;

namespace HseBank.Proxies
{
    public class BankAccountServiceProxy : IBankAccountService
    {
        private readonly IBankAccountService _realService;
        private readonly string _logFilePath;

        public BankAccountServiceProxy(IBankAccountService realService, string logFilePath)
        {
            _realService = realService;
            _logFilePath = logFilePath;
        }

        private void Log(string message)
        {
            string logMessage = $"{DateTime.Now}: {message}";
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        }

        public void CreateAccount(Account account)
        {
            Log($"Создание счета: {JsonConvert.SerializeObject(account)}");
            _realService.CreateAccount(account);
        }

        public void DeleteAccount(Guid accountId)
        {
            Log($"Удаление счета с ID: {accountId}");
            _realService.DeleteAccount(accountId);
        }

        public Account GetAccount(Guid accountId)
        {
            Log($"Получение счета с ID: {accountId}");
            return _realService.GetAccount(accountId);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            Log("Получение всех счетов");
            return _realService.GetAllAccounts();
        }
    }
}
