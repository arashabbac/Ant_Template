namespace XUnitTestProject.ControllersFolder.UsersController
{
    public class EditFunction
    {
        private readonly Moq.Mock<Application.Services.IUserService> _userServiceMock;
        private readonly Moq.Mock<Application.Services.INotificationService> _notificationService;
        private readonly Moq.Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor> _httpContextAccessor;
        private readonly Application.Controllers.UsersController _controller;

        public EditFunction()
        {
            _notificationService = new Moq.Mock<Application.Services.INotificationService>();
            _userServiceMock = new Moq.Mock<Application.Services.IUserService>();
            _httpContextAccessor = new Moq.Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();

            _controller = new Application.Controllers.UsersController
                (_userServiceMock.Object, _notificationService.Object, _httpContextAccessor.Object);
            _controller.AuthenticatedUser = new Moq.Mock<ViewModels.Users.UserViewModel>().Object;
        }

        [Xunit.Fact]
        public async System.Threading.Tasks.Task
            Should_Return_BadRequest_When_Parameter_Null()
        {
            var actual = await _controller.Edit(null);

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
                await _controller.Edit(Moq.It.IsAny<ViewModels.Account.AccountInformationViewModel>());

            Xunit.Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>
                (actual);
        }

        [Xunit.Fact]
        public async System.Threading.Tasks.Task
            Should_Return_OkResult_When_Success_Edit()
        {
            _userServiceMock.Setup(current => current.UpdateAsync(Moq.It.IsAny<ViewModels.Users.CreateViewModel>()))
                .Verifiable();

            var actual =
                await _controller.Edit(new Moq.Mock<ViewModels.Account.AccountInformationViewModel>().Object);

            Xunit.Assert.IsType<Microsoft.AspNetCore.Mvc.OkResult>(actual);
        }
    }
}
