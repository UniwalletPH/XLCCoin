using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XLCCoin.Application.NodeCommands.Commands;
using XLCCoin.Application.NodeCommands.Queries;

namespace XLCCoin.NodeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMediator mediator;
        string _baseUrl = "http://leracoin.azurewebsites.net";


        public MainWindow(IMediator mediator)
        {
            InitializeComponent();

            this.mediator = mediator;
        }

        private void AddLog(string log, params object[] args)
        {
            if (args != null)
            {
                this.Dispatcher.Invoke(() =>
                {
                    (this.DataContext as MainWindowVM).AddLog(string.Format(log, args));
                });

            }
            else
            {
                this.Dispatcher.Invoke(() =>
                {
                    (this.DataContext as MainWindowVM).AddLog(log);
                });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                // Get Neighbor Command 
                AddLog("Fetching neighbors...");

                var _getNeighbors = new GetNeighborsCommand($"{_baseUrl}/Node/Neighbors");

                List<NodeVM> _neighbors = mediator.Send(_getNeighbors).Result;

                AddLog("{0} neighbor/s found", _neighbors.Count());


                List<TranSiteVM> _transites = new List<TranSiteVM>();

                foreach (var _node in _neighbors)
                {
                    AddLog("Connecting node..");

                    IPEndPoint _nodeEndpoint = new IPEndPoint(IPAddress.Parse(_node.IPAddress), _node.Port);

                    TryConnectNodeCommand _connectCmd = new TryConnectNodeCommand(_nodeEndpoint);

                    TcpClient _client = mediator.Send(_connectCmd).Result;

                    if (_client.Connected)
                    {
                        NodeVM _connectedNode = new NodeVM
                        {
                            Client = _client,
                        };

                        this.Dispatcher.Invoke(() =>
                        {
                            (this.DataContext as MainWindowVM).AddConnectedNode(_connectedNode);
                        });

                        var _r = mediator.Send(new ListenMessageCommand(_connectedNode, (string msg) =>
                        {
                            AddLog($"Message: {msg}");
                        })).Result;

                        AddLog("Connected!");
                    }
                }
            }).Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            AddLog($"MID server: {_baseUrl}");

            var _myEndpoint = mediator.Send(new GenerateSelfNodeEndpointCommand())
                .GetAwaiter()
                .GetResult();

            AddLog("Generating self address..");
            AddLog("My Address: {0}:{1}", _myEndpoint.Address, _myEndpoint.Port);

            ListenForConnectionCommand _listenforcon = new ListenForConnectionCommand(
                myEndpoint: _myEndpoint,
                whenConnected: async (NodeVM connectedNode) =>
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        (this.DataContext as MainWindowVM).AddConnectedNode(connectedNode);
                    });

                    await mediator.Send(new ListenMessageCommand(connectedNode, (string msg) =>
                    {
                        Console.WriteLine("The message is: " + msg);
                    }));
                });

            mediator.Send(_listenforcon)
                .GetAwaiter()
                .GetResult();

            AddLog("Listening for connection..");


            new Thread(() =>
            {
                AddLog("Sending self to MID: {0}", $"/Node/Register");

                var _sendSelf = new SendSelfCommand(_myEndpoint, $"{_baseUrl}/Node/Register");

                mediator.Send(_sendSelf)
                    .GetAwaiter()
                    .GetResult();

                AddLog("Send self done!");
            }).Start();
        }
    }
}
