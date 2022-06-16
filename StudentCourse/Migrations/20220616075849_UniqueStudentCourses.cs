using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCourse.Migrations
{
    public partial class UniqueStudentCourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentCourses_CourseId",
                table: "StudentCourses");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_CourseId_StudentId",
                table: "StudentCourses",
                columns: new[] { "CourseId", "StudentId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentCourses_CourseId_StudentId",
                table: "StudentCourses");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_CourseId",
                table: "StudentCourses",
                column: "CourseId");
        }
    }
}
