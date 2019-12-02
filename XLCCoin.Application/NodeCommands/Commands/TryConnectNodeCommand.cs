using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;

namespace XLCCoin.Application.NodeCommands.Commands
{
    public class TryConnectNodeCommand : IRequest<TcpClient>
    {
        private readonly IPEndPoint endpoint;

        public TryConnectNodeCommand(IPEndPoint endpoint)
        {
            this.endpoint = endpoint;
        }

        public class TryConnectNodeCommandHandler : BaseRequestHandler, IRequestHandler<TryConnectNodeCommand, TcpClient>
        {
            public TryConnectNodeCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {
            }

            public async Task<TcpClient> Handle(TryConnectNodeCommand request, CancellationToken cancellationToken)
            {

                string _ip = request.endpoint.Address.ToString();
                int _port = request.endpoint.Port;

                TcpClient _client = new TcpClient();

                try
                {


                    _client.Connect(_ip, _port);
                    //using (_client = new TcpClient(request.endpoint.Address.ToString(), request.endpoint.Port))
                    //using (NetworkStream _stream = _client.GetStream())
                    //{
                    //    while (_stream != null)
                    //    {
                    //        string msgs = Console.ReadLine();
                    //        Byte[] data = System.Text.Encoding.ASCII.GetBytes(msgs);

                    //        //Send the message to the connected TcpServer.
                    //        _stream.Write(data, 0, data.Length);
                    //        Console.WriteLine("Message Sent: {0}", msgs);

                    //        //Bytes Array to receive Server Response.
                    //        data = new Byte[256];
                    //        String response = String.Empty;

                    //        //Read the Tcp Server Response Bytes.
                    //        Int32 bytes = _stream.Read(data, 0, data.Length);
                    //        response = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    //        Console.WriteLine("Received: {0}", response);

                    //    }
                    //}
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: {0}", e);
                }

                return _client;
            }
        }
    }
}