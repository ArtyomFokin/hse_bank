using System;
using System.Collections.Generic;
using System.Linq;
using HseBank.Domain;
using HseBank.Services;

namespace HseBank.Services.Implementations
{
    public class BankAccountService : IBankAccountService
    {
        private readonly List<Account> _accounts = new();

        public void CreateAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (_accounts.Any(a => a.Id == account.Id))
                throw new InvalidOperationException("Счет с таким ID уже существует.");

            _accounts.Add(account);
        }

        public void DeleteAccount(Guid accountId)
        {
            var account = _accounts.FirstOrDefault(a => a.Id == accountId);
            if (account == null)
                throw new InvalidOperationException("Счет не найден.");

            _accounts.Remove(account);
        }

        public Account GetAccount(Guid accountId)
        {
            var account = _accounts.FirstOrDefault(a => a.Id == accountId);
            if (account == null)
                throw new InvalidOperationException("Счет не найден.");

            return account;
        }

        public IEnumerable<Account> GetAllAccounts() => _accounts;
    }
}
