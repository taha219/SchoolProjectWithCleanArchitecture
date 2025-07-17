using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<DepartmentSubject> DepartmentSubjects { get; set; }
        public DbSet<Ins_Subject> InsSubjects { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        public DbSet<UserOTP> UserOtps { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table name for DepartmentSubject
            modelBuilder.Entity<DepartmentSubject>().ToTable("DepartmentSubjects");

            // Configure composite key (redundant with [PrimaryKey], but safe to include)
            modelBuilder.Entity<DepartmentSubject>()
                .HasKey(ds => new { ds.DID, ds.SubID });

            // Configure relationships
            modelBuilder.Entity<DepartmentSubject>()
                .HasOne(ds => ds.Department)
                .WithMany(d => d.DepartmentSubjects)
                .HasForeignKey(ds => ds.DID);

            modelBuilder.Entity<DepartmentSubject>()
                .HasOne(ds => ds.Subject)
                .WithMany(s => s.DepartmentSubjects)
                .HasForeignKey(ds => ds.SubID);

            modelBuilder.Entity<Ins_Subject>().ToTable("InsSubjects");
            modelBuilder.Entity<Ins_Subject>()
                .HasKey(ins => new { ins.InsId, ins.SubId });
            modelBuilder.Entity<Ins_Subject>()
                .HasOne(ins => ins.instructor)
                .WithMany(i => i.Ins_Subjects)
                .HasForeignKey(ins => ins.InsId);
            modelBuilder.Entity<Ins_Subject>()
                .HasOne(ins => ins.Subject)
                .WithMany(s => s.Ins_Subjects)
                .HasForeignKey(ins => ins.SubId);
            modelBuilder.Entity<UserOTP>()
                .HasOne(otp => otp.User)
                .WithMany(user => user.Otps)
                .HasForeignKey(otp => otp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            #region seed data
            //// Departments
            //modelBuilder.Entity<Department>().HasData(
            //    new Department { DID = 1, DNameAr = "علوم الحاسوب", DNameEn = "Computer Science" },
            //    new Department { DID = 2, DNameAr = "هندسة البرمجيات", DNameEn = "Software Engineering" },
            //    new Department { DID = 3, DNameAr = "نظم المعلومات", DNameEn = "Information Systems" },
            //    new Department { DID = 4, DNameAr = "الذكاء الاصطناعي", DNameEn = "Artificial Intelligence" },
            //    new Department { DID = 5, DNameAr = "الأمن السيبراني", DNameEn = "Cyber Security" }
            //);

            //// Subjects
            //modelBuilder.Entity<Subject>().HasData(
            //    new Subject { SubID = 1, SubjectNameAr = "برمجة", SubjectNameEn = "Programming", Period = 60 },
            //    new Subject { SubID = 2, SubjectNameAr = "قواعد بيانات", SubjectNameEn = "Databases", Period = 45 },
            //    new Subject { SubID = 3, SubjectNameAr = "شبكات", SubjectNameEn = "Networks", Period = 40 },
            //    new Subject { SubID = 4, SubjectNameAr = "ذكاء صناعي", SubjectNameEn = "AI", Period = 50 },
            //    new Subject { SubID = 5, SubjectNameAr = "أنظمة تشغيل", SubjectNameEn = "Operating Systems", Period = 55 }
            //);

            //// Instructors
            //modelBuilder.Entity<Instructor>().HasData(
            //    new Instructor { InsId = 1, ENameAr = "د. محمد", ENameEn = "Dr. Mohamed", DID = 1 },
            //    new Instructor { InsId = 2, ENameAr = "د. سارة", ENameEn = "Dr. Sarah", DID = 2 },
            //    new Instructor { InsId = 3, ENameAr = "د. أحمد", ENameEn = "Dr. Ahmed", DID = 3 },
            //    new Instructor { InsId = 4, ENameAr = "د. فاطمة", ENameEn = "Dr. Fatma", DID = 4 },
            //    new Instructor { InsId = 5, ENameAr = "د. عمرو", ENameEn = "Dr. Amr", DID = 5 }
            //);

            // Students
            //modelBuilder.Entity<Student>().HasData(
            //    new Student { StudID = 1, NameAr = "علي", NameEn = "Ali", DID = 1 },
            //    new Student { StudID = 2, NameAr = "مريم", NameEn = "Mariam", DID = 2 },
            //    new Student { StudID = 3, NameAr = "يوسف", NameEn = "Youssef", DID = 3 },
            //    new Student { StudID = 4, NameAr = "سلمى", NameEn = "Salma", DID = 4 },
            //    new Student { StudID = 5, NameAr = "حسن", NameEn = "Hassan", DID = 5 }
            //);

            //// DepartmentSubjects
            //modelBuilder.Entity<DepartmentSubject>().HasData(
            //    new DepartmentSubject { DID = 1, SubID = 1 },
            //    new DepartmentSubject { DID = 2, SubID = 2 },
            //    new DepartmentSubject { DID = 3, SubID = 3 },
            //    new DepartmentSubject { DID = 4, SubID = 4 },
            //    new DepartmentSubject { DID = 5, SubID = 5 }
            //);

            //// Ins_Subject
            //modelBuilder.Entity<Ins_Subject>().HasData(
            //    new Ins_Subject { InsId = 1, SubId = 1 },
            //    new Ins_Subject { InsId = 2, SubId = 2 },
            //    new Ins_Subject { InsId = 3, SubId = 3 },
            //    new Ins_Subject { InsId = 4, SubId = 4 },
            //    new Ins_Subject { InsId = 5, SubId = 5 }
            //);

            //// StudentSubjects
            //modelBuilder.Entity<StudentSubject>().HasData(
            //    new StudentSubject { StudID = 1, SubID = 1, Grade = 85 },
            //    new StudentSubject { StudID = 2, SubID = 2, Grade = 90 },
            //    new StudentSubject { StudID = 3, SubID = 3, Grade = 88 },
            //    new StudentSubject { StudID = 4, SubID = 4, Grade = 91 },
            //    new StudentSubject { StudID = 5, SubID = 5, Grade = 87 }
            //);
            #endregion
        }
    }
}