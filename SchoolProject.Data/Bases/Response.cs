using System.Net;

namespace SchoolProject.Core.Bases
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; } = new List<string>();
        public HttpStatusCode StatusCode { get; set; }
    }

}
