using System;
using HseBank.Domain;
using HseBank.Services;

namespace HseBank.Commands
{
    public class DeleteAccountCommand : IFinancialCommand
    {
        private readonly IBankAccountService _accountService;
        private readonly Guid _accountId;

        public DeleteAccountCommand(IBankAccountService accountService, Guid accountId)
        {
            _accountService = accountService;
            _accountId = accountId;
        }

        public object Execute()
        {
            _accountService.DeleteAccount(_accountId);
            Console.WriteLine($"Счет с ID {_accountId} удален.");
            return null;
        }
    }
}
