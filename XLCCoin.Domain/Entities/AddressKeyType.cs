using System;
using System.Collections.Generic;
using System.Text;
using XLCCoin.Domain.Entities.Base;

namespace XLCCoin.Domain.Entities
{
    public class AddressKeyType : BaseEntity
    {
        public string AddressKeyTypeName { get; set; }
        public string AddressKeyTypeDescription { get; set; }
        public string AddressKeyTypeISO3 { get; set; }

    }
}
