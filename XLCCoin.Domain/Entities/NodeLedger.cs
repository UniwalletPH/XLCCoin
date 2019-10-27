using System;
using System.Collections.Generic;
using System.Text;
using XLCCoin.Domain.Entities.Base;

namespace XLCCoin.Domain.Entities
{
    public class NodeLedger : BaseEntity

    {
        public string NodeName { get; set; }
        public string NodeSignature { get; set; }
        public string NodeIP { get; set; }
        public int NodePort { get; set; }
        public int NetworkLatency { get; set; }
        public string NodeTimezone { get; set; }
        public string NodePrimaryTangleTypeSignature { get; set; }
        public string LastConnected { get; set; }
        public int IsOnline { get; set; }
    }
}
