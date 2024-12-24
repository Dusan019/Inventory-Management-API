using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_Management_API.Migrations
{
    /// <inheritdoc />
    public partial class NormalizedEmailUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "b937f496-526f-472e-a558-18b031e52212", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEFdd72JTVCWS/mnGiMhBplxu4E9Im9J3QuHg00NwKIBK1UXmlURfVFn0++ZaCzM8Cw==", "admin@example.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "31b69165-c6f3-4425-a4af-ed5e61ebb660", "MANAGER@EXAMPLE.COM", "MANAGER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEIC7tl/TOnU9QZ0OgD+guGOVAGBNrJ/KH1qFDoKOG42kENd7dmJHse4ghGO0Hchd4w==", "manager@example.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "aa782477-b264-4e91-9645-a8cb9a1b547f", "EMPLOYEE@EXAMPLE.COM", "EMPLOYEE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEA7RwbLjwhjA4afDluMBqOa1xIreTYfdwnxoddmnPEImhdYax6Tv8Tqjstqc4SWysA==", "employee@example.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "3e6153b5-3201-4651-8c92-8aa769e8d480", null, null, "AQAAAAIAAYagAAAAENshuGtr5IAAQkgPyl4130F0Ld/+/6PwcPgFrNCFUclHF3fU04waBScFJlxN0KEzdQ==", "admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "10050f78-cd52-4e16-b8f7-3bc2a9898510", null, null, "AQAAAAIAAYagAAAAEK5Cba44ErW2uCk8jppvgD8VFwlKvO54YE2kweodXqHgOiYXM3h/SJutLYoJlxHRVA==", "manager" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "6caa528e-d3f3-467d-8de7-2e400e80a736", null, null, "AQAAAAIAAYagAAAAEDWo8mo8u2FPryt1ZO2GXT1poKOg/j7F+ZWp84Btm19jX/3AcSG3n5sNHwpUnhktxw==", "employee" });
        }
    }
}
