using ITH.ArchProg.T3.Banking.Domain.Commands;
using ITH.ArchProg.T3.Banking.Domain.Entities.BankAccountAggregate;
using ITH.ArchProg.T3.Banking.Domain.Events;
using System;

namespace ITH.ArchProg.T3.Banking.Domain.Mappers
{
    internal static class WithdrawAmountMapper
    {
        internal static AmountWithdrawn ToEvent(this WithdrawAmount command, BankAccount bankAccount)
        {
            return new AmountWithdrawn
            {
                BankTransactionId = Guid.NewGuid().ToString(),
                Entity = bankAccount,
                Amount = command.Amount,
                BankAccountId = command.BankAccountId,
                Reciever = command.Reciever,
                Timestamp = DateTime.Now
            };
        }
    }
}
