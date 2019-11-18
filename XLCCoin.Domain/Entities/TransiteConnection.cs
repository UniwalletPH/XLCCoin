using System;
using System.Collections.Generic;
using System.Text;
using XLCCoin.Domain.Entities.Base;

namespace XLCCoin.Domain.Entities
{
    public class TransiteConnection : BaseEntity
    {
        public Guid FromTransiteID { get; set; }
        public Guid ToTransiteID { get; set; }

        public Transite FromTransite { get; set; }
        public Transite ToTransite { get; set; }
    }
}
