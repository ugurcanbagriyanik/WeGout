using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGout.Migrations
{
    public partial class miggration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "LastSignIn", "SignUpDate" },
                values: new object[] { new DateTime(2021, 12, 26, 9, 33, 38, 104, DateTimeKind.Utc).AddTicks(6832), new DateTime(2021, 12, 26, 9, 33, 38, 104, DateTimeKind.Utc).AddTicks(6836) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "LastSignIn", "SignUpDate" },
                values: new object[] { new DateTime(2021, 11, 21, 14, 16, 0, 381, DateTimeKind.Utc).AddTicks(2398), new DateTime(2021, 11, 21, 14, 16, 0, 381, DateTimeKind.Utc).AddTicks(2403) });
        }
    }
}
