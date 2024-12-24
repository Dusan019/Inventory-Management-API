using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_Management_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3e6153b5-3201-4651-8c92-8aa769e8d480", "AQAAAAIAAYagAAAAENshuGtr5IAAQkgPyl4130F0Ld/+/6PwcPgFrNCFUclHF3fU04waBScFJlxN0KEzdQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "10050f78-cd52-4e16-b8f7-3bc2a9898510", "AQAAAAIAAYagAAAAEK5Cba44ErW2uCk8jppvgD8VFwlKvO54YE2kweodXqHgOiYXM3h/SJutLYoJlxHRVA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6caa528e-d3f3-467d-8de7-2e400e80a736", "AQAAAAIAAYagAAAAEDWo8mo8u2FPryt1ZO2GXT1poKOg/j7F+ZWp84Btm19jX/3AcSG3n5sNHwpUnhktxw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "42de3650-c5f5-4003-9045-749461a67c4f", "AQAAAAIAAYagAAAAEAxJ0uc1cCojiIv9Tp7aOE+57fLOpA48RIqppaMgSIP5McnUC7vuxwZiMjS+OTmQ5Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "58af1daf-806e-4a41-ae12-c1f083c8d641", "AQAAAAIAAYagAAAAENk3IWL2ANreTVoevooDu0tSae265k24PEIvi4vZlbX+Y2uu8XObMX90VPoiwHzqmg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c16cad17-cf9f-47b9-94c8-e15e3b919a11", "AQAAAAIAAYagAAAAECuycPJAbaGYeZycckcQd40DA1/1wTMrYp4AwW5KKTYhLlJwBpoSyldIQBZXAA8iZA==" });
        }
    }
}
