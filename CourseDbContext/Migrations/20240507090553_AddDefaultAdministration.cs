using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseDbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultAdministration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "date_time_registration", "email", "full_name", "number_phone", "password", "user_name", "user_type" },
                values: new object[] { new Guid("4f1be005-4b17-41f3-b3fb-d7419750523c"), new DateTime(2024, 5, 7, 9, 5, 52, 747, DateTimeKind.Utc).AddTicks(1181), "oleg.maionezov@gmail.com", "Олег Майонезов Степанович", "+37377712345", "DAAAD6E5604E8E17BD9F108D91E26AFE6281DAC8FDA0091040A7A6D7BD9B43B5", "Oleg", "Administrator" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: new Guid("4f1be005-4b17-41f3-b3fb-d7419750523c"));
        }
    }
}
