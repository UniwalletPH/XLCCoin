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

namespace XLCCoin.Application.NodeCommands.Queries
{

    public class TestGetNodesQuery : IRequest<IEnumerable<Node>>
    {


        public class TestGetNodesQueryHandler : BaseRequestHandler, IRequestHandler<TestGetNodesQuery, IEnumerable<Node>>
        {
            public TestGetNodesQueryHandler(IXLCDbContext dbContext) : base(dbContext)
            {
            }

            public async Task<IEnumerable<Node>> Handle(TestGetNodesQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.Nodes.ToListAsync();
            }
        }
    }
}
