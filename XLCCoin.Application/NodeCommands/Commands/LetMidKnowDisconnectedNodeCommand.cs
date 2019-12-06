using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;
using XLCCoin.Application.NodeCommands.Queries;
using XLCCoin.Domain.Entities;

namespace XLCCoin.Application.NodeCommands.Commands
{
    public class LetMidKnowDisconnectedNodeCommand : IRequest<Guid>
    {
        private readonly NodeVM node;
        private readonly string url;

        public LetMidKnowDisconnectedNodeCommand(string url, NodeVM node)
        {
            this.url = url;
            this.node = node; 
        }

        public class LetMidKnowDisconnectedNodeCommandHandler : BaseRequestHandler, IRequestHandler<LetMidKnowDisconnectedNodeCommand,Guid>
        {

            public LetMidKnowDisconnectedNodeCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            { 
            
            }
       
            public async Task<Guid> Handle(LetMidKnowDisconnectedNodeCommand request, CancellationToken cancellationToken)
            {
                Node _disconnectedNode = new Node
                {
                    ID = request.node.ID,
                    IPAddress = request.node.IPAddress.ToString(),
                    Port = request.node.Port
                };

                
                var _json = JsonConvert.SerializeObject(_disconnectedNode);

                using (var _client = new HttpClient())
                using (var _data = new StringContent(_json, Encoding.UTF8, "application/json"))
                {
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var _response = await _client.PostAsync(request.url, _data);

                    return _disconnectedNode.ID;
                }
            }
        }
    }
}
