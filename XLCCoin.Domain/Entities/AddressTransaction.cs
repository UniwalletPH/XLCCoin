using System;
using System.Collections.Generic;
using System.Text;
using XLCCoin.Domain.Entities.Base;

namespace XLCCoin.Domain.Entities
{
    public class AddressTransaction : BaseEntity
    {
        public long TangleTransactionID { get; set; }
        public long AddressLedgerID { get; set; }
        public decimal Amount { get; set; }
        public long SourceAddressLedgerID { get; set; }
        public string TransactionHash { get; set; }


        public TangleTransaction TangleTransaction { get; set; }

        public AddressLedger AddressLedger { get; set; }
    }
}
