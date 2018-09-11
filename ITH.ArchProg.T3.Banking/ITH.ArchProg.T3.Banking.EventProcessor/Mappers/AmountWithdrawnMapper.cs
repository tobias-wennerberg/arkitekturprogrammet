using ITH.ArchProg.T3.Banking.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.EventProcessor.Mappers
{
    internal static class AmountWithdrawnMapper
    {
        internal static Domain.ReadModels.BankTransaction ToReadModel(this AmountWithdrawn amountWithdrawn, string sender)
        {
            return new Domain.ReadModels.BankTransaction
            {
                Id = string.Empty,
                Amount = -amountWithdrawn.Amount,
                BankAccountId = amountWithdrawn.BankAccountId,
                Reciever = amountWithdrawn.Reciever,
                Sender = sender,
                Timestamp = amountWithdrawn.Timestamp,
            };
        }

        internal static void Map(this AmountWithdrawn amountWithdrawn, Domain.ReadModels.BankAccount bankAccount)
        {
            bankAccount.Amount -= amountWithdrawn.Amount;
            bankAccount.Timestamp = amountWithdrawn.Timestamp;
        }
    }
}
