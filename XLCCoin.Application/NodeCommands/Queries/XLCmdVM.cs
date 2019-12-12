using System;
using System.Collections.Generic;
using System.Text;

namespace XLCCoin.Application.NodeCommands.Queries
{
    public class XLCmdVM
    {
        public string CommandName { get; set; }
        public Dictionary<string,object> Parameters { get; set; }
        public string Response { get; set; }

    }
}
