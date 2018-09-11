using ITH.ArchProg.T3.Banking.Domain.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Infrastructure.Repositories
{
    public interface IBankAccountRepository
    {
        BankAccount Get(string id);

        IReadOnlyList<BankAccount> List();

        bool Create(BankAccount bankAccount);

        bool Delete(string id);

        bool Update(BankAccount bankAccount);

        bool Create(BankTransaction transaction);

        bool UpdateEventNumber(long eventNumber);

        long? GetEventNumber();
    }
}
