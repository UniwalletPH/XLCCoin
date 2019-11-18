using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using XLCCoin.Domain.Entities.Base;

namespace XLCCoin.Domain.Entities
{
    public class Transite : BaseEntity
    {
        [ForeignKey("FromWallet")]
        public Guid FromWalletID { get; set; }

        [ForeignKey("ToWallet")]
        public Guid ToWalletID { get; set; }

        public decimal Amount { get; set; }

        public Wallet FromWallet { get; set; }
        public Wallet ToWallet { get; set; }



        public ICollection<TransiteConnection> FromTransiteConnections { get; set; }
        public ICollection<TransiteConnection> ToTransiteConnections { get; set; }
    }
}
