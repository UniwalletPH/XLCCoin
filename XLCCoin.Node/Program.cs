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



        static async Task Main(string[] args)
        {
            NodeVM _testNode = new NodeVM();

            _testNode.SendMessage("The quick brown fox jumps over the lazy dog");
            return;









            var _url = "http://192.168.1.7:5000/Node/Register";

            List<NodeVM> ConnectedNodes = new List<NodeVM>();


            #region Step 1
            var _myEndpoint = await Mediator.Send(new GenerateSelfNodeEndpointCommand());
            #endregion

            #region Step 2
            Action<TcpClient> _whenConnected = (TcpClient theTcpClientConnected) =>
                {
                    NodeVM _connectedNode = new NodeVM
                    {
                        Client = theTcpClientConnected
                    };

                    ConnectedNodes.Add(_connectedNode);
                };
            ListenForConnectionCommand _listenforcon = new ListenForConnectionCommand(_myEndpoint, _whenConnected);
            await Mediator.Send(_listenforcon);
            #endregion

            #region Step 3
            var _sendSelf = new SendSelfCommand(_myEndpoint, _url);

            List<NodeVM> _neighbors = await Mediator.Send(_sendSelf);

            _neighbors = _neighbors.Take(1)
                .ToList();
            #endregion

            #region Step 4
            // Get Neighbor Command 
            #endregion

            #region Step 5
            foreach (var _node in _neighbors)
            {
                IPEndPoint _nodeEndpoint = new IPEndPoint(IPAddress.Parse(_node.IPAddress), _node.Port);

                TryConnectNodeCommand _connectCmd = new TryConnectNodeCommand(_nodeEndpoint);

                TcpClient _client = await Mediator.Send(_connectCmd);

                if (_client.Connected)
                {
                    NodeVM _connectedNode = new NodeVM
                    {
                        Client = _client
                    };

                    ConnectedNodes.Add(_connectedNode);
                }
            }
            #endregion

            #region Step 6
            Action<string> messageZ = (string msg) =>
                {
                    Console.WriteLine("The message is: " + msg);
                };

            var FirstNode = ConnectedNodes.First();

            await Mediator.Send(new ListenMessageCommand(messageZ, FirstNode));

            Console.ReadLine();
            #endregion


            #region Step 7
            Console.Write("Please send a message: ");
            string _msgToSend = Console.ReadLine();

            await Mediator.Send(new SendMessageCommand(_msgToSend, FirstNode)); 
            #endregion

        }
    }
}