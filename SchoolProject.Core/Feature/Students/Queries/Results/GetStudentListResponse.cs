﻿namespace SchoolProject.Core.Feature.Students.Queries.Results
{
    public class GetStudentListResponse
    {
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; }
        public string? DepartmentName { get; set; }
    }
}
