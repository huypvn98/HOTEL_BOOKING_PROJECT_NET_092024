using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingHotel.Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRolesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BE072024_HB_Roles",
                columns: new[] { "RoleID", "Description", "RoleName" },
                values: new object[,]
                {
                    { 1, "Administrator role with full permissions", "Admin" },
                    { 2, "Regular user with limited permissions", "User" },
                    { 3, "Customer role with permissions to book and view hotels", "Customer" },
                    { 4, "Staff role with permissions to manage hotel operations", "Staff" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BE072024_HB_Roles",
                keyColumn: "RoleID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BE072024_HB_Roles",
                keyColumn: "RoleID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BE072024_HB_Roles",
                keyColumn: "RoleID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BE072024_HB_Roles",
                keyColumn: "RoleID",
                keyValue: 4);
        }
    }
}
