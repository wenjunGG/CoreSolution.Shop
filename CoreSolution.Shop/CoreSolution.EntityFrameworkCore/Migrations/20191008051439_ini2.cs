using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreSolution.EntityFrameworkCore.Migrations
{
    public partial class ini2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUserId = table.Column<Guid>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    UpdateDateTime = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 2000, nullable: true),
                    UserName = table.Column<string>(nullable: false),
                    RealName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    IsEmailConfirmed = table.Column<bool>(nullable: false),
                    PhoneNum = table.Column<string>(nullable: false),
                    IsPhoneNumConfirmed = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    UserType = table.Column<int>(nullable: false),
                    Sex = table.Column<bool>(nullable: false),
                    Picture = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
