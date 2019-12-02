using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Application.NodeCommands.Commands;
using XLCCoin.Application.NodeCommands.Queries;

namespace XLCCoin.Node
{
    class Program
    {
        static IMediator Mediator
        {
            get
            {
                return ServiceRegistration.ServiceProvider.GetService<IMediator>();
            }
        }

        static async Task Main(string[] args)
        {
            var _url = "http://192.168.1.7:5000/Node/Register";


            var _myEndpoint = await Mediator.Send(new GenerateSelfNodeEndpointCommand());
            var _sendSelf = new SendSelfCommand(_myEndpoint, _url);

            List<NodeVM> _neighbors = await Mediator.Send(_sendSelf);

            _neighbors = _neighbors.Take(1)
                .ToList();


            ListenForConnectionCommand _listenforcon = new ListenForConnectionCommand(_myEndpoint);
            await Mediator.Send(_listenforcon);

            foreach (var _node in _neighbors)
            {
                IPEndPoint _nodeEndpoint = new IPEndPoint(IPAddress.Parse(_node.IPAddress), _node.Port);

                TryConnectNodeCommand _connectCmd = new TryConnectNodeCommand(_nodeEndpoint);

                _node.Connection = await Mediator.Send(_connectCmd);

                if (_node.Connection.Connected)
                {

                }
            }


            // 1: 192.168.1.1
            // 2: 192.168.1.2

            //var _node = _neighbors.Where(a => a.IPAddress == "192.168.1.2")
            //    .SingleOrDefault();

            //if (_node != null)
            //{
            //    _node.SendMessage("Hey!");
            //}

            Console.ReadLine();
        }
    }
}