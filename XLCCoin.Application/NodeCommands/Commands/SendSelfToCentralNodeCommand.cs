using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace XLCCoin.Application.NodeCommands.Commands
{
    public class SendCentralSelfNode
    {
        TcpListener server = null; //initialize server
        public SendCentralSelfNode(string ip, int port) //constructor
        {
            IPAddress localAddress = IPAddress.Parse(ip);
            server = new TcpListener(localAddress, port);
            server.Start();
            StartListener();
        }

        public void StartListener() //keeps listening for incoming connections then 
        {
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
    }
}
