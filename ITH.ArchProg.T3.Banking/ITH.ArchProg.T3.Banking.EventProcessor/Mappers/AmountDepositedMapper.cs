using ITH.ArchProg.T3.Banking.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.EventProcessor.Mappers
{
    internal static class AmountDepositedMapper
    {
        internal static Domain.ReadModels.BankTransaction ToReadModel(this AmountDeposited amountDeposited, string reciever)
        {
            return new Domain.ReadModels.BankTransaction
            {
                Id = string.Empty,
                Amount = amountDeposited.Amount,
                BankAccountId = amountDeposited.BankAccountId,
                Reciever = reciever,
                Sender = amountDeposited.Sender,
                Timestamp = amountDeposited.Timestamp,
            };
        }

        internal static void Map(this AmountDeposited amountDeposited, Domain.ReadModels.BankAccount bankAccount)
        {
            bankAccount.Amount += amountDeposited.Amount;
            bankAccount.Timestamp = amountDeposited.Timestamp;
        }
    }
}
