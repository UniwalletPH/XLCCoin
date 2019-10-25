using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using XLCCoin.Domain.Entities;

namespace XLCCoin.Application.Interfaces
{
    public interface IXLCDbContext
    {
        DbSet<AddressKey> AddressKeys { get; set; }

        DbSet<TangleType> TangleTypes { get; set; }
        DbSet<TokenType> TokenTypes { get; set; }
    }
}
