using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method)]
    public class AuthorizeAttribute :
        System.Attribute, Microsoft.AspNetCore.Mvc.Filters.IAsyncAuthorizationFilter
    {
        private readonly Models.Enums.UserType? _userType;
        private Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext _context;

        public AuthorizeAttribute() : base()
        {

        }

        public AuthorizeAttribute(Models.Enums.UserType userType) : base()
        {
            _userType = userType;
        }

        public async System.Threading.Tasks.Task OnAuthorizationAsync
            (Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext context)
        {
            _context = context;

            ViewModels.Users.UserViewModel user =
                _context.HttpContext.Items["User"] as ViewModels.Users.UserViewModel;

            if (user == null)
            {
                // Not Logged in

                var result =
                    new FluentResults.Result();

                result.WithError(Resources.ErrorMessages.ErrorMessage406);

                _context.Result =
                    new Microsoft.AspNetCore.Mvc.JsonResult(result);
            }
            else if (_userType.HasValue)
            {
                var userService =
                    _context.HttpContext.RequestServices.GetRequiredService<Application.Services.IUserService>();

                var foundedUser =
                    await userService
                    .GetByIdAsync(user.Id);

                if (foundedUser.ValueOrDefault != null && _userType.Value != Models.Enums.UserType.User)
                {
                    checkUserTypeAsync(foundedUser.ValueOrDefault);
                }
            }
        }

        private void checkUserTypeAsync
            (ViewModels.Users.UserViewModel foundedUser)
        {
            if ((int)foundedUser.Type < (int)_userType.Value)
            {
                // Not Access in
                var result =
                    new FluentResults.Result();

                result.WithError(Resources.ErrorMessages.ErrorMessage406);

                _context.Result =
                    new Microsoft.AspNetCore.Mvc.JsonResult(result);
            }
        }
    }
}
