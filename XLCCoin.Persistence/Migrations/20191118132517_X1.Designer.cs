﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XLCCoin.Persistence;

namespace XLCCoin.Persistence.Migrations
{
    [DbContext(typeof(XLCDbContext))]
    [Migration("20191118132517_X1")]
    partial class X1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("XLCCoin.Domain.Entities.Device", b =>
                {
                    b.Property<Guid>("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("DeviceType")
                        .HasColumnType("tinyint");

                    b.Property<int>("NumberOfAllowedConnections")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("XLCCoin.Domain.Entities.Node", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Geolocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IPAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBehindNAT")
                        .HasColumnType("bit");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Nodes");
                });

            modelBuilder.Entity("XLCCoin.Domain.Entities.Transite", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("FromWalletID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ToWalletID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("FromWalletID");

                    b.HasIndex("ToWalletID");

                    b.ToTable("Transites");
                });

            modelBuilder.Entity("XLCCoin.Domain.Entities.Wallet", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("NodeID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("NodeID");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("XLCCoin.Domain.Entities.Device", b =>
                {
                    b.HasOne("XLCCoin.Domain.Entities.Node", "Node")
                        .WithOne("Device")
                        .HasForeignKey("XLCCoin.Domain.Entities.Device", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("XLCCoin.Domain.Entities.Transite", b =>
                {
                    b.HasOne("XLCCoin.Domain.Entities.Wallet", "FromWallet")
                        .WithMany("FromTransites")
                        .HasForeignKey("FromWalletID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("XLCCoin.Domain.Entities.Wallet", "ToWallet")
                        .WithMany("ToTransites")
                        .HasForeignKey("ToWalletID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("XLCCoin.Domain.Entities.Wallet", b =>
                {
                    b.HasOne("XLCCoin.Domain.Entities.Node", "Node")
                        .WithMany("Wallets")
                        .HasForeignKey("NodeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
