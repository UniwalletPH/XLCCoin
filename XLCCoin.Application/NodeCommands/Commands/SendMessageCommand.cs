using MediatR;
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
    public class SendMessageCommand : IRequest<bool>
    {
        private readonly string msg;
        private readonly NodeVM destination;
        public SendMessageCommand(NodeVM destination, string msg)
        {
            this.msg = msg;
            this.destination = destination;
        }
        public class SendMessageCommandHandler : BaseRequestHandler, IRequestHandler<SendMessageCommand, bool>
        {
            public SendMessageCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {

            }

            public async Task<bool> Handle(SendMessageCommand request, CancellationToken cancellationToken)
            {
                var _client = request.destination.Client;
                var _myStream = _client.GetStream();


                var _dataToSend = Encoding.ASCII.GetBytes(request.msg);

                _myStream.Write(_dataToSend, 0, _dataToSend.Length);


                return true;

            }
        }
    }
}
