using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;
using XLCCoin.Application.NodeCommands.Queries;
using XLCCoin.Domain.Entities;

namespace XLCCoin.Application.NodeCommands.Commands
{
    public class SendSelfCommand : IRequest<string>
    {
        private readonly string serverUrl;
        private readonly IPEndPoint myEndpoint;

        public SendSelfCommand(IPEndPoint myEndpoint, string serverUrl)
        {
            this.serverUrl = serverUrl;
            this.myEndpoint = myEndpoint;
        }

        public class SendSelfCommandHandler :
            BaseRequestHandler,
            IRequestHandler<SendSelfCommand, string>
        {

            public SendSelfCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {

            }

            public async Task<string> Handle(SendSelfCommand request, CancellationToken cancellationToken)
            {
                Node _myNode = new Node
                {
                    ID = Guid.NewGuid(),
                    IPAddress = request.myEndpoint.Address.ToString(),
                    Port = request.myEndpoint.Port
                };

                var _json = JsonConvert.SerializeObject(_myNode);

                using (var _client = new HttpClient())
                using (var _data = new StringContent(_json, Encoding.UTF8, "application/json"))
                {
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var _response = await _client.PostAsync(request.serverUrl, _data);

                    return "Success";
                }
            }
        }
    }
}
