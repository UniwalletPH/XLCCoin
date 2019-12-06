using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;
using XLCCoin.Application.NodeCommands.Queries;
using XLCCoin.Domain.Entities;

namespace XLCCoin.Application.NodeCommands.Commands
{
    public class SaveNodeCommand : IRequest<Guid>
    {
        private NodeVM node;
        
        public SaveNodeCommand(NodeVM node)
        {
            this.node = node;
        }

        public class SaveNodeCommandHandler : 
            BaseRequestHandler, 
            IRequestHandler<SaveNodeCommand, Guid>
        {
            public SaveNodeCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {

            }

            public async Task<Guid> Handle(SaveNodeCommand request, CancellationToken cancellationToken)
            {
                Node _node = new Node
                {
                    ID = request.node.ID,
                    IPAddress = request.node.IPAddress,
                    Port = request.node.Port,
                    Geolocation = request.node.Geolocation,
                    IsBehindNAT = request.node.IsBehindNAT,
                };

                dbContext.Nodes.Add(_node);
                await dbContext.SaveChangesAsync();

                return _node.ID;

            }
        }


    }
}
