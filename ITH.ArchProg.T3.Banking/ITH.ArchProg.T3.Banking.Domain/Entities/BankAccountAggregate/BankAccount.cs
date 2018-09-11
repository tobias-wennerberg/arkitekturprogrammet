using FluentValidation;
using ITH.ArchProg.T3.Banking.Domain.Commands;
using ITH.ArchProg.T3.Banking.Domain.Commands.Validators;
using ITH.ArchProg.T3.Banking.Domain.Events;
using ITH.ArchProg.T3.Banking.Domain.Mappers;
using ITH.ArchProg.T3.Banking.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Domain.Entities.BankAccountAggregate
{
    public class BankAccount : Entity, 
        IRequestHandler<CreateBankAccount>,
        IRequestHandler<DepositAmount>,
        IRequestHandler<WithdrawAmount>,
        IRequestHandler<CloseBankAccount>
    {
        private const string ClearingNoFormat = "0000";
        private const string IdFormat = "0000000";
        internal const decimal MaxDepositLimit = 100000;
        internal const decimal MaxWithdrawLimit = 100000;

        private readonly IEventRepository eventRepository;
        private readonly IValidator<DepositAmount> depositAmountValidator;
        private readonly IValidator<ValidatonContext<WithdrawAmount, BankAccount>> withdrawAmountValidator;

        internal BankAccount(
            IEventRepository eventRepository, 
            IValidator<DepositAmount> depositAmountValidator,
            IValidator<ValidatonContext<WithdrawAmount, BankAccount>> withdrawAmountValidator)
        {
            this.eventRepository = eventRepository;
            this.depositAmountValidator = depositAmountValidator;
            this.withdrawAmountValidator = withdrawAmountValidator;
            transactions = new List<BankTransaction>();
        }

        public int ClearingNo { get; private set; }
            
        public string AccountNo => GetAccountNo(ClearingNo);

        private List<BankTransaction> transactions; 

        public string CustomerId { get; private set; }

        public string Fullname { get; private set; }

        public string Address { get; private set; }

        public string Phonenumber { get; private set; }

        public decimal Amount { get; private set; }

        public IReadOnlyList<BankTransaction> Transactions => transactions;

        public DateTime Timestamp { get; private set; }

        public bool IsClosed { get; private set; }

        public async Task Handle(CreateBankAccount message, CancellationToken cancellationToken)
        {
            Id = await GetNewId();

            var @event = message.ToEvent(this);

            Apply(@event);

            await eventRepository.Save(@event);
        }

        public async Task Handle(DepositAmount message, CancellationToken cancellationToken)
        {
            Id = message.BankAccountId;

            depositAmountValidator.ValidateAndThrow(message);
            
            var @event = message.ToEvent(this);

            Apply(@event);

            await eventRepository.Save(@event);
        }

        public async Task Handle(WithdrawAmount message, CancellationToken cancellationToken)
        {
            Id = message.BankAccountId;
            LoadPreviousEvents();

            withdrawAmountValidator.ValidateAndThrow(new ValidatonContext<WithdrawAmount, BankAccount>(message, this));

            var @event = message.ToEvent(this);

            Apply(@event);

            await eventRepository.Save(@event);
        }

        public async Task Handle(CloseBankAccount message, CancellationToken cancellationToken)
        {
            Id = message.Id;

            var @event = message.ToEvent(this);

            Apply(@event);

            await eventRepository.Save(@event);
        }

        private void Apply(BankAccountCreated bankAccountCreated)
        {
            Id = bankAccountCreated.Id;
            ClearingNo = bankAccountCreated.ClearingNo;
            CustomerId = bankAccountCreated.CustomerId;
            Fullname = bankAccountCreated.Fullname;
            Address = bankAccountCreated.Address;
            Phonenumber = bankAccountCreated.Phonenumber;
            Timestamp = bankAccountCreated.Timestamp;
        }

        private void Apply(AmountDeposited amountDeposited)
        {
            Amount += amountDeposited.Amount;
            transactions.Add(new BankTransaction(
                amountDeposited.BankTransactionId,
                amountDeposited.BankAccountId,
                amountDeposited.Amount,
                amountDeposited.Sender,
                AccountNo,
                amountDeposited.Timestamp));
        }

        private void Apply(AmountWithdrawn amountWithdrawn)
        {
            Amount -= amountWithdrawn.Amount;
            transactions.Add(new BankTransaction(
                amountWithdrawn.BankTransactionId,
                amountWithdrawn.BankAccountId,
                -amountWithdrawn.Amount,
                AccountNo,
                amountWithdrawn.Reciever,
                amountWithdrawn.Timestamp));
        }

        private void Apply(BankAccountClosed bankAccountClosed)
        {
            IsClosed = true;
        }

        internal string GetAccountNo(int clearingNo)
        {
            var accountNo = string.Empty;

            if (clearingNo > 0)
            {
                accountNo = clearingNo.ToString(ClearingNoFormat) + Id;
            }
            return accountNo;
        }

        private async Task<string> GetNewId()
        {
            var lastCreatedEvent = await eventRepository.GetLast<BankAccount, BankAccountCreated>();
            var id = lastCreatedEvent != null ? int.Parse(lastCreatedEvent.Id) + 1 : 1;
            return id.ToString(IdFormat);
        }
        
        private void ApplyPreviousEvents(IReadOnlyList<IEvent> previousEvents)
        {
            foreach (var previousEvent in previousEvents)
            {
                if (previousEvent is BankAccountCreated bankAccountCreated)
                {
                    Apply(bankAccountCreated);
                }
                else if (previousEvent is AmountDeposited amountDeposited)
                {
                    Apply(amountDeposited);
                }
                else if (previousEvent is AmountWithdrawn amountWithdrawn)
                {
                    Apply(amountWithdrawn);
                }
                else if (previousEvent is BankAccountClosed bankAccountClosed)
                {
                    Apply(bankAccountClosed);
                }
            }
        }

        private void LoadPreviousEvents()
        {
            //how to solve this???

            var previousEvents = eventRepository.GetPrevious(this);
            ApplyPreviousEvents(previousEvents);
        }
    }
}
