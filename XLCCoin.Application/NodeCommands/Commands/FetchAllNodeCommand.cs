
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;
using XLCCoin.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace XLCCoin.Application.NodeCommands.Commands
{

    public class FetchAllNodeCommand : IRequest<List<Node>>
    {
        private readonly IMediator mediator;
        private Node node;
        public FetchAllNodeCommand(Node node)
        {
            this.node = node;
        }
        public string Name { get; set; }

        public class FetchAllNodeCommandHandler : BaseRequestHandler, IRequestHandler<FetchAllNodeCommand, List<Node>>
        {
            public FetchAllNodeCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {
            }

            public async Task<List<Node>> Handle(FetchAllNodeCommand request, CancellationToken cancellationToken)
            {


                var _temp = dbContext.Nodes;
                var results = await _temp.ToListAsync();
                return results;
            }



        }
    }
}