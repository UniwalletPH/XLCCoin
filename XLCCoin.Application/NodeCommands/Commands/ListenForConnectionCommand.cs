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
    public class ListenForConnectionCommand : IRequest<string>
    {
        public string IP { get; set; }
        public int Port { get; set; }

        public ListenForConnectionCommand(string ip, int port) //constructor
        {
            IP = ip;
            Port = port;
        }

     

       

        public class ListenForConnectionCommandHandler : BaseRequestHandler, IRequestHandler<ListenForConnectionCommand, string>
        {
            public ListenForConnectionCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {
            }

            public void HandleDevice(Object obj) //receives bytes stream from the client(s)
            {
                TcpClient client = (TcpClient)obj;
                var stream = client.GetStream();
                string imei = String.Empty;
                string data = null;
                Byte[] bytes = new Byte[256];
                int i;

                try
                {
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0) //parsing of data from BIT to STRING
                    {
                        string hex = BitConverter.ToString(bytes);
                        data = Encoding.ASCII.GetString(bytes, 0, i);

                        Console.WriteLine("{1}: Received: {0}", data, Thread.CurrentThread.ManagedThreadId);
                        string str = "Hey Device!";

                        Byte[] reply = System.Text.Encoding.ASCII.GetBytes(str);

                        stream.Write(reply, 0, reply.Length);
                        Console.WriteLine("{1}: Sent: {0}", str, Thread.CurrentThread.ManagedThreadId);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: {0}", e.ToString());
                    client.Close();
                }
            }

            public async Task<string> Handle(ListenForConnectionCommand request, CancellationToken cancellationToken)
            {
                TcpListener server = null; //initialize server
                IPAddress localAddress = IPAddress.Parse(request.IP);
                server = new TcpListener(localAddress, request.Port);
                server.Start();

                try
                {
                    while (true)
                    {
                        Console.WriteLine("Waiting for a connection...");
                        TcpClient client = server.AcceptTcpClient();
                        Console.WriteLine("Connected!");
                        Thread t = new Thread(new ParameterizedThreadStart(HandleDevice));
                        t.Start(client);
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketException: {0}", e);
                    server.Stop();
                }

                return "Success";
            }
        }
    }
}
