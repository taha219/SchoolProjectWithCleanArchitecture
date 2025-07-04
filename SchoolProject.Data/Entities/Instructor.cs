﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolProject.Data.Common;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Data.Entities
{
    public class Instructor : GeneralLocalizableEntity, ISoftDeleteable
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            Ins_Subjects = new HashSet<Ins_Subject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InsId { get; set; }
        public string? ENameAr { get; set; }
        public string? ENameEn { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public int? SupervisorId { get; set; }
        public decimal? Salary { get; set; }
        public string? Image { get; set; }
        public int DID { get; set; }
        [ForeignKey(nameof(DID))]
        [InverseProperty("Instructors")]
        public Department? Department { get; set; }

        [InverseProperty("Instructor")]
        public Department? DepartmentManager { get; set; }


        [ForeignKey(nameof(SupervisorId))]
        [InverseProperty("Instructors")]
        public Instructor? Supervisor { get; set; }
        [InverseProperty("Supervisor")]
        public virtual ICollection<Instructor> Instructors { get; set; }

        [InverseProperty("instructor")]
        public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
    }

}