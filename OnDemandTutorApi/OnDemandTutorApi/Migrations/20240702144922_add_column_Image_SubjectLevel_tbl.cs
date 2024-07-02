using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnDemandTutorApi.Migrations
{
    public partial class add_column_Image_SubjectLevel_tbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                schema: "dbo",
                table: "SubjectLevel",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                schema: "dbo",
                table: "SubjectLevel");
        }
    }
}
