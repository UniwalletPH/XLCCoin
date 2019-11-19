using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XLCCoin.Persistence.Migrations
{
    public partial class X1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transites",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    FromWalletID = table.Column<Guid>(nullable: false),
                    ToWalletID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transites", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transites_Wallets_FromWalletID",
                        column: x => x.FromWalletID,
                        principalTable: "Wallets",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Transites_Wallets_ToWalletID",
                        column: x => x.ToWalletID,
                        principalTable: "Wallets",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transites_FromWalletID",
                table: "Transites",
                column: "FromWalletID");

            migrationBuilder.CreateIndex(
                name: "IX_Transites_ToWalletID",
                table: "Transites",
                column: "ToWalletID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transites");
        }
    }
}
