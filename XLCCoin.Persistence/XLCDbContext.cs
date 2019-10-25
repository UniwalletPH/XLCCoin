using System;
using Microsoft.EntityFrameworkCore;
using XLCCoin.Application.Interfaces;
using XLCCoin.Domain.Entities;

namespace XLCCoin.Persistence
{
    public class XLCDbContext : DbContext, IXLCDbContext
    {
        private readonly ISecurity security;

        public XLCDbContext(ISecurity security, DbContextOptions<XLCDbContext> option) : base(option)
        {
            this.security = security;

            security.IsValid("vrynxzent");
        }

        public DbSet<AddressKey> AddressKeys { get; set; }
        public DbSet<TangleType> TangleTypes { get; set; }
        public DbSet<TokenType> TokenTypes { get; set; }
    }
}