using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;
using XLCCoin.Domain.Entities;

namespace XLCCoin.Application.NodeCommands.Commands
{
    public class SaveNodesCommand : IRequest<Guid>
    {


        private Node node;
        private IMediator mediator;

        public SaveNodesCommand(Node node)
        {

            this.node = node;
        
        
        }

        public class SaveNodesCommandHandler : BaseRequestHandler, IRequestHandler<SaveNodesCommand, Guid>
        {


            public SaveNodesCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {


            }

            public async Task<Guid> Handle(SaveNodesCommand request, CancellationToken cancellationToken)
            {
                Node node = new Node
                {
                    ID = request.node.ID,
                    IPAddress = request.node.IPAddress,
                    Port = request.node.Port,
                    Geolocation = request.node.Geolocation,
                    IsBehindNAT = request.node.IsBehindNAT,
                    Device = request.node.Device,
                    Wallets = request.node.Wallets

                };

                dbContext.Nodes.Add(node);
                await dbContext.SaveChangesAsync();

                return node.ID;

            }
        }


    }
}
