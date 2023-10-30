using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace student_management.Migrations
{
    /// <inheritdoc />
    public partial class ColumnsUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Students",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Students",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Admins",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Admins",
                newName: "CreatedOn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Students",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Students",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Admins",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Admins",
                newName: "CreatedAt");
        }
    }
}
