using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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

        static List<NodeVM> ConnectedNodes = new List<NodeVM>();


        static async Task Main(string[] args)
        {
            var _url = "http://192.168.1.7:5000/Node/Register";




            var _myEndpoint = await Mediator.Send(new GenerateSelfNodeEndpointCommand());
            var _sendSelf = new SendSelfCommand(_myEndpoint, _url);

            List<NodeVM> _neighbors = await Mediator.Send(_sendSelf);

            _neighbors = _neighbors.Take(1)
                .ToList();




            foreach (var _node in _neighbors)
            {
                IPEndPoint _nodeEndpoint = new IPEndPoint(IPAddress.Parse(_node.IPAddress), _node.Port);

                TryConnectNodeCommand _connectCmd = new TryConnectNodeCommand(_nodeEndpoint);

                TcpClient _client = await Mediator.Send(_connectCmd);

                if (_client.Connected)
                {
                    NodeVM _connectedNode = new NodeVM
                    {
                        Connection = _client
                    };

                    ConnectedNodes.Add(_connectedNode);
                }
            }



            Action<TcpClient> _whenConnected = (TcpClient theTcpClientConnected) =>
            {
                NodeVM _connectedNode = new NodeVM
                {
                    Connection = theTcpClientConnected
                };

                ConnectedNodes.Add(_connectedNode);
            };






            var _node1 = ConnectedNodes.First();

            var _stream = _node1.Connection.GetStream();

            string _message2 = "hey2";
            byte[] _dataToSend2 = Encoding.ASCII.GetBytes(_message2);
            _stream.Write(_dataToSend2, 0, _dataToSend2.Length);


            ListenForConnectionCommand _listenforcon = new ListenForConnectionCommand(_myEndpoint, _whenConnected);
            await Mediator.Send(_listenforcon);






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