using ITH.ArchProg.T3.Banking.Domain.ReadModels;
using ITH.ArchProg.T3.Banking.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Domain.Queries.Handlers
{
    public class BankAccountQueriesHandler : 
        IRequestHandler<BankAccountCustomerExists, bool>,
        IRequestHandler<GetBankAccount, BankAccount>,
        IRequestHandler<GetBankAccountBalance, decimal>,
        IRequestHandler<GetBankTransactionList, IReadOnlyList<BankTransaction>>
    {
        private readonly IBankAccountQueryRepository repository;

        internal BankAccountQueriesHandler(IBankAccountQueryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Handle(BankAccountCustomerExists request, CancellationToken cancellationToken)
        {
            //Validate query?

            return await repository.BankAccountWithCustomerExists(request.CustomerId);
        }

        public Task<BankAccount> Handle(GetBankAccount request, CancellationToken cancellationToken)
        {
            //Validate query?

            var bankAccount = repository.Get(request.Id);

            return Task.FromResult(bankAccount);
        }

        public Task<decimal> Handle(GetBankAccountBalance request, CancellationToken cancellationToken)
        {
            //Validate query?

            var balance = repository.GetBankAccountBalance(request.BankAccountId);

            return Task.FromResult(balance);
        }

        public async Task<IReadOnlyList<BankTransaction>> Handle(GetBankTransactionList request, CancellationToken cancellationToken)
        {
            //Validate query?

            return await repository.BankTransactionList(request.BankAccountId);
        }
    }
}
