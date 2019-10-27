using System;
using System.Collections.Generic;
using System.Text;
using XLCCoin.Domain.Entities.Base;

namespace XLCCoin.Domain.Entities
{
    public  class TangleTransaction : BaseEntity
    {
        public long CreatedByAddressLedgerID { get; set; }
        public string AddressLedgerIDArray { get; set; }
        public string SourceAddressLedgerIDArray { get; set; }
        public string TransactionAmountArray { get; set; }
        public string Message { get; set; }
        public byte[] MyProperty { get; set; }
        public string TransactionHash { get; set; }


        public AddressLedger AddressLedger { get; set; }

    }
}
