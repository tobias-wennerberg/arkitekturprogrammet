using System;

namespace ITH.ArchProg.T3.Banking.Domain.Entities.BankAccountAggregate
{
    public class BankTransaction : Entity
    {
        internal BankTransaction(string id, string bankAccountId, decimal amount, string sender, string reciever, DateTime timestamp)
        {
            Id = id;
            BankAccountId = bankAccountId;
            Amount = amount;
            Sender = sender;
            Reciever = reciever;
            Timestamp = timestamp;
        }

        public string BankAccountId { get; private set; }

        public decimal Amount { get; private set; }

        public string Sender { get; private set; }

        public string Reciever { get; private set; }

        public DateTime Timestamp { get; private set; }
    }
}
