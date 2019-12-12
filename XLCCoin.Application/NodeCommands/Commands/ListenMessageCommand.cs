using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly NodeVM source;
        public ListenMessageCommand(NodeVM source, Action<string> messageY)
        {
            this.messageX = messageY;
            this.source = source;
        }

        public class ListenMessageCommandHandler : BaseRequestHandler, IRequestHandler<ListenMessageCommand>
        {
            private readonly IMediator mediator;
            public ListenMessageCommandHandler(IMediator mediator, IXLCDbContext dbContext) : base(dbContext)
            {
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(ListenMessageCommand request, CancellationToken cancellationToken)
            {
                var client = request.source.Client;
                var _myStream = client.GetStream();

                new Thread(async ()=> {
                    while (client.Connected)
                    {
                        try
                        {
                            byte[] _data = new byte[10];
                            int i;

                            StringBuilder _sb = new StringBuilder();

                            do
                            {
                                i = _myStream.Read(_data, 0, _data.Length);
                                string _message = Encoding.ASCII.GetString(_data.Take(i).ToArray());
                                _sb.Append(_message);
                            }


                            while (_myStream.DataAvailable);

                            var _receiveRequest = JsonConvert.DeserializeObject<XLCmdVM>(_sb.ToString());
                            
                            switch (_receiveRequest.CommandName)
                            {
                                case "FindTip":

                                    var _findTipResponse = await mediator.Send(new FindTipCommand());
                                    _receiveRequest.CommandName = "FindTipResponse";
                                    _receiveRequest.Response = _findTipResponse;
                                    var _responseRequest = JsonConvert.SerializeObject(_receiveRequest);

                                    await mediator.Send(new SendMessageCommand(request.source, _responseRequest));

                                    break;


                                case "FindTipResponse":
                                    var _listOfTranSite = _receiveRequest.Response as List<TranSiteVM>;
                                    request.source.TIPS = _listOfTranSite;

                                 
                                    break;

                                default:
                                    break;
                            }

                            request.messageX(_sb.ToString());
                        }
                        catch (Exception c)
                        {
                            Console.WriteLine(c);
                        }
                    }
                }) .Start();
                
                return Unit.Value;
            }
        }
    }
}
