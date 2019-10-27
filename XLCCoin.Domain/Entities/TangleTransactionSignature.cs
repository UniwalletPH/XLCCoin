using System;
using System.Collections.Generic;
using System.Text;
using XLCCoin.Domain.Entities.Base;

namespace XLCCoin.Domain.Entities
{
    public class TangleTransactionSignature : BaseEntity
    {
        public long TangleTransactionID { get; set; }
        public string Signature { get; set; }
        public string NodeSignature { get; set; }


        public TangleTransaction TangleTransaction { get; set; }
    }
}
