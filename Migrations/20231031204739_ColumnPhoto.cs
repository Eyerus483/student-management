using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace student_management.Migrations
{
    /// <inheritdoc />
    public partial class ColumnPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ROle",
                table: "Departments",
                newName: "Role");

            migrationBuilder.AddColumn<string>(
                name: "Pid",
                table: "Teachers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pid",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Departments",
                newName: "ROle");
        }
    }
}
