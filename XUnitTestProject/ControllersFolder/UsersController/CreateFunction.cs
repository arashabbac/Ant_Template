using Moq;

namespace XUnitTestProject.ControllersFolder.UsersController
{
    public class CreateFunction
    {
        private readonly Moq.Mock<Application.Services.IUserService> _userServiceMock;
        private readonly Moq.Mock<Application.Services.INotificationService> _notificationService;
        private readonly Moq.Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor> _httpContextAccessor;
        private readonly Application.Controllers.UsersController _controller;

        public CreateFunction()
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
            var actual = await _controller.Create(null);

            Xunit.Assert.IsType
                <Microsoft.AspNetCore.Mvc.BadRequestResult>
                (actual);
        }

        [Xunit.Fact]
        public async System.Threading.Tasks.Task
            Should_Return_BadRequest_When_ModelState_NotValid()
        {
            _controller.ModelState.AddModelError("key", "error message");

            var actual =
                await _controller.Create(Moq.It.IsAny<ViewModels.Users.CreateViewModel>());

            Xunit.Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>
                (actual);
        }

        [Xunit.Fact]
        public async System.Threading.Tasks.Task
            Should_Return_ConflictObject_When_Username_Exists()
        {
            _userServiceMock.Setup(current => current.GetByUsernameAsync(Moq.It.IsAny<string>()))
                .ReturnsAsync(new Moq.Mock<ViewModels.Users.UserViewModel>().Object);

            var actual =
                await _controller.Create(new Moq.Mock<ViewModels.Users.CreateViewModel>().Object);

            Xunit.Assert.IsType<Microsoft.AspNetCore.Mvc.ConflictObjectResult>
                (actual);

        }

        [Xunit.Fact]
        public async System.Threading.Tasks.Task
            Should_Return_OkResult_When_Success_Insert()
        {
            _userServiceMock.Setup(current => current.InsertByAdminAsync(Moq.It.IsAny<ViewModels.Users.CreateViewModel>()))
                .Verifiable();

            var actual =
                await _controller.Create(new Moq.Mock<ViewModels.Users.CreateViewModel>().Object);

            Xunit.Assert.IsType<Microsoft.AspNetCore.Mvc.OkResult>(actual);
        }

    }
}
