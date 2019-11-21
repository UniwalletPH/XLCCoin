using System;
using System.Collections.Generic;
using System.Text;
using XLCCoin.Application.Interfaces;

namespace XLCCoin.Application.Common.Base
{
    public class BaseRequestHandler
    {
        internal readonly IXLCDbContext dbContext;

        public BaseRequestHandler(IXLCDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
