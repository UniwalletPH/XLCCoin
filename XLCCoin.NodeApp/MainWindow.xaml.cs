using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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
            (this.DataContext as MainWindowVM).AddLog("haha");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
        }
    }
}
