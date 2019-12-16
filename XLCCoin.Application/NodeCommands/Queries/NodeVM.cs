using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using XLCCoin.Domain.Entities;
using XLCCoin.Domain.Entities.Base;

namespace XLCCoin.Application.NodeCommands.Queries
{
    public class NodeVM : BaseEntity
    {

        public string IPAddress { get; set; }
        public int Port { get; set; }
        public bool IsBehindNAT { get; set; }
        public string Geolocation { get; set; }

        private string name;
        public string Name
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    return name.Trim();
                }

                return $"{IPAddress}:{Port}";
            }
        }

        public bool IsConnected { get; set; }

        public TcpClient Client { get; set; }


        public List<TranSiteVM> TIPS { get; set; }
    }
}

