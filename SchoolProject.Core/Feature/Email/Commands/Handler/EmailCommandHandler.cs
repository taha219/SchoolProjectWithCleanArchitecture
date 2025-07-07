using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Email.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.Email.Commands.Handler
{
    public class EmailCommandHandler : IRequestHandler<SendEmailCommand, ApiResponse<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IEmailsService _emailsService;

        public EmailCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IEmailsService emailsService)
        {
            _stringLocalizer = stringLocalizer;
            _emailsService = emailsService;
        }

        public Task<ApiResponse<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {

        }
    }
}
