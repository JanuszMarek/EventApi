using Microsoft.EntityFrameworkCore.Migrations;

namespace EventApi.Migrations
{
    public partial class EventRestrictions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "EventTickets",
                newName: "IsDeleted");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "EventTickets",
                newName: "IsActive");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
