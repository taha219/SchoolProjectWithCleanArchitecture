using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;

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

        }
    }
}