using APeer.Application.Common.Base;
using APeer.Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;

namespace XLCCoin.Application.NodeCommands.Commands
{

    public class GenerateSelfNodeEndpointCommand : IRequest<IPEndPoint>
    {


        public class GenerateSelfNodeEndpointCommandHandler : BaseRequestHandler, 
            IRequestHandler<GenerateSelfNodeEndpointCommand, IPEndPoint>
        {
            public GenerateSelfNodeEndpointCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {

            }

            public async Task<IPEndPoint> Handle(GenerateSelfNodeEndpointCommand request, CancellationToken cancellationToken)
            {
                IPAddress _myIP = SharedFunction.GetLocalIPAddress();
                int _myPort = SharedFunction.GetRandomPort();

                return new IPEndPoint(_myIP, _myPort);
            }
        }
    }
}
