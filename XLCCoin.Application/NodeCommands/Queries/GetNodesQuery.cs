using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;
using XLCCoin.Domain.Entities;

namespace XLCCoin.Application.NodeCommands.Queries
{

    public class GetNodesQuery : IRequest<IEnumerable<NodeVM>>
    {


        public class GetNodesQueryHandler : BaseRequestHandler, IRequestHandler<GetNodesQuery, IEnumerable<NodeVM>>
        {
            public GetNodesQueryHandler(IXLCDbContext dbContext) : base(dbContext)
            {
            }

            public async Task<IEnumerable<NodeVM>> Handle(GetNodesQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.Nodes
                    .Select(a => new NodeVM
                    {
                        ID = a.ID,
                        IPAddress = a.IPAddress,
                        Port = a.Port,
                        IsBehindNAT = a.IsBehindNAT
                    }).ToListAsync();
            }
        }
    }
}