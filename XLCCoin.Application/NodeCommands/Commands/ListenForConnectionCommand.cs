using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;
using XLCCoin.Application.NodeCommands.Queries;

namespace XLCCoin.Application.NodeCommands.Commands
{
    public class ListenForConnectionCommand : IRequest<IPEndPoint>
    {
        private readonly IPEndPoint myEndpoint;
        private readonly Action<NodeVM> whenConnected;

        public ListenForConnectionCommand(IPEndPoint myEndpoint, Action<NodeVM> whenConnected)
        {
            this.myEndpoint = myEndpoint;
            this.whenConnected = whenConnected;
        }

        public class ListenForConnectionCommandHandler : 
            BaseRequestHandler, 
            IRequestHandler<ListenForConnectionCommand, IPEndPoint>
        {
            public ListenForConnectionCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {
                
            }

            public async Task<IPEndPoint> Handle(ListenForConnectionCommand request, CancellationToken cancellationToken)
            {
                IPEndPoint _retVal = request.myEndpoint;

                TcpListener server = new TcpListener(_retVal.Address, _retVal.Port);

                server.Start();

                try
                {
                    Thread _listenForConnectionThread = new Thread(() =>
                    {
                        while (true)
                        {
                            TcpClient client = server.AcceptTcpClient();

                            request.whenConnected(new NodeVM
                            {
                                Client = client
                            });
                        }
                    });

                    _listenForConnectionThread.Start();
                }
                catch (SocketException e)
                {
                    server.Stop();
                }

                return _retVal;
            }
        }
    }
}