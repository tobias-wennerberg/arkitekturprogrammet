using Autofac;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Core
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetType().Assembly)
                .FindConstructorsWith(x => x.GetConstructors(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance))
                .AsClosedTypesOf(typeof(IValidator<>)).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(GetType().Assembly)
                .FindConstructorsWith(x => x.GetConstructors(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance))
                .AsClosedTypesOf(typeof(IRequestHandler<>)).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(GetType().Assembly)
                .FindConstructorsWith(x => x.GetConstructors(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance))
                .AsClosedTypesOf(typeof(IRequestHandler<,>)).InstancePerRequest();

            base.Load(builder);
        }
    }
}
