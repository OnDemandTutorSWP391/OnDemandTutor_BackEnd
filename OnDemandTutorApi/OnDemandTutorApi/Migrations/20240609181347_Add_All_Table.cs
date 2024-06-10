using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnDemandTutorApi.Migrations
{
    public partial class Add_All_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Level",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestCategory",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoinManagement",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Coin = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinManagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinManagement_User",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Request",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestCategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_RequestCategory",
                        column: x => x.RequestCategoryId,
                        principalSchema: "dbo",
                        principalTable: "RequestCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Request_User",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tutor",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademicLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnlineStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AverageStar = table.Column<double>(type: "float", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TutorServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TutorServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TutorServiceVideo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LearningMaterialDemo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tutor_User",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Response",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Response", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Response_Request",
                        column: x => x.RequestId,
                        principalSchema: "dbo",
                        principalTable: "Request",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Star = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_Tutor",
                        column: x => x.TutorId,
                        principalSchema: "dbo",
                        principalTable: "Tutor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rating_User",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubjectLevel",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coin = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectLevel_Level",
                        column: x => x.LevelId,
                        principalSchema: "dbo",
                        principalTable: "Level",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubjectLevel_Subject",
                        column: x => x.SubjectId,
                        principalSchema: "dbo",
                        principalTable: "Subject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubjectLevel_Tutor",
                        column: x => x.TutorId,
                        principalSchema: "dbo",
                        principalTable: "Tutor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentJoin",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    SubjectLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentJoin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentJoin_SubjectLevel",
                        column: x => x.SubjectLevelId,
                        principalSchema: "dbo",
                        principalTable: "SubjectLevel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentJoin_User",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Time",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectLevelId = table.Column<int>(type: "int", nullable: false),
                    SlotName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartSlot = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndSlot = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Time", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Time_SubjectLevel",
                        column: x => x.SubjectLevelId,
                        principalSchema: "dbo",
                        principalTable: "SubjectLevel",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CoinManagement_UserId",
                schema: "dbo",
                table: "CoinManagement",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_TutorId",
                schema: "dbo",
                table: "Rating",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_UserId",
                schema: "dbo",
                table: "Rating",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                schema: "dbo",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_RequestCategoryId",
                schema: "dbo",
                table: "Request",
                column: "RequestCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_UserId",
                schema: "dbo",
                table: "Request",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Response_RequestId",
                schema: "dbo",
                table: "Response",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                schema: "dbo",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentJoin_SubjectLevelId",
                schema: "dbo",
                table: "StudentJoin",
                column: "SubjectLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentJoin_UserId",
                schema: "dbo",
                table: "StudentJoin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectLevel_LevelId",
                schema: "dbo",
                table: "SubjectLevel",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectLevel_SubjectId",
                schema: "dbo",
                table: "SubjectLevel",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectLevel_TutorId",
                schema: "dbo",
                table: "SubjectLevel",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Time_SubjectLevelId",
                schema: "dbo",
                table: "Time",
                column: "SubjectLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tutor_UserId",
                schema: "dbo",
                table: "Tutor",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                schema: "dbo",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                schema: "dbo",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "dbo",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinManagement",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Rating",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Response",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RoleClaim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "StudentJoin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Time",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserClaim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserLogin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserToken",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Request",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SubjectLevel",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RequestCategory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Level",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Subject",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Tutor",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");
        }
    }
}
