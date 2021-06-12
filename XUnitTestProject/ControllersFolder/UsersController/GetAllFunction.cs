using Moq;

namespace XUnitTestProject.ControllersFolder.UsersController
{
    public class GetAllFunction
    {
        private readonly Moq.Mock<Application.Services.IUserService> _userServiceMock;
        private readonly Moq.Mock<Application.Services.INotificationService> _notificationService;
        private readonly Moq.Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor> _httpContextAccessor;
        private readonly Application.Controllers.UsersController _controller;

        public GetAllFunction()
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
            var actual =
                await _controller.GetAll(request: null);

            Xunit.Assert.IsType
                <Microsoft.AspNetCore.Mvc.BadRequestResult>(actual.Result);
        }

        [Xunit.Fact]
        public async System.Threading.Tasks.Task
            Should_Return_OkResult_When_Success_Get()
        {
            var requestMock =
                new Moq.Mock<ViewModels.Users.GetAllRequestViewModel>();

            _userServiceMock.Setup(current => current.GetAllAsync(requestMock.Object))
                .Verifiable();

            var actual = await _controller.GetAll(requestMock.Object);

            Xunit.Assert.IsType
                <Microsoft.AspNetCore.Mvc.OkObjectResult>(actual.Result);
        }
    }

}
