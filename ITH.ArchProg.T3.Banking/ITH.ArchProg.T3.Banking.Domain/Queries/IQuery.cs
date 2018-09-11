using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Domain.Queries
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
