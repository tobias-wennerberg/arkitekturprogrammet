using ITH.ArchProg.T3.Banking.EventProcessor.Projections;
using ITH.ArchProg.T3.Banking.Infrastructure.EventSourcing;
using ITH.ArchProg.T3.Banking.Infrastructure.Repositories;
using System.ServiceProcess;

namespace ITH.ArchProg.T3.Banking.EventProcessor
{
    public partial class EventProcessorService : ServiceBase
    {
        private readonly IEventStoreManager eventStoreManager;
        private readonly IBankAccountRepository bankAccountRepository;

        public EventProcessorService(IEventStoreManager eventStoreManager, IBankAccountRepository bankAccountRepository)
        {
            InitializeComponent();
            this.eventStoreManager = eventStoreManager;
            this.bankAccountRepository = bankAccountRepository;
        }

        protected override void OnStart(string[] args)  
        {
            //Instantiate all projections, reflection?
            var bankAccountProjection = new BankAccountProjection(bankAccountRepository);

            eventStoreManager.StartProjection(bankAccountProjection);
        }

        protected override void OnStop()
        {
            eventStoreManager.StopProjections();
        }

        //public void StartConsole(string[] args)
        //{
        //    OnStart(args);
        //}

        //public void StopConsole()
        //{
        //    OnStop();
        //}

    }
}
