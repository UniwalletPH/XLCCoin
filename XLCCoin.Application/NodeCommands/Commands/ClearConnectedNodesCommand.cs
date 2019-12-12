using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;

namespace XLCCoin.Application.NodeCommands.Commands
{
    public class ClearConnectedNodesCommand : IRequest
    {
        public ClearConnectedNodesCommand()
        {

        }

        public class ClearConnectedNodesCommandHandler : BaseRequestHandler, IRequestHandler<ClearConnectedNodesCommand>
        {
            public ClearConnectedNodesCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {

            }

            public async Task<Unit> Handle(ClearConnectedNodesCommand request, CancellationToken cancellationToken)
            {
                var _data = await dbContext.Nodes.ToListAsync();

                dbContext.Nodes.RemoveRange(_data);

                await dbContext.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
