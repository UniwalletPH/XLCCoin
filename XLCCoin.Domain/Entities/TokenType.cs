using System;
using System.Collections.Generic;
using System.Text;
using XLCCoin.Domain.Entities.Base;

namespace XLCCoin.Domain.Entities
{
    public class TokenType : BaseEntity
    {
        public long TangleTypeID { get; set; }

        public Guid UID { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }

        public TangleType TangleType { get; set; }
    }
}
