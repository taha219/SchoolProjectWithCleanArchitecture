using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DID", "DNameAr", "DNameEn", "InsManager" },
                values: new object[,]
                {
                    { 1, "علوم الحاسوب", "Computer Science", null },
                    { 2, "هندسة البرمجيات", "Software Engineering", null },
                    { 3, "نظم المعلومات", "Information Systems", null },
                    { 4, "الذكاء الاصطناعي", "Artificial Intelligence", null },
                    { 5, "الأمن السيبراني", "Cyber Security", null }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubID", "Period", "SubjectNameAr", "SubjectNameEn" },
                values: new object[,]
                {
                    { 1, 60, "برمجة", "Programming" },
                    { 2, 45, "قواعد بيانات", "Databases" },
                    { 3, 40, "شبكات", "Networks" },
                    { 4, 50, "ذكاء صناعي", "AI" },
                    { 5, 55, "أنظمة تشغيل", "Operating Systems" }
                });

            migrationBuilder.InsertData(
                table: "DepartmentSubjects",
                columns: new[] { "DID", "SubID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "InsId", "Address", "DID", "ENameAr", "ENameEn", "Image", "Position", "Salary", "SupervisorId" },
                values: new object[,]
                {
                    { 1, null, 1, "د. محمد", "Dr. Mohamed", null, null, null, null },
                    { 2, null, 2, "د. سارة", "Dr. Sarah", null, null, null, null },
                    { 3, null, 3, "د. أحمد", "Dr. Ahmed", null, null, null, null },
                    { 4, null, 4, "د. فاطمة", "Dr. Fatma", null, null, null, null },
                    { 5, null, 5, "د. عمرو", "Dr. Amr", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudID", "AddressAr", "AddressEn", "DID", "DateDeleted", "IsDeleted", "NameAr", "NameEn", "Phone" },
                values: new object[,]
                {
                    { 1, null, null, 1, null, false, "علي", "Ali", null },
                    { 2, null, null, 2, null, false, "مريم", "Mariam", null },
                    { 3, null, null, 3, null, false, "يوسف", "Youssef", null },
                    { 4, null, null, 4, null, false, "سلمى", "Salma", null },
                    { 5, null, null, 5, null, false, "حسن", "Hassan", null }
                });

            migrationBuilder.InsertData(
                table: "InsSubjects",
                columns: new[] { "InsId", "SubId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "StudentSubjects",
                columns: new[] { "StudID", "SubID", "Grade" },
                values: new object[,]
                {
                    { 1, 1, 85m },
                    { 2, 2, 90m },
                    { 3, 3, 88m },
                    { 4, 4, 91m },
                    { 5, 5, 87m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DID", "SubID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DID", "SubID" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DID", "SubID" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DID", "SubID" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DID", "SubID" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "InsSubjects",
                keyColumns: new[] { "InsId", "SubId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "InsSubjects",
                keyColumns: new[] { "InsId", "SubId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "InsSubjects",
                keyColumns: new[] { "InsId", "SubId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "InsSubjects",
                keyColumns: new[] { "InsId", "SubId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "InsSubjects",
                keyColumns: new[] { "InsId", "SubId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudID", "SubID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudID", "SubID" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudID", "SubID" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudID", "SubID" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudID", "SubID" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "InsId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "InsId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "InsId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "InsId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "InsId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudID",
                keyValue: 5);

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
                table: "Subjects",
                keyColumn: "SubID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DID",
                keyValue: 5);
        }
    }
}
