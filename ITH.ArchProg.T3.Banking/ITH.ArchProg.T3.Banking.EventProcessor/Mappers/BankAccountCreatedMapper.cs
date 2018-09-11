using ITH.ArchProg.T3.Banking.Domain.Events;

namespace ITH.ArchProg.T3.Banking.EventProcessor.Mappers
{
    internal static class BankAccountCreatedMapper
    {
        internal static Domain.ReadModels.BankAccount ToReadModel(this BankAccountCreated bankAccountCreated)
        {
            return new Domain.ReadModels.BankAccount
            {
                Id = bankAccountCreated.Id.ToString(),
                ClearingNo = bankAccountCreated.ClearingNo.ToString(),
                AccountNo = bankAccountCreated.AccountNo,
                Address = bankAccountCreated.Address,
                Fullname = bankAccountCreated.Fullname,
                Phonenumber = bankAccountCreated.Phonenumber,
                CustomerId = bankAccountCreated.CustomerId,
                Timestamp = bankAccountCreated.Timestamp,
                Amount = 0,
                IsClosed = false
            };
        }
    }
}
