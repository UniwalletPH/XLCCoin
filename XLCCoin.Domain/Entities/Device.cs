using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using XLCCoin.Domain.Entities.Base;
using XLCCoin.Domain.Enums;

namespace XLCCoin.Domain.Entities
{
    public class Device : BaseEntity
    {
        [ForeignKey("Node")]
        public override Guid ID { get; set; }

        public DeviceType DeviceType { get; set; }
        public int NumberOfAllowedConnections { get; set; }

        public Node Node { get; set; }
    }
}
