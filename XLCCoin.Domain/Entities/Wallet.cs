using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using XLCCoin.Domain.Entities.Base;

namespace XLCCoin.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public Guid NodeID { get; set; }

        public Node Node { get; set; }

        public string Address { get; set; }
        public decimal Amount { get; set; }

        public ICollection<Transite> FromTransites { get; set; }
        public ICollection<Transite> ToTransites { get; set; }
    }
}
