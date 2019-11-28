using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;
using XLCCoin.Domain.Entities;

namespace XLCCoin.Application.NodeCommands.Commands
{

    public class TestCommand : IRequest<string>
    {
        public  Guid Identity { get; set; }
        public int Allowed { get; set; }
        public Node Node { get; set; }


        public class TestCommandHandler : BaseRequestHandler, IRequestHandler<TestCommand, string>
        {
            public TestCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {


            }

            public async Task<string> Handle (TestCommand request, CancellationToken cancellationToken)
            {

             

                return ":)";

            }
        }
    }
}
