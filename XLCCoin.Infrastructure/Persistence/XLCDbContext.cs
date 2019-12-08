using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using XLCCoin.Application.Interfaces;
using XLCCoin.Domain.Entities;

namespace XLCCoin.Infrastructure.Persistence
{
    public class XLCDbContext : DbContext, IXLCDbContext
    {
        public XLCDbContext(DbContextOptions<XLCDbContext> option) : base(option)
        {

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
