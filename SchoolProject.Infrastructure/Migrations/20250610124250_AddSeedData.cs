using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructor_InsManager",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_InsSubjects_Instructor_InsId",
                table: "InsSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Departments_DID",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Instructor_SupervisorId",
                table: "Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructor",
                table: "Instructor");

            migrationBuilder.RenameTable(
                name: "Instructor",
                newName: "Instructors");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_SupervisorId",
                table: "Instructors",
                newName: "IX_Instructors_SupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_DID",
                table: "Instructors",
                newName: "IX_Instructors_DID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructors",
                table: "Instructors",
                column: "InsId");

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DID", "DNameAr", "DNameEn", "InsManager" },
                values: new object[,]
                {
                    { 1, "علوم الحاسب", "Computer Science", null },
                    { 2, "الرياضيات", "Mathematics", null }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubID", "Period", "SubjectNameAr", "SubjectNameEn" },
                values: new object[,]
                {
                    { 1, 3, "برمجة 1", "Programming 1" },
                    { 2, 4, "قواعد بيانات", "Databases" },
                    { 3, 3, "الجبر الخطي", "Linear Algebra" }
                });

            migrationBuilder.InsertData(
                table: "DepartmentSubjects",
                columns: new[] { "DID", "SubID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "InsId", "Address", "DID", "ENameAr", "ENameEn", "Image", "Position", "Salary", "SupervisorId" },
                values: new object[] { 1, "القاهرة", 1, "أحمد محمد", "Ahmed Mohamed", null, "أستاذ", 5000m, null });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudID", "AddressAr", "AddressEn", "DID", "NameAr", "NameEn", "Phone" },
                values: new object[,]
                {
                    { 1, "القاهرة", "Cairo", 1, "محمد حسن", "Mohamed Hassan", "0123456789" },
                    { 2, "الإسكندرية", "Alexandria", 2, "فاطمة خالد", "Fatima Khaled", "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "InsSubjects",
                columns: new[] { "InsId", "SubId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "InsId", "Address", "DID", "ENameAr", "ENameEn", "Image", "Position", "Salary", "SupervisorId" },
                values: new object[] { 2, "الجيزة", 2, "سارة علي", "Sarah Ali", null, "أستاذ مساعد", 4000m, 1 });

            migrationBuilder.InsertData(
                table: "StudentSubjects",
                columns: new[] { "StudID", "SubID", "Grade" },
                values: new object[,]
                {
                    { 1, 1, 85m },
                    { 1, 2, 90m },
                    { 2, 3, 88m }
                });

            migrationBuilder.InsertData(
                table: "InsSubjects",
                columns: new[] { "InsId", "SubId" },
                values: new object[] { 2, 3 });

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_InsManager",
                table: "Departments",
                column: "InsManager",
                principalTable: "Instructors",
                principalColumn: "InsId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsSubjects_Instructors_InsId",
                table: "InsSubjects",
                column: "InsId",
                principalTable: "Instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Departments_DID",
                table: "Instructors",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Instructors_SupervisorId",
                table: "Instructors",
                column: "SupervisorId",
                principalTable: "Instructors",
                principalColumn: "InsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_InsManager",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_InsSubjects_Instructors_InsId",
                table: "InsSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Departments_DID",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Instructors_SupervisorId",
                table: "Instructors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructors",
                table: "Instructors");

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DID", "SubID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DID", "SubID" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DID", "SubID" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "InsSubjects",
                keyColumns: new[] { "InsId", "SubId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "InsSubjects",
                keyColumns: new[] { "InsId", "SubId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "InsSubjects",
                keyColumns: new[] { "InsId", "SubId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudID", "SubID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudID", "SubID" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudID", "SubID" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "InsId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "InsId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DID",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "Instructors",
                newName: "Instructor");

            migrationBuilder.RenameIndex(
                name: "IX_Instructors_SupervisorId",
                table: "Instructor",
                newName: "IX_Instructor_SupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructors_DID",
                table: "Instructor",
                newName: "IX_Instructor_DID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructor",
                table: "Instructor",
                column: "InsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructor_InsManager",
                table: "Departments",
                column: "InsManager",
                principalTable: "Instructor",
                principalColumn: "InsId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsSubjects_Instructor_InsId",
                table: "InsSubjects",
                column: "InsId",
                principalTable: "Instructor",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Departments_DID",
                table: "Instructor",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Instructor_SupervisorId",
                table: "Instructor",
                column: "SupervisorId",
                principalTable: "Instructor",
                principalColumn: "InsId");
        }
    }
}
