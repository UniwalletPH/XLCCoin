using System;
using System.Collections.Generic;
using XLCCoin.Domain.Entities;
using XLCCoin.Web.Models; 
namespace XLCCoin.Web.Models
{
    public class CenterViewModel   
    {
        public List<Node> ListofNodesAvailable { get; set; } = new List<Node>();
    }
}
