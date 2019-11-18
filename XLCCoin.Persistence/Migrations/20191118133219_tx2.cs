using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XLCCoin.Persistence.Migrations
{
    public partial class tx2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransiteConnections");
        }
    }
}
