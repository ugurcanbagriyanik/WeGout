using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WeGout.Migrations
{
    public partial class favplacesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavPlaces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PlaceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavPlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavPlaces_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavPlaces_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "LastSignIn", "SignUpDate" },
                values: new object[] { new DateTime(2022, 5, 25, 13, 30, 15, 470, DateTimeKind.Utc).AddTicks(3439), new DateTime(2022, 5, 25, 13, 30, 15, 470, DateTimeKind.Utc).AddTicks(3441) });

            migrationBuilder.CreateIndex(
                name: "IX_FavPlaces_PlaceId",
                table: "FavPlaces",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_FavPlaces_UserId",
                table: "FavPlaces",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavPlaces");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "LastSignIn", "SignUpDate" },
                values: new object[] { new DateTime(2021, 12, 26, 9, 33, 38, 104, DateTimeKind.Utc).AddTicks(6832), new DateTime(2021, 12, 26, 9, 33, 38, 104, DateTimeKind.Utc).AddTicks(6836) });
        }
    }
}
