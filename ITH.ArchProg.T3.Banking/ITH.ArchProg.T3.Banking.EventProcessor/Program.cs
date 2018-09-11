using Autofac;
using ITH.ArchProg.T3.Banking.Infrastructure;
using ITH.ArchProg.T3.Banking.Infrastructure.EventSourcing;
using ITH.ArchProg.T3.Banking.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.EventProcessor
{
    static class Program
    {
        //static void Main(string[] args)
        //{
        //    var builder = new ContainerBuilder();

        //    builder.RegisterModule<InfrastructureModule>();

        //    var container = builder.Build();
        //    var eventStoreManager = container.Resolve<IEventStoreManager>();
        //    var bankAccountRepository = container.Resolve<IBankAccountRepository>();

        //    var service = new EventProcessorService(eventStoreManager, bankAccountRepository);

        //    service.StartConsole(args);
        //    Console.WriteLine("Press any key to stop program");
        //    Console.ReadKey();
        //    service.StopConsole();
        //}

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var container = SetupIoCContainer();
            var eventStoreManager = container.Resolve<IEventStoreManager>();
            var bankAccountRepository = container.Resolve<IBankAccountRepository>();

            ServiceBase[] ServicesToRun;


            ServicesToRun = new ServiceBase[]
            {
                new EventProcessorService(eventStoreManager, bankAccountRepository)
            };
            ServiceBase.Run(ServicesToRun);
        }

        private static IContainer SetupIoCContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<InfrastructureModule>();
            return builder.Build();
        }
    }
}
