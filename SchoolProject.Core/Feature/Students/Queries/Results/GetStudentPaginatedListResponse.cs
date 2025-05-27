namespace SchoolProject.Core.Feature.Students.Queries.Results
{
    public class GetStudentPaginatedListResponse
    {
        public GetStudentPaginatedListResponse(int id, string? name, string address, string? phone, string? depatname)
        {
            StudentId = id;
            Name = name;
            Address = address;
            Phone = phone;
            DepartmentName = depatname;
        }
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? DepartmentName { get; set; }
    }
}
