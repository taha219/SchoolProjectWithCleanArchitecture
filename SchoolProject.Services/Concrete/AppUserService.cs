using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailsService _emailsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _appDBContext;
        private readonly IUrlHelperFactory _urlHelperFactory;
        public AppUserService(UserManager<AppUser> userManager,
                              IEmailsService emailsService,
                              IHttpContextAccessor httpContextAccessor,
                              AppDbContext appDBContext,
                              IUrlHelperFactory urlHelperFactory)
        {
            _userManager = userManager;
            _emailsService = emailsService;
            _httpContextAccessor = httpContextAccessor;
            _appDBContext = appDBContext;
            _urlHelperFactory = urlHelperFactory;

        }
        public async Task<string> AddUserAsync(AppUser user, string password, string role)
        {
            var trans = await _appDBContext.Database.BeginTransactionAsync();
            try
            {
                var userByName = await _userManager.FindByNameAsync(user.UserName);
                if (userByName != null) return "UserNameExists";

                try
                {
                    var userByEmail = await _userManager.FindByEmailAsync(user.Email);

                }
                catch (Exception ex)
                {
                    return "EmailExists";
                }
                var createResult = _userManager.CreateAsync(user, password);
                if (!createResult.Result.Succeeded)
                {
                    return "CreateUserFailed";
                }
                await _userManager.AddToRoleAsync(user, role);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var httpContext = _httpContextAccessor.HttpContext;

                if (httpContext == null)
                    throw new InvalidOperationException("HttpContext is null. Cannot generate URL.");

                var actionContext = new ActionContext
                {
                    HttpContext = httpContext,
                    RouteData = httpContext.GetRouteData(), // مهم جدًا
                    ActionDescriptor = new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor() // Placeholder لو مش بتستخدم MVC Actions
                };

                var urlHelper = _urlHelperFactory.GetUrlHelper(actionContext);

                var request = httpContext.Request;
                var returnUrl = $"{request.Scheme}://{request.Host}" +
                                urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code });

                var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Link Of Confirmation</a>";


                //$"/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={code}";
                //message or body
                await _emailsService.SendEmail(user.Email, message, "ConFirm Email");

                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }


        }
    }
}
