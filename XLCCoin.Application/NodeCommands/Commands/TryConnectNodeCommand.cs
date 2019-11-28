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
    public class TryConnectNodeCommand : IRequest<string>
    {
        public string Server { get; set; }
        public string Message { get; set; }

        public TryConnectNodeCommand(String server) //constructor
        {
            Server = server;
        }

        public class TryConnectNodeCommandHandler : BaseRequestHandler, IRequestHandler<TryConnectNodeCommand, string>
        {
            public TryConnectNodeCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {
            }

            public async Task<string> Handle(TryConnectNodeCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Int32 port = 13000;

                    TcpClient client = new TcpClient(request.Server, port);
                    NetworkStream stream = client.GetStream();

                    while (stream != null)
                    {
                        //Translate the Message into ASCII.
                        Console.Write("Please send a message:");
                        string msgs = Console.ReadLine();
                        Byte[] data = System.Text.Encoding.ASCII.GetBytes(msgs);

                        //Send the message to the connected TcpServer.
                        stream.Write(data, 0, data.Length);
                        Console.WriteLine("Message Sent: {0}", msgs);

                        //Bytes Array to receive Server Response.
                        data = new Byte[256];
                        String response = String.Empty;

                        //Read the Tcp Server Response Bytes.
                        Int32 bytes = stream.Read(data, 0, data.Length);
                        response = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        Console.WriteLine("Received: {0}", response);

                    }
                    stream.Close();
                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: {0}", e);
                }
                Console.Read();
                return "Success";
            }
        }
    }
}