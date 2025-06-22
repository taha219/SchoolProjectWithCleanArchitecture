using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolProject.Data.Common;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Data.Entities
{
    public partial class Department : GeneralLocalizableEntity, ISoftDeleteable
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
            Instructors = new HashSet<Instructor>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DID { get; set; }
        [StringLength(200)]
        public string? DNameAr { get; set; }
        [StringLength(200)]
        public string? DNameEn { get; set; }

        public int? InsManager { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Instructor> Instructors { get; set; }

        [ForeignKey("InsManager")]
        [InverseProperty("DepartmentManager")]
        public virtual Instructor? Instructor { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }

    }
}