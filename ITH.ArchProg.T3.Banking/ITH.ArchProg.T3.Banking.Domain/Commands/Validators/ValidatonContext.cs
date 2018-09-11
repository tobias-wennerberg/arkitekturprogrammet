using ITH.ArchProg.T3.Banking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Domain.Commands.Validators
{
    public class ValidatonContext<TCommand, TEntity>
        where TCommand : ICommand
        where TEntity : Entity
    {
        internal ValidatonContext(TCommand command, TEntity entity)
        {
            Command = command;
            Entity = entity;
        }
        public TCommand Command { get; private set; }

        public TEntity Entity { get; private set; }
    }
}
