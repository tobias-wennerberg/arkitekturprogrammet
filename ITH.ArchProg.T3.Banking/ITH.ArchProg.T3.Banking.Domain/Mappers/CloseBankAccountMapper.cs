using ITH.ArchProg.T3.Banking.Domain.Commands;
using ITH.ArchProg.T3.Banking.Domain.Entities;
using ITH.ArchProg.T3.Banking.Domain.Entities.BankAccountAggregate;
using ITH.ArchProg.T3.Banking.Domain.Events;
using System;

namespace ITH.ArchProg.T3.Banking.Domain.Mappers
{
    internal static class CloseBankAccountMapper
    {
        internal static BankAccountClosed ToEvent(this CloseBankAccount command, BankAccount bankAccount)
        {
            return new BankAccountClosed
            {
                Entity = bankAccount,
                Id = command.Id,
                Timestamp = DateTime.Now
            };
        }
    }
}
