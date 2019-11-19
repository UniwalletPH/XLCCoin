using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using XLCCoin.Domain.Entities;

namespace XLCCoin.Persistence.Configurations
{
    public class TransiteConfiguration : IEntityTypeConfiguration<Transite>
    {
        public void Configure(EntityTypeBuilder<Transite> builder)
        {
            builder.HasOne(a => a.FromWallet).WithMany(b => b.FromTransites)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.ToWallet).WithMany(b => b.ToTransites)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
