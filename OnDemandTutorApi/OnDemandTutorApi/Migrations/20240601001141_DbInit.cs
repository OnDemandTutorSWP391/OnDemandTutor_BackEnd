using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnDemandTutorApi.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "407d2c70-b4f7-41d1-82ed-c0bb4c337d67");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "705a805d-1ff8-4c90-a1c7-d1d045b58112");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "8b0f11a7-a61b-4de3-b2a2-85706a8e1276");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "99fe6e61-8c42-47cc-9632-5a2528bf4abc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "407d2c70-b4f7-41d1-82ed-c0bb4c337d67", "2", "Moderator", "MODERATOR" },
                    { "705a805d-1ff8-4c90-a1c7-d1d045b58112", "1", "Admin", "ADMIN" },
                    { "8b0f11a7-a61b-4de3-b2a2-85706a8e1276", "4", "Student", "STUDENT" },
                    { "99fe6e61-8c42-47cc-9632-5a2528bf4abc", "3", "Tutor", "TUTOR" }
                });
        }
    }
}
