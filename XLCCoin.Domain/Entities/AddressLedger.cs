using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using XLCCoin.Domain.Entities.Base;

namespace XLCCoin.Domain.Entities
{
    public class AddressLedger : BaseEntity
    {
        public long AddressKeyID { get; set; }
        public string AddressValue { get; set; }
        public decimal AddressBalance { get; set; }
        public string AddressHash { get; set; }

      
        public AddressKey AddressKeys { get; set; }

    }
}
