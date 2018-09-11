using ITH.ArchProg.T3.Banking.Domain.Commands;
using ITH.ArchProg.T3.Banking.Domain.Queries;
using MediatR;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ITH.ArchProg.T3.Banking.API.Controllers
{
    public class BankAccountsController : ApiController
    {
        private readonly IMediator mediator;

        public BankAccountsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/BankAccounts/5
        public async Task<HttpResponseMessage> Get([FromUri]string id)
        {
            var query = new GetBankAccount
            {
                Id = id
            };
            var bankAccount = await mediator.Send(query);
            return Request.CreateResponse(HttpStatusCode.OK, bankAccount);
        }

        // POST: api/BankAccounts
        public async Task<HttpResponseMessage> Post([FromBody]CreateBankAccount command)
        {
            var customerExists = await CustomerExists(command.CustomerId);

            if (customerExists)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Customer already exists");
            }
            await mediator.Send(command);
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        [Route("api/v1/BankAccounts/{id}/Deposit")]
        public async Task<HttpResponseMessage> PutDeposit([FromUri]string id, [FromUri]decimal amount, [FromUri]string sender)
        {
            var command = new DepositAmount
            {
                BankAccountId = id,
                Amount = amount,
                Sender = sender
            };
            await mediator.Send(command);
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        [Route("api/v1/BankAccounts/{id}/Withdraw")]
        public async Task<HttpResponseMessage> PutWithdraw([FromUri]string id, [FromUri]decimal amount, [FromUri]string reciever)
        {
            var command = new WithdrawAmount
            {
                BankAccountId = id,
                Amount = amount,
                Reciever = reciever
            };
            await mediator.Send(command);
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        [Route("api/v1/BankAccounts/{id}/Balance")]
        public async Task<HttpResponseMessage> GetBalance([FromUri]string id)
        {
            var command = new GetBankAccountBalance
            {
                BankAccountId = id
            };
            var balance = await mediator.Send(command);
            return Request.CreateResponse(HttpStatusCode.OK, balance);

        }

        [Route("api/v1/BankAccounts/{id}/Transactions")]
        public async Task<HttpResponseMessage> GetTransactions([FromUri]string id)
        {
            var command = new GetBankTransactionList
            {
                BankAccountId = id
            };
            var transactions = await mediator.Send(command);
            return Request.CreateResponse(HttpStatusCode.OK, transactions);

        }

        // DELETE: api/BankAccounts/5
        public async Task<HttpResponseMessage> Delete([FromUri]string id)
        {
            var query = new CloseBankAccount
            {
                Id = id
            };
            await mediator.Send(query);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        private async Task<bool> CustomerExists(string customerId)
        {
            var query = new BankAccountCustomerExists
            {
                CustomerId = customerId
            };
            return await mediator.Send(query);
        }
    }
}
