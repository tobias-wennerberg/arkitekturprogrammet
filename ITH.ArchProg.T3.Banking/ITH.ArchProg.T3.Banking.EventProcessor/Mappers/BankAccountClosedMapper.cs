using ITH.ArchProg.T3.Banking.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.EventProcessor.Mappers
{
    internal static class BankAccountClosedMapper
    {
        internal static void Map(this BankAccountClosed bankAccountClosed, Domain.ReadModels.BankAccount bankAccount)
        {
            bankAccount.IsClosed = true;
            bankAccount.Timestamp = bankAccountClosed.Timestamp;
        }
    }
}
