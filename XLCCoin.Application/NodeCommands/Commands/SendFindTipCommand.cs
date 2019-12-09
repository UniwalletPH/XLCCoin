using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;
using XLCCoin.Application.NodeCommands.Queries;

namespace XLCCoin.Application.NodeCommands.Commands
{
    public class SendFindTipCommand : IRequest<bool>
    {

        private readonly NodeVM destination;

        public SendFindTipCommand(NodeVM destination) {

            this.destination = destination;
        }

        public class SendFindTipCommandHandler : BaseRequestHandler, IRequestHandler<SendFindTipCommand, bool>
        {
            private readonly IMediator mediator;
            public SendFindTipCommandHandler(IMediator mediator, IXLCDbContext dbContext) : base(dbContext)
            {

                this.mediator = mediator;
            }

            public async Task<bool> Handle(SendFindTipCommand request, CancellationToken cancellationToken)
            {
                XLCmdVM _findTipCommand = new XLCmdVM { 
                
                    CommandName = "FindTip"  
                };

                var _json = JsonConvert.SerializeObject(_findTipCommand);

                await mediator.Send(new SendMessageCommand(request.destination, _json));

                return true;
            }
        }

    }
}
