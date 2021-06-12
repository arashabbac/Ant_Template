using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertDateTime = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(maxLength: 50, nullable: true),
                    Username = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 64, nullable: false),
                    NationalCode = table.Column<string>(maxLength: 10, nullable: true),
                    CellPhoneNumber = table.Column<string>(maxLength: 14, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CellPhoneNumber", "Description", "FullName", "InsertDateTime", "IsActive", "IsDeleted", "NationalCode", "Password", "Type", "Username" },
                values: new object[] { new Guid("525fec52-df2e-472f-9162-e9cb81b7e94b"), "09351008895", null, "آرش عباسی", new DateTime(2021, 6, 12, 20, 52, 54, 707, DateTimeKind.Local).AddTicks(7866), true, false, null, "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8", 900, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
