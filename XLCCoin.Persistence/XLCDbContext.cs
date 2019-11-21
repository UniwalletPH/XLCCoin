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

        }

        public DbSet<Node> Nodes { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transite> Transites { get; set; }
        public DbSet<TransiteConnection> TransiteConnections { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(XLCDbContext).Assembly);
        }
    }
}