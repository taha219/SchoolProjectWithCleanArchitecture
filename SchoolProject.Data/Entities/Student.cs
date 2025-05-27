using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolProject.Data.Common;

namespace SchoolProject.Data.Entities
{
    public class Student : GeneralLocalizableEntity
    {
        [Key]
        [Column("StudentId")]
        public int StudentId { get; set; }

        [StringLength(200)]

        public string NameAr { get; set; }
        [StringLength(200)]

        public string NameEn { get; set; }

        [StringLength(300)]
        public string AddressAr { get; set; }
        [StringLength(300)]
        public string AddressEn { get; set; }

        [StringLength(200)]
        public string Phone { get; set; }

        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        [InverseProperty("Students")]
        public virtual Department Department { get; set; }
    }
}
