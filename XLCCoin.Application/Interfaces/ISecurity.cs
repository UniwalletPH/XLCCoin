using System;
using System.Collections.Generic;
using System.Text;

namespace XLCCoin.Application.Interfaces
{
    public interface ISecurity
    {
        string User { get; set; }

        bool IsValid(string username);
    }
}
