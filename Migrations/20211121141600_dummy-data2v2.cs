using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGout.Migrations
{
    public partial class dummydata2v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FileStorage",
                columns: new[] { "Id", "Extension", "Name", "Path" },
                values: new object[] { 1L, "jpg", "none", "none" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Coin", "Email", "GenderId", "LastSignIn", "Name", "Password", "PhoneNumber", "ProfilePhotoId", "SignUpDate", "Surname" },
                values: new object[] { 1L, 0L, "ugur@wegout.com", 1L, new DateTime(2021, 11, 21, 14, 16, 0, 381, DateTimeKind.Utc).AddTicks(2398), "Ugurcan", "12345", "05398478481", 1L, new DateTime(2021, 11, 21, 14, 16, 0, 381, DateTimeKind.Utc).AddTicks(2403), "Bagriyanik" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "FileStorage",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
