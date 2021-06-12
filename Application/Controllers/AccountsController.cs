namespace Application.Controllers
{
    [Microsoft.AspNetCore.Mvc.ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class AccountsController : Infrastructure.ControllerBase
    {
        private readonly Services.IAccountService _accountService;

        public AccountsController
            (Services.IAccountService accountService,
            Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor) : base()
        {
            _accountService = accountService;
            AuthenticatedUser = httpContextAccessor.HttpContext?.Items["User"] as ViewModels.Users.UserViewModel;
        }

        public ViewModels.Users.UserViewModel AuthenticatedUser { get; set; }

        /// <summary>  
        /// Register new user
        /// </summary>  
        /// <param name="user">Model to add a new user</param>  
        /// <returns></returns>  
        /// <response code="200">Returned if the user was created</response>  
        [Microsoft.AspNetCore.Mvc.HttpPost
            (template: "Register")]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

        public async System.Threading.Tasks.Task
            <Microsoft.AspNetCore.Mvc.IActionResult>
            Register(ViewModels.Users.RegisterViewModel user)
        {
            if (ModelState.IsValid == false)
            {
                return ModelStateResult();
            }

            var result = await _accountService.RegisterAsync(user);
            return FluentResult(result);
        }

        /// <summary>  
        /// Login user
        /// </summary>  
        /// <param name="login">Model to login user</param>  
        /// <returns></returns>  
        /// <response code="200">Returned login user</response>  
        [Microsoft.AspNetCore.Mvc.HttpPost
            (template: "Login")]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result<ViewModels.Users.LoginResponseViewModel>),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

        public async System.Threading.Tasks.Task
            <Microsoft.AspNetCore.Mvc.IActionResult>
            Login(ViewModels.Users.LoginRequestViewModel login)
        {
            if (ModelState.IsValid == false)
            {
                return ModelStateResult();
            }

            var result = await _accountService.LoginAsync(login);
            return FluentResult(result);
        }

        /// <summary>  
        /// Change password
        /// </summary>  
        /// <param name="user">viewModel to change password</param>  
        /// <returns></returns>  
        /// <response code="200">Returned if the password was updated</response>  
        [Microsoft.AspNetCore.Mvc.HttpPost
            (template: "ChangePassword")]

        [Infrastructure.Attributes.Authorize
            (userType: Models.Enums.UserType.User)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

        public async System.Threading.Tasks.Task
            <Microsoft.AspNetCore.Mvc.IActionResult>
            ChangePassword(ViewModels.Account.ChangePasswordViewModel user)
        {
            if (ModelState.IsValid == false)
            {
                return ModelStateResult();
            }

            user.Id = AuthenticatedUser.Id;
            user.Username = AuthenticatedUser.Username;

            var result = await _accountService.ChangePasswordAsync(user);
            return FluentResult(result);
        }

        /// <summary>  
        /// Edit user
        /// </summary>  
        /// <param name="user">viewModel to edit user</param>  
        /// <returns></returns>  
        /// <response code="200">Returned if the user was updated</response>  
        [Microsoft.AspNetCore.Mvc.HttpPut]

        [Infrastructure.Attributes.Authorize
            (userType: Models.Enums.UserType.User)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

        public async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult>
            Edit(ViewModels.Account.AccountInformationViewModel user)
        {
            if (ModelState.IsValid == false)
            {
                return ModelStateResult();
            }

            user.Id = AuthenticatedUser.Id;
            user.Username = AuthenticatedUser.Username;

            var result = await _accountService.UpdateUserInfoAsync(user);
            return FluentResult(result);
        }
    }
}
