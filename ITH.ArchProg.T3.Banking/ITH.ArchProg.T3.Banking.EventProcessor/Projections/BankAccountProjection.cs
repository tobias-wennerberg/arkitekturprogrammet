using ITH.ArchProg.T3.Banking.Domain.Events;
using ITH.ArchProg.T3.Banking.EventProcessor.Mappers;
using ITH.ArchProg.T3.Banking.Infrastructure.EventSourcing;
using ITH.ArchProg.T3.Banking.Infrastructure.Repositories;

namespace ITH.ArchProg.T3.Banking.EventProcessor.Projections
{
    internal class BankAccountProjection : Projection<Domain.Entities.BankAccountAggregate.BankAccount>
    {
        private readonly IBankAccountRepository bankAccountRepository;

        internal BankAccountProjection(IBankAccountRepository bankAccountRepository)
        {
            this.bankAccountRepository = bankAccountRepository;
        }

        public override void Project(IEvent @event, long eventNumber)
        {
            if (@event is BankAccountCreated bankAccountCreated)
            {
                var newBankAccount = bankAccountCreated.ToReadModel();
                bankAccountRepository.Create(newBankAccount);
            }
            else if (@event is AmountDeposited amountDeposited)
            {
                var bankAccount = bankAccountRepository.Get(amountDeposited.BankAccountId);
                var transaction = amountDeposited.ToReadModel(bankAccount.AccountNo);
                amountDeposited.Map(bankAccount);
                bankAccountRepository.Update(bankAccount);
                bankAccountRepository.Create(transaction);

            }
            else if (@event is AmountWithdrawn amountWithdrawn)
            {
                var bankAccount = bankAccountRepository.Get(amountWithdrawn.BankAccountId);
                var transaction = amountWithdrawn.ToReadModel(bankAccount.AccountNo);
                amountWithdrawn.Map(bankAccount);
                bankAccountRepository.Update(bankAccount);
                bankAccountRepository.Create(transaction);
            }
            else if (@event is BankAccountClosed bankAccountClosed)
            {
                var bankAccount = bankAccountRepository.Get(bankAccountClosed.Id);
                bankAccountClosed.Map(bankAccount);
                bankAccountRepository.Update(bankAccount);
            }
            bankAccountRepository.UpdateEventNumber(eventNumber);
        }

        public override long? GetLastEventNumber()
        {
            return bankAccountRepository.GetEventNumber();
        }
    }
}
