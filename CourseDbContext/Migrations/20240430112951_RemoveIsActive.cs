using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseDbContext.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "quantity_score",
                table: "rating",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "course",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "course");

            migrationBuilder.AlterColumn<int>(
                name: "quantity_score",
                table: "rating",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
