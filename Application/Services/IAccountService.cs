namespace Application.Services
{
    public interface IAccountService
    {
        System.Threading.Tasks.Task<FluentResults.Result>
            RegisterAsync(ViewModels.Users.RegisterViewModel viewModel);

        System.Threading.Tasks.Task<FluentResults.Result<ViewModels.Users.LoginResponseViewModel>>
            LoginAsync(ViewModels.Users.LoginRequestViewModel viewModel);


        System.Threading.Tasks.Task<FluentResults.Result>
            ChangePasswordAsync(ViewModels.Account.ChangePasswordViewModel user);

        System.Threading.Tasks.Task<FluentResults.Result>
            UpdateUserInfoAsync(ViewModels.Account.AccountInformationViewModel viewModel);
    }
}
