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

        public GetNeighborsCommand(string url) {

            this.neighborsUrl = url;
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
                        if (_listOfNodes == null)
                        {
                            return new List<NodeVM>();
                        }

                        return _listOfNodes;
                    }

                }

            }
        }

    }
}
