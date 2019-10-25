using System;
using System.Collections.Generic;
using System.Text;
using XLCCoin.Domain.Entities.Base;

namespace XLCCoin.Domain.Entities
{
    public class TangleType : BaseEntity
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public int MaxTokenTypes { get; set; }
        public decimal MaxSupply { get; set; }
        public int Signature { get; set; }
    }
}
