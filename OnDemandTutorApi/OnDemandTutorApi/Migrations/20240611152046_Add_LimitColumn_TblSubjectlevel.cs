using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnDemandTutorApi.Migrations
{
    public partial class Add_LimitColumn_TblSubjectlevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.AddColumn<int>(
                name: "LimitMember",
                schema: "dbo",
                table: "SubjectLevel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimitMember",
                schema: "dbo",
                table: "SubjectLevel");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21fbb4e5-6aa0-415b-b291-cef7d067d04e", "1", "Admin", "ADMIN" },
                    { "aca0397a-ca5b-4ccf-b742-14db92651480", "2", "Moderator", "MODERATOR" },
                    { "b378a07f-e146-434e-88ce-1cd7dd087dc2", "4", "Student", "STUDENT" },
                    { "c530249e-5b13-4566-8e5b-36f66e958ef2", "3", "Tutor", "TUTOR" }
                });
        }
    }
}
