using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;
using XLCCoin.Domain.Entities;

namespace XLCCoin.Application.NodeCommands.Commands
{
    public class SendSelfCommand : IRequest<List<Node>>
    {
        private readonly IMediator mediator;

        public SendSelfCommand()
        {
        }

        public SendSelfCommand(IMediator mediator)
        {

            this.mediator = mediator;

        }


        public class SendSelfCommandHandler : BaseRequestHandler, IRequestHandler<SendSelfCommand, List<Node>>
        {



            public SendSelfCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {

            }

            public string GetLocalIPAddress()
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }

            public async Task<List<Node>> Handle(SendSelfCommand request, CancellationToken cancellationToken)
            {



                XLCCoin.Domain.Entities.Node MyNode = new Domain.Entities.Node();


                MyNode.ID = Guid.NewGuid();
                MyNode.IPAddress = GetLocalIPAddress();
                MyNode.Port = 123;


                var json = JsonConvert.SerializeObject(MyNode);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "http://192.168.2.12:5000/AvailableNodes";
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync(url, data);

                string result = await response.Content.ReadAsStringAsync();


                Console.WriteLine(result);

                var ListOfNodes = JsonConvert.DeserializeObject<List<Node>>(result);


                return ListOfNodes;
            }

        }

    }


 }

