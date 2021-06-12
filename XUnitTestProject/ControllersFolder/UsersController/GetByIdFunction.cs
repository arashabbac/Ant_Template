using Moq;

namespace XUnitTestProject.ControllersFolder.UsersController
{
    public class GetByIdFunction
    {
        private readonly Moq.Mock<Application.Services.IUserService> _userServiceMock;
        private readonly Moq.Mock<Application.Services.INotificationService> _notificationService;
        private readonly Moq.Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor> _httpContextAccessor;
        private readonly Application.Controllers.UsersController _controller;

        public GetByIdFunction()
        {
            _notificationService = new Moq.Mock<Application.Services.INotificationService>();
            _userServiceMock = new Moq.Mock<Application.Services.IUserService>();
            _httpContextAccessor = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();

            _controller = new Application.Controllers.UsersController
                (_userServiceMock.Object, _notificationService.Object, _httpContextAccessor.Object);
            _controller.AuthenticatedUser = new Moq.Mock<ViewModels.Users.UserViewModel>().Object;
        }

        [Xunit.Fact]
        public async System.Threading.Tasks.Task
            Should_Return_BadRequest_When_Parameter_Null()
        {
            var actual = await _controller.GetById(null);

            Xunit.Assert.IsType
                <Microsoft.AspNetCore.Mvc.BadRequestResult>
                (actual.Result);
        }

        [Xunit.Fact]
        public async System.Threading.Tasks.Task
            Should_Return_NotFoundObjectResult_When_NotFound()
        {
            _userServiceMock.Setup(current => current.GetByIdAsync(System.Guid.NewGuid()))
                .ReturnsAsync(() => null);

            var actual =
                await _controller.GetById(System.Guid.NewGuid());

            Xunit.Assert.IsType
                <Microsoft.AspNetCore.Mvc.NotFoundResult>
                (actual.Result);

        }

        [Xunit.Fact]
        public async System.Threading.Tasks.Task
            Should_Return_OKResult_When_GetUser_Success()
        {
            var userId = System.Guid.NewGuid();

            _userServiceMock.Setup(current => current.GetByIdAsync(userId))
                .ReturnsAsync(new Moq.Mock<ViewModels.Users.UserViewModel>().Object);

            var actual =
                await _controller.GetById(userId);

            Xunit.Assert.IsType
                <Microsoft.AspNetCore.Mvc.OkObjectResult>
                (actual.Result);

        }
    }
}
