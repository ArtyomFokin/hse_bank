using System;
using HseBank.Domain;
using HseBank.Services;

namespace HseBank.Commands
{
    public class UpdateAccountCommand : IFinancialCommand
    {
        private readonly IBankAccountService _accountService;
        private readonly Guid _accountId;
        private readonly string _newName;

        public UpdateAccountCommand(IBankAccountService accountService, Guid accountId, string newName)
        {
            _accountService = accountService;
            _accountId = accountId;
            _newName = newName;
        }

        public object Execute()
        {
            var account = _accountService.GetAccount(_accountId);
            if (account == null)
                throw new InvalidOperationException("Счет не найден.");
            account.Name = _newName;
            Console.WriteLine($"Счет обновлен. Новый имя: {account.Name}");
            return account;
        }
    }
}
