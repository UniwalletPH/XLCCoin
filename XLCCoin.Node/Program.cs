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
            
            var _url = "http://192.168.2.12:5000/Node/Register";
            var _url2 = "http://192.168.2.12:5000/Node/Neighbors";

            List<NodeVM> ConnectedNodes = new List<NodeVM>();


            #region Step 1
            var _myEndpoint = await Mediator.Send(new GenerateSelfNodeEndpointCommand());
            Console.WriteLine("Generating self address...");
            #endregion

            #region Step 2 //ListenForConnection Command
            Action<TcpClient> _whenConnected = async (TcpClient theTcpClientConnected) =>
                {
                    NodeVM _connectedNode = new NodeVM
                    {
                        Client = theTcpClientConnected
                    };

                    ConnectedNodes.Add(_connectedNode);

                   await Mediator.Send(new ListenMessageCommand(_connectedNode, (string msg) =>
                    {
                        Console.WriteLine("The message is: " + msg);
                    }));
                };

            ListenForConnectionCommand _listenforcon = new ListenForConnectionCommand(_myEndpoint, _whenConnected);
            await Mediator.Send(_listenforcon);
            Console.WriteLine("Listening for connection...");
            #endregion

            #region Step 3 //SendSelf Command
            var _sendSelf = new SendSelfCommand(_myEndpoint, _url);

            await Mediator.Send(_sendSelf);

            Console.WriteLine("Sending self to MID...");
            #endregion

            #region Step 4 //GetNeighbors Command
            // Get Neighbor Command 
            var _getNeighbors = new GetNeighborsCommand(_myEndpoint, _url2);

            List<NodeVM> _neighbors = await Mediator.Send(_getNeighbors);
            Console.WriteLine("Fetching neighbors...");
            #endregion

            #region Step 5  //TryConnectNode Command
            List<TranSiteVM> _transites = new List<TranSiteVM>();

            foreach (var _node in _neighbors)
            {
                IPEndPoint _nodeEndpoint = new IPEndPoint(IPAddress.Parse(_node.IPAddress), _node.Port);

                TryConnectNodeCommand _connectCmd = new TryConnectNodeCommand(_nodeEndpoint);

                TcpClient _client = await Mediator.Send(_connectCmd);

                if (_client.Connected)
                {
                    NodeVM _connectedNode = new NodeVM
                    {
                        Client = _client,
                        Age = 9
                    };

                    bool hasAllTips = true;

                    foreach (var item in ConnectedNodes)
                    {
                        if (item.TIPS == null)
                        {
                            hasAllTips = false;
                        }
                    }

                    if (hasAllTips)
                    {
                        // Randommize All TIps
                    }

                    
                    ConnectedNodes.Add(_connectedNode);
                    await Mediator.Send(new ListenMessageCommand(_connectedNode,(string msg) =>
                    {
                        Console.WriteLine("The message is: " + msg);
                    }));
                }
            }
            Console.WriteLine("Connecting node...");
            #endregion


            Console.WriteLine("1 - List of all connected nodes");
            Console.WriteLine("2 - Send a message");
            Console.WriteLine("3 - Exit");
            
           start: 
            Console.Write("Please enter a command: ");
            string _cmdNumber = Console.ReadLine();

            switch (_cmdNumber)
            {
                case "1":

                    Console.WriteLine("Fetching neighbors...");

                    foreach (var item in ConnectedNodes)
                    {
                        Console.WriteLine(item.ID + item.IPAddress + item.Port);
                    }
                    Console.WriteLine("End of list");

                    goto start;

                case "2":

                    int neighborID = 0;
                    foreach (var item in ConnectedNodes)
                    {
                        Console.WriteLine("ID: {0} | IP Address: {1} | Port: {2}", neighborID, item.IPAddress, item.Port);
                        neighborID++;
                    }
                    Console.Write("Please select neighbor ID: ");
                    string _neighborIdToSend = Console.ReadLine();
                    int ID = int.Parse(_neighborIdToSend);

                    var _selectedNode = ConnectedNodes[ID];
                    Console.WriteLine("ID selected: " + _selectedNode.IPAddress + _selectedNode.Port);


                    Console.Write("Please send a message: ");
                    string _msgToSend = Console.ReadLine();
                    //await Mediator.Send(new SendMessageCommand(_selectedNode, _msgToSend));

                    await Mediator.Send(new SendFindTipCommand(_selectedNode));


                    goto start;

                case "3":
                    goto start;
                default:
                    Console.WriteLine("Invalid Command!");
                    goto start;
            }

        }
    }
}