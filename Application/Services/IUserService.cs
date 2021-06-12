namespace Application.Services
{
    public interface IUserService
    {
        System.Threading.Tasks.Task<FluentResults.Result<ViewModels.Users.UserViewModel>>
            GetByIdAsync(System.Guid? id);

        System.Threading.Tasks.Task<ViewModels.Users.UserViewModel>
            GetByUsernameAsync(string username);

        System.Threading.Tasks.Task<FluentResults.Result>
            InsertAsync(ViewModels.Users.CreateViewModel viewModel);

        System.Threading.Tasks.Task<FluentResults.Result>
            UpdateAsync(ViewModels.Users.CreateViewModel viewModel);

        System.Threading.Tasks.Task
            <FluentResults.Result<ViewModels.ViewPagingDataResult<ViewModels.Users.GetAllViewModel>>>
            GetAllAsync(ViewModels.Users.GetAllRequestViewModel request);

        System.Threading.Tasks.Task<FluentResults.Result>
            DeleteByIdAsync(System.Guid? id);
    }
}
