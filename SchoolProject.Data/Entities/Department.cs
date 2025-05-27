using System.ComponentModel.DataAnnotations;
using SchoolProject.Data.Common;

namespace SchoolProject.Data.Entities
{
    public class Department : GeneralLocalizableEntity
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
        }
        [Key]
        public int DID { get; set; }

        [StringLength(100)]
        public string DNameAr { get; set; }
        [StringLength(100)]
        public string DNameEn { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
    }
}
