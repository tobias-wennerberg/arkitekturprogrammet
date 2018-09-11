using ITH.ArchProg.T3.Banking.Domain.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Domain.Repositories
{
    public interface IBankAccountQueryRepository
    {
        BankAccount Get(string id);

        IReadOnlyList<BankAccount> List();

        Task<bool> BankAccountWithCustomerExists(string customerId);

        decimal GetBankAccountBalance(string id);

        Task<IReadOnlyList<BankTransaction>> BankTransactionList(string id);
    }
}
