using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XLCCoin.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    IsBehindNAT = table.Column<bool>(nullable: false),
                    Geolocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DeviceType = table.Column<byte>(nullable: false),
                    NumberOfAllowedConnections = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Devices_Nodes_ID",
                        column: x => x.ID,
                        principalTable: "Nodes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    NodeID = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Wallets_Nodes_NodeID",
                        column: x => x.NodeID,
                        principalTable: "Nodes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "TransiteConnections",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    FromTransiteID = table.Column<Guid>(nullable: false),
                    ToTransiteID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransiteConnections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransiteConnections_Transites_FromTransiteID",
                        column: x => x.FromTransiteID,
                        principalTable: "Transites",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TransiteConnections_Transites_ToTransiteID",
                        column: x => x.ToTransiteID,
                        principalTable: "Transites",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransiteConnections_FromTransiteID",
                table: "TransiteConnections",
                column: "FromTransiteID");

            migrationBuilder.CreateIndex(
                name: "IX_TransiteConnections_ToTransiteID",
                table: "TransiteConnections",
                column: "ToTransiteID");

            migrationBuilder.CreateIndex(
                name: "IX_Transites_FromWalletID",
                table: "Transites",
                column: "FromWalletID");

            migrationBuilder.CreateIndex(
                name: "IX_Transites_ToWalletID",
                table: "Transites",
                column: "ToWalletID");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_NodeID",
                table: "Wallets",
                column: "NodeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "TransiteConnections");

            migrationBuilder.DropTable(
                name: "Transites");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Nodes");
        }
    }
}
