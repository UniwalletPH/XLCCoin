using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using XLCCoin.Domain.Entities.Base;

namespace XLCCoin.Domain.Entities
{
    public class Node : BaseEntity
    {
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public bool IsBehindNAT { get; set; }
        public string Geolocation { get; set; }

        public Device Device { get; set; }
        public ICollection<Wallet> Wallets { get; set; }
    }
}
