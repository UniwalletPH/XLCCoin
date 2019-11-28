using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Domain.Entities;

namespace XLCCoin.Application.Interfaces
{
    public interface IXLCDbContext
    {
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transite> Transites { get; set; }
        public DbSet<TransiteConnection> TransiteConnections { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default );
    }
}
