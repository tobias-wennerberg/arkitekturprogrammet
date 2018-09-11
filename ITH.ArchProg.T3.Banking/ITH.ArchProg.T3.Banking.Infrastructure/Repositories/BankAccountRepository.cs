using ITH.ArchProg.T3.Banking.Domain.ReadModels;
using ITH.ArchProg.T3.Banking.Domain.Repositories;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Infrastructure.Repositories
{
    public class BankAccountRepository : IBankAccountQueryRepository, IBankAccountRepository
    {
        private readonly IDocumentStore documentStore;

        public BankAccountRepository()
        {
            var documentStore = new DocumentStore
            {
                Urls = new[] { "http://127.0.0.1:8080" },
                Database = "BankingDB",
                Conventions = { }
            };
            documentStore.Initialize();
        }

        internal BankAccountRepository(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        public BankAccount Get(string id)
        {
            BankAccount bankAccount = null;

            using (var session = documentStore.OpenSession())
            {
                bankAccount = session.Load<BankAccount>(id);
            }
            return bankAccount;
        }
        public IReadOnlyList<BankAccount> List()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> BankAccountWithCustomerExists(string customerId)
        {
            var customerExists = false;

            using (var session = documentStore.OpenAsyncSession())
            {
                customerExists = await session.Query<BankAccount>().AnyAsync(ba => ba.CustomerId == customerId);
            }
            return customerExists;
        }

        public bool Create(BankAccount bankAccount)
        {
            using (var session = documentStore.OpenSession())
            {
                session.Store(bankAccount);
                session.SaveChanges();
            }
            return true;
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public bool Update(BankAccount bankAccount)
        {
            using (var session = documentStore.OpenSession())
            {
                session.Store(bankAccount);
                session.SaveChanges();
            }
            return true;
        }

        public bool Create(BankTransaction transaction)
        {
            using (var session = documentStore.OpenSession())
            {
                session.Store(transaction);
                session.SaveChanges();
            }
            return true;
        }

        public bool UpdateEventNumber(long eventNumber)
        {
            using (var session = documentStore.OpenSession())
            {
                var obj = new
                {
                    Id = typeof(BankAccount).Name,
                    EventNumber = eventNumber
                };
                session.Store(obj);
                session.SaveChanges();
            }
            return true;
        }

        public long? GetEventNumber()
        {
            long? eventNumber;

            using (var session = documentStore.OpenSession())
            {
                var obj = session.Load<dynamic>(typeof(BankAccount).Name);
                eventNumber = obj != null ? obj.EventNumber : null;
            }
            return eventNumber;
        }

        public decimal GetBankAccountBalance(string id)
        {
            BankAccount bankAccount = null;

            using (var session = documentStore.OpenSession())
            {
                bankAccount = session.Load<BankAccount>(id);
            }
            return bankAccount.Amount;
        }

        public async Task<IReadOnlyList<BankTransaction>> BankTransactionList(string id)
        {
            List<BankTransaction> transactionList = null;

            using (var session = documentStore.OpenAsyncSession())
            {
                transactionList = await session.Query<BankTransaction>().Where(t => t.BankAccountId == id, true).ToListAsync();
            }
            return transactionList;
        }

    }
}
