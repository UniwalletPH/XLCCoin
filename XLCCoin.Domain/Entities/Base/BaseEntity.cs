using System;
using System.Collections.Generic;
using System.Text;

namespace XLCCoin.Domain.Entities.Base
{
    public class BaseEntity
    {
        public long ID { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
