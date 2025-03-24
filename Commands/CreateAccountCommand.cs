using System;
using HseBank.Domain;
using HseBank.Factories;
using HseBank.Services;

namespace HseBank.Commands
{
    public class CreateAccountCommand : IFinancialCommand
    {
        private readonly IBankAccountService _accountService;
        private readonly string _accountName;

        public CreateAccountCommand(IBankAccountService accountService, string accountName)
        {
            _accountService = accountService;
            _accountName = accountName;
        }

        public object Execute()
        {
            var account = AccountFactory.Create(_accountName);
            _accountService.CreateAccount(account);
            return account;
        }
    }
}
