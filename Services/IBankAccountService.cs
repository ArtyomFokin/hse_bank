using System;
using System.Collections.Generic;
using HseBank.Domain;

namespace HseBank.Services
{
    public interface IBankAccountService
    {
        void CreateAccount(Account account);
        void DeleteAccount(Guid accountId);
        Account GetAccount(Guid accountId);
        IEnumerable<Account> GetAllAccounts();
    }
}
