namespace Application.Controllers
{
    [Microsoft.AspNetCore.Mvc.ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class UsersController : Infrastructure.ControllerBase
    {
        private readonly Services.IUserService _userService;

        public UsersController
            (Services.IUserService userService,
            Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            AuthenticatedUser = httpContextAccessor.HttpContext?.Items["User"] as ViewModels.Users.UserViewModel;
        }

        public ViewModels.Users.UserViewModel AuthenticatedUser { get; set; }

        /// <summary>  
        /// Get user by id
        /// </summary>  
        /// <param name="userId">user id to get user</param>  
        /// <returns></returns>  
        /// <response code="200">Returned if the user is exists</response>  
        [Microsoft.AspNetCore.Mvc.HttpGet
            (template: "GetById/{userId?}")]

        [Infrastructure.Attributes.Authorize]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result<ViewModels.Users.UserViewModel>),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

        public async System.Threading.Tasks.Task
            <Microsoft.AspNetCore.Mvc.IActionResult>
            GetById(System.Guid? userId)
        {
            var user = await _userService.GetByIdAsync(userId.Value);
            return FluentResult(user);
        }

        /// <summary>  
        /// Insert new user
        /// </summary>  
        /// <param name="user">viewModel to create a new user</param>  
        /// <returns></returns>  
        /// <response code="200">Returned if the user was created</response>  
        /// <response code="400">Returned if the viewModel is null or is not valid</response>  
        /// <response code="409">Returned when the user is exists</response>
        [Microsoft.AspNetCore.Mvc.HttpPost(template: "Create")]
        [Infrastructure.Attributes.Authorize
            (userType: Models.Enums.UserType.SuperUser)]
        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
        (Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [Microsoft.AspNetCore.Mvc.ProducesResponseType
        (Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict)]
        public async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult>
            Create(ViewModels.Users.CreateViewModel user)
        {
            if (ModelState.IsValid == false)
            {
                return ModelStateResult();
            }

            var result = await _userService.InsertAsync(user);
            return FluentResult(result);
        }

        /// <summary>  
        /// Edit user
        /// </summary>  
        /// <param name="user">Model to edit user</param>  
        /// <returns></returns>  
        /// <response code="200">Returned if the user was updated</response>  
        [Microsoft.AspNetCore.Mvc.HttpPut]

        [Infrastructure.Attributes.Authorize
            (userType: Models.Enums.UserType.SuperUser)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

        public async System.Threading.Tasks.Task
            <Microsoft.AspNetCore.Mvc.IActionResult>
            Edit(ViewModels.Users.CreateViewModel user)
        {
            if (ModelState.IsValid == false)
            {
                return ModelStateResult();
            }

            var result = await _userService.UpdateAsync(user);
            return FluentResult(result);
        }

        /// <summary>  
        /// Get all users
        /// </summary>  
        /// <param name="request">Model of ViewDataRequest</param>  
        /// <returns></returns>  
        /// <response code="200">Returned if get users success</response>
        [Microsoft.AspNetCore.Mvc.HttpPost
            (template: "GetAll")]

        [Infrastructure.Attributes.Authorize
            (userType: Models.Enums.UserType.User)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result<ViewModels.ViewPagingDataResult<ViewModels.Users.GetAllViewModel>>),
             statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result),
             statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

        public async System.Threading.Tasks.Task
            <Microsoft.AspNetCore.Mvc.IActionResult>
            GetAll(ViewModels.Users.GetAllRequestViewModel request)
        {
            var users = await _userService.GetAllAsync(request);
            return FluentResult(users);
        }

        /// <summary>  
        /// Delete user by id
        /// </summary>  
        /// <param name="userId">user id to get user</param>  
        /// <returns></returns>  
        /// <response code="200">Returned if the user is deleted</response>  
        [Microsoft.AspNetCore.Mvc.HttpDelete
            (template: "{userId?}")]

        [Infrastructure.Attributes.Authorize]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result<ViewModels.Users.UserViewModel>),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(FluentResults.Result),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

        public async System.Threading.Tasks.Task
            <Microsoft.AspNetCore.Mvc.IActionResult>
            Delete(System.Guid? userId)
        {
            var user = await _userService.DeleteByIdAsync(userId.Value);
            return FluentResult(user);
        }
    }
}
