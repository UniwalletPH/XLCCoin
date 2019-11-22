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

namespace XLCCoin.Application.NodeCommands.Commands
{

    public class TestCommand : IRequest<string>
    {
        public string Ip { get; set; }
        public string Port { get; set; }

        public class TestCommandHandler : BaseRequestHandler, IRequestHandler<TestCommand, string>
        {
            public TestCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {


            }

            public async Task<string> Handle (TestCommand request, CancellationToken cancellationToken)
            {

           

                return $"Hey! {request.Ip}";

            }
        }
    }
}
