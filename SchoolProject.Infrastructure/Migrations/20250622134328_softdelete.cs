using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class softdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Students",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Students",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

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
        }
    }
}
