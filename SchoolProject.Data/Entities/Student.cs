using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [StringLength(200)]

        public string Name { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(200)]
        public string Phone { get; set; }

        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        [InverseProperty("Students")]
        public virtual Department Department { get; set; }
    }
}
