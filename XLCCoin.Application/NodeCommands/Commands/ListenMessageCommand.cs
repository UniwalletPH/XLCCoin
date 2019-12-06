﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;
using XLCCoin.Application.NodeCommands.Queries;

namespace XLCCoin.Application.NodeCommands.Commands
{
    public class ListenMessageCommand : IRequest
    {
        private readonly Action<string> messageX;
        private readonly NodeVM connectedNodes;
        public ListenMessageCommand(Action<string> messageY, NodeVM connectedNodes)
        {
            this.messageX = messageY;
            this.connectedNodes = connectedNodes;
        }

        public class ListenMessageCommandHandler : BaseRequestHandler, IRequestHandler<ListenMessageCommand>
        {
            public ListenMessageCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {
            }

            public async Task<Unit> Handle(ListenMessageCommand request, CancellationToken cancellationToken)
            {
                var client = request.connectedNodes.Client;
                var _myStream = client.GetStream();


                while (client.Connected)
                {
                    try
                    {
                        byte[] _data = new byte[10];
                        int i;

                        StringBuilder _sb = new StringBuilder();
                        while ((i = _myStream.Read(_data, 0, _data.Length)) != 0
                                 && _myStream.DataAvailable)
                        {
                            string _message = Encoding.ASCII.GetString(_data);

                            _sb.Append(_message);
                        }

                        request.messageX(_sb.ToString());
                    }
                    catch (Exception c)
                    {
                        Console.WriteLine(c);
                    }
                }
                return Unit.Value;
            }
        }
    }
}