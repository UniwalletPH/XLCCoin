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
    public class ListenForConnectionCommand : IRequest<IPEndPoint>
    {
        private readonly IPEndPoint myEndpoint;
        private readonly Action<TcpClient> whenConnected;

        public ListenForConnectionCommand(IPEndPoint myEndpoint, Action<TcpClient> whenConnected)
        {
            this.myEndpoint = myEndpoint;
            this.whenConnected = whenConnected;
        }

        public class ListenForConnectionCommandHandler : 
            BaseRequestHandler, 
            IRequestHandler<ListenForConnectionCommand, IPEndPoint>
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
                        string str = "";
                        int threadID;
                        Byte[] reply = null;
                        switch (data)
                        {
                            case "findtip":
                                Console.WriteLine("Thread ID: {0} | Server is finding possible tips...", Thread.CurrentThread.ManagedThreadId);
                                threadID = Thread.CurrentThread.ManagedThreadId;
                                str = "Thread ID: " + threadID + "| Server is finding possible tips...";
                                reply = System.Text.Encoding.ASCII.GetBytes(str);
                                stream.Write(reply, 0, reply.Length);
                                break;

                            case "updatedb":
                                Console.WriteLine("Thread ID: {0} | Server is updating database...", Thread.CurrentThread.ManagedThreadId);
                                threadID = Thread.CurrentThread.ManagedThreadId;
                                str = "Thread ID: " + threadID + "| Server is finding updating database...";
                                reply = System.Text.Encoding.ASCII.GetBytes(str);
                                stream.Write(reply, 0, reply.Length);
                                break;

                            case "getneighbor":
                                Console.WriteLine("Thread ID: {0} | Server is fetching neighbor nodes...", Thread.CurrentThread.ManagedThreadId);
                                threadID = Thread.CurrentThread.ManagedThreadId;
                                str = "Thread ID: " + threadID + "| Server is finding fetching neighbor nodes...";
                                reply = System.Text.Encoding.ASCII.GetBytes(str);
                                stream.Write(reply, 0, reply.Length);
                                break;

                            case "checkvalidation":
                                Console.WriteLine("Thread ID: {0} | Server is checking...", Thread.CurrentThread.ManagedThreadId);
                                threadID = Thread.CurrentThread.ManagedThreadId;
                                str = "Thread ID: " + threadID + "| Server is finding fetching checking...";
                                reply = System.Text.Encoding.ASCII.GetBytes(str);
                                stream.Write(reply, 0, reply.Length);
                                break;

                            default:
                                Console.WriteLine("Thread ID: {0} | Invalid command!", Thread.CurrentThread.ManagedThreadId);
                                threadID = Thread.CurrentThread.ManagedThreadId;
                                str = "Thread ID: " + threadID + "| Invalid Command!";
                                reply = System.Text.Encoding.ASCII.GetBytes(str);
                                stream.Write(reply, 0, reply.Length);
                                break;
                        }
                        ////Console.WriteLine("{1}: Message Received: {0}", data, Thread.CurrentThread.ManagedThreadId);
                        ////string str = "Hey Device!";
                        //Console.Write("Please send a message: ");
                        //string str = Console.ReadLine();
                        //Byte[] reply = System.Text.Encoding.ASCII.GetBytes(str);
                        ////if byte
                        //stream.Write(reply, 0, reply.Length);
                        //Console.WriteLine("{1}: Message Sent: {0}", str, Thread.CurrentThread.ManagedThreadId);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: {0}", e.ToString());
                    client.Close();
                }
            }

            public async Task<IPEndPoint> Handle(ListenForConnectionCommand request, CancellationToken cancellationToken)
            {
                IPEndPoint _retVal = request.myEndpoint;

                TcpListener server = new TcpListener(_retVal.Address, _retVal.Port);

                server.Start();

                try
                {
                    Thread _listenForConnectionThread = new Thread(() =>
                    {
                        while (true)
                        {
                            TcpClient client = server.AcceptTcpClient();

                            request.whenConnected(client);


                            //Thread t = new Thread(new ParameterizedThreadStart(HandleDevice));
                            //t.Start(client);
                        }
                    });

                    _listenForConnectionThread.Start();
                }
                catch (SocketException e)
                {
                    server.Stop();
                }

                return _retVal;
            }
        }
    }
}