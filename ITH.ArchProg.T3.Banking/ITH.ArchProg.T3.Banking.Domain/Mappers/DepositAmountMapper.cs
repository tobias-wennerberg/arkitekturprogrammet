using ITH.ArchProg.T3.Banking.Domain.Commands;
using ITH.ArchProg.T3.Banking.Domain.Entities.BankAccountAggregate;
using ITH.ArchProg.T3.Banking.Domain.Events;
using System;

namespace ITH.ArchProg.T3.Banking.Domain.Mappers
{
    internal static class DepositAmountMapper
    {
        internal static AmountDeposited ToEvent(this DepositAmount command, BankAccount bankAccount)
        {
            return new AmountDeposited
            {
                BankTransactionId = Guid.NewGuid().ToString(),
                Entity = bankAccount,
                Amount = command.Amount,
                BankAccountId = command.BankAccountId,
                Sender = command.Sender,
                Timestamp = DateTime.Now
            };
        }
    }
}
