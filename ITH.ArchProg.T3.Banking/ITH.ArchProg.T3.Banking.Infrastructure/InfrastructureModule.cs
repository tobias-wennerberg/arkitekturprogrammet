using Autofac;
using ITH.ArchProg.T3.Banking.Domain.Events;
using ITH.ArchProg.T3.Banking.Domain.Repositories;
using ITH.ArchProg.T3.Banking.Infrastructure.EventSourcing;
using ITH.ArchProg.T3.Banking.Infrastructure.Repositories;
using Raven.Client.Documents;

namespace ITH.ArchProg.T3.Banking.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register the document store as single instance,
            // initializing it on first use.
            builder.Register(x =>
            {
                var store = new DocumentStore
                {
                    Urls = new[] { "http://127.0.0.1:8080" },
                    Database = "BankingDB",
                    Conventions = { }
                };
                store.Initialize();
                return store;
            })
                 .As<IDocumentStore>()
                 .SingleInstance()
                 .OnRelease(x => x.Dispose());

            builder.RegisterType<EventStoreManager>().FindConstructorsWith(x => x.GetConstructors(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)).As<IEventRepository>().InstancePerRequest();
            builder.RegisterType<EventStoreManager>().FindConstructorsWith(x => x.GetConstructors(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)).As<IEventStoreManager>().InstancePerLifetimeScope();
            builder.RegisterType<BankAccountRepository>().FindConstructorsWith(x => x.GetConstructors(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)).As<IBankAccountRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BankAccountRepository>().FindConstructorsWith(x => x.GetConstructors(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)).As<IBankAccountQueryRepository>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
