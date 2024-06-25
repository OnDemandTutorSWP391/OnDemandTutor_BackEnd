using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnDemandTutorApi.Migrations
{
    public partial class add_column_IsLocked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                schema: "dbo",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                schema: "dbo",
                table: "User");
        }
    }
}
