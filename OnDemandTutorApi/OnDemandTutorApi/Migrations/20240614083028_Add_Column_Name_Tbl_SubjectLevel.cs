using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnDemandTutorApi.Migrations
{
    public partial class Add_Column_Name_Tbl_SubjectLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "SubjectLevel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "dbo",
                table: "SubjectLevel");
        }
    }
}
