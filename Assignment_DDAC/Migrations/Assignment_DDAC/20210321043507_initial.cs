using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment_DDAC.Migrations.Assignment_DDAC
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseAdmin",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    CourseName = table.Column<string>(nullable: true),
                    UniversityName = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseAdmin", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Enquiry",
                columns: table => new
                {
                    CustomerName = table.Column<string>(nullable: false),
                    CustomerEmail = table.Column<string>(nullable: true),
                    UniversityName = table.Column<string>(nullable: true),
                    CourseName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enquiry", x => x.CustomerName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseAdmin");

            migrationBuilder.DropTable(
                name: "Enquiry");
        }
    }
}
