using Microsoft.EntityFrameworkCore.Migrations;

namespace Course_Management_System_API.Migrations
{
    public partial class RemoveStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Students_StudentId1",
                table: "StudentCourses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourses_StudentId1",
                table: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "StudentCourses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId1",
                table: "StudentCourses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "bob@email.com", "Bob", "secret" },
                    { 2, "jimmy@email.com", "Jimmy", "secret" },
                    { 3, "sarah@email.com", "Sarah", "secret" },
                    { 4, "jessica@email.com", "Jessica", "secret" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_StudentId1",
                table: "StudentCourses",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Students_StudentId1",
                table: "StudentCourses",
                column: "StudentId1",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
