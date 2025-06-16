using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolProject.Data.Entities
{
    [PrimaryKey(nameof(InsId), nameof(SubId))]
    public class Ins_Subject
    {

        public int InsId { get; set; }

        public int SubId { get; set; }
        [ForeignKey(nameof(InsId))]
        [InverseProperty("Ins_Subjects")]
        public Instructor? instructor { get; set; }
        [ForeignKey(nameof(SubId))]
        [InverseProperty("Ins_Subjects")]
        public Subject? Subject { get; set; }

    }
}