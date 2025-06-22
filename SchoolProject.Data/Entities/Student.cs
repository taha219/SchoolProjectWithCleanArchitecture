using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolProject.Data.Common;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Data.Entities
{
    public class Student : GeneralLocalizableEntity, ISoftDeleteable
    {
        public Student()
        {
            StudentSubjects = new HashSet<StudentSubject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudID { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        [StringLength(200)]
        public string? AddressAr { get; set; }
        [StringLength(200)]
        public string? AddressEn { get; set; }
        [StringLength(200)]
        public string? Phone { get; set; }
        public int? DID { get; set; }

        [ForeignKey("DID")]
        [InverseProperty("Students")]
        public virtual Department? Department { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }


    }
}