using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolProject.Data.Entities
{
    [PrimaryKey(nameof(StudID), nameof(SubID))]
    public class StudentSubject
    {

        public int StudID { get; set; }
        public int SubID { get; set; }
        public decimal? Grade { get; set; }

        [ForeignKey("StudID")]
        [InverseProperty("StudentSubjects")]
        public virtual Student? Student { get; set; }

        [ForeignKey("SubID")]
        [InverseProperty("StudentSubjects")]
        public virtual Subjects? Subject { get; set; }

    }
}