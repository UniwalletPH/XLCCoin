using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;
using XLCCoin.Application.NodeCommands.Queries;
using XLCCoin.Domain.Entities;

namespace XLCCoin.Application.NodeCommands.Commands
{
    public class GetNeighborsCommand : IRequest<List<NodeVM>>
    {
       
        private readonly string neighborsUrl;
        private readonly IPEndPoint myEndpoint;

        public GetNeighborsCommand(IPEndPoint myEndpoint, string url) {

            this.neighborsUrl = url;
            this.myEndpoint = myEndpoint;
        }

        public class GetNeighborsCommandHandler : BaseRequestHandler, IRequestHandler<GetNeighborsCommand, List<NodeVM>>
        {

            public GetNeighborsCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {

            }

            public async Task<List<NodeVM>> Handle(GetNeighborsCommand request, CancellationToken cancellationToken)
            {

                using (var _client = new HttpClient())
                {
                    var _response = await _client.GetAsync(request.neighborsUrl);

                    using (var _content = _response.Content)
                    {
                        string _result = await _content.ReadAsStringAsync();

                        var _listOfNodes = JsonConvert.DeserializeObject<List<NodeVM>>(_result);

                        return _listOfNodes.Where(a => a.IPAddress != request.myEndpoint.ToString()
                                                        && a.Port != request.myEndpoint.Port)
                            .ToList();


                    }

                }

            }
        }

    }
}
