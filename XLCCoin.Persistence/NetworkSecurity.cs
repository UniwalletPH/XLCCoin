using System;
using System.Collections.Generic;
using System.Text;
using XLCCoin.Application.Interfaces;

namespace XLCCoin.Persistence
{
    public class NetworkSecurity : ISecurity
    {
        public string User { get; set; }

        public bool IsValid(string username)
        {
            if (username == "vrynxzent")
            {
                return true;
            }

            return false;
        }
    }
}
