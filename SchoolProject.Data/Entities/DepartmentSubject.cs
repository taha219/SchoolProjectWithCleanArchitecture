using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolProject.Data.Entities
{
    [PrimaryKey(nameof(DID), nameof(SubID))]
    public class DepartmentSubject
    {
        public int DID { get; set; }
        public int SubID { get; set; }

        [ForeignKey("DID")]
        [InverseProperty("DepartmentSubjects")]
        public virtual Department? Department { get; set; }

        [ForeignKey("SubID")]
        [InverseProperty("DepartmentSubjects")]
        public virtual Subjects? Subject { get; set; }
    }
}