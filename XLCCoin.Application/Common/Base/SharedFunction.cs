using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace XLCCoin.Application.Common.Base
{
    public static class SharedFunction
    {
        public static IPAddress GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return IPAddress.Parse(ip.ToString());
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static int GetRandomPort()
        {
            return new Random().Next(5000, 6000);
        }
    }
}
