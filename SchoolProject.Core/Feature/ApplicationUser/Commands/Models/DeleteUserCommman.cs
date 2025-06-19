using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.ApplicationUser.Commands.Models
{
    public class DeleteUserCommman : IRequest<ApiResponse<string>>
    {
        public string Id { get; set; }
        public DeleteUserCommman(string id)
        {
            Id = id;
        }
    }
}
