using ITH.ArchProg.T3.Banking.Domain.Commands;
using ITH.ArchProg.T3.Banking.Domain.Entities.BankAccountAggregate;
using ITH.ArchProg.T3.Banking.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Domain.Mappers
{
    internal static class CreateBankAccountMapper
    {
        internal static BankAccountCreated ToEvent(this CreateBankAccount command, BankAccount bankAccount)
        {
            return new BankAccountCreated
            {
                Id = bankAccount.Id,
                ClearingNo = command.ClearingNo,
                AccountNo = bankAccount.GetAccountNo(command.ClearingNo),
                Address = command.Address,
                Fullname = command.Fullname,
                Phonenumber = command.Phonenumber,
                CustomerId = command.CustomerId,
                Timestamp = DateTime.Now,
                Entity = bankAccount
            };
        }
    }
}
