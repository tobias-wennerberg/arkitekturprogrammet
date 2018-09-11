using Autofac;
using Autofac.Integration.WebApi;
using ITH.ArchProg.T3.Banking.Core;
using ITH.ArchProg.T3.Banking.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace ITH.ArchProg.T3.Banking.API
{
    public static class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<Mediator>().As<IMediator>().InstancePerRequest();

            // request handlers
            builder.Register<SingleInstanceFactory>(ctx =>

            {
                var c = ctx.Resolve<IComponentContext>();
                return t => { return c.TryResolve(t, out object o) ? o : null; };
            }).InstancePerRequest();

            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<InfrastructureModule>();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}