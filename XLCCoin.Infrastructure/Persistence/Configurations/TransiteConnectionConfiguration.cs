using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using XLCCoin.Domain.Entities;

namespace XLCCoin.Infrastructure.Persistence.Configurations
{
    public class TransiteConnectionConfiguration : IEntityTypeConfiguration<TransiteConnection>
    {
        public void Configure(EntityTypeBuilder<TransiteConnection> builder)
        {
            builder.HasOne(a => a.FromTransite).WithMany(b => b.FromTransiteConnections)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.ToTransite).WithMany(b => b.ToTransiteConnections)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
