using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using XLCCoin.Domain.Entities.Base;

namespace XLCCoin.Domain.Entities
{
    [Table("tbl_XYZ")]
    public class AddressKey : BaseEntity
    {
        public string Value { get; set; }



    }
}
