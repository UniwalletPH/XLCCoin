using System;
using System.Collections.Generic;
using System.Text;

namespace XLCCoin.Application.NodeCommands.Queries
{
    public class TranSiteVM
    {
        public string CommandName { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public object Response { get; set; }
    }
}
