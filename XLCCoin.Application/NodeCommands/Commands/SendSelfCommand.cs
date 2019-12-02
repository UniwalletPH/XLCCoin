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
    public class SendSelfCommand : IRequest<List<NodeVM>>
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
            IRequestHandler<SendSelfCommand, List<NodeVM>>
        {

            public SendSelfCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {

            }

            public async Task<List<NodeVM>> Handle(SendSelfCommand request, CancellationToken cancellationToken)
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

                    using (var _content = _response.Content)
                    {
                        string _result = await _content.ReadAsStringAsync();

                        var _listOfNodes = JsonConvert.DeserializeObject<List<NodeVM>>(_result);

                        return _listOfNodes
                            .Where(a => a.IPAddress != request.myEndpoint.ToString()
                                                        && a.Port != request.myEndpoint.Port)
                            .ToList();
                    }
                }
            }
        }
    }
}