using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace student_management.Migrations
{
    /// <inheritdoc />
    public partial class Pid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Decription",
                table: "Courses",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Pid",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pid",
                table: "Departments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pid",
                table: "Admins",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pid",
                value: "");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Pid",
                value: "");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                column: "Pid",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pid",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Pid",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Pid",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Courses",
                newName: "Decription");
        }
    }
}
