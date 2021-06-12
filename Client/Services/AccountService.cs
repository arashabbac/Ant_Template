using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Net.Http.Json;

namespace Services
{
    public class AccountService : Infrastructure.BaseHttpClient
    {
        private readonly TokenService _tokenService;
        private readonly NavigationManager _navigationManager;
        private readonly Microsoft.JSInterop.IJSRuntime _jSRuntime;
        private readonly CurrieTechnologies.Razor.SweetAlert2.SweetAlertService _swal;

        public AccountService
            (System.Net.Http.HttpClient client,
            Services.TokenService tokenService,
            Microsoft.JSInterop.IJSRuntime jSRuntime,
            CurrieTechnologies.Razor.SweetAlert2.SweetAlertService swal,
            Microsoft.AspNetCore.Components.NavigationManager navigationManager)
                : base(client, tokenService, jSRuntime,navigationManager)
        {
            _tokenService = tokenService;
            _navigationManager = navigationManager;
            _jSRuntime = jSRuntime;
            _swal = swal;
        }

        public bool postLoading = false;
        public bool putLoading = false;

        public async System.Threading.Tasks.Task
            LoginAsync(ViewModels.Users.LoginRequestViewModel login)
        {
            var response =
                await PostAsJsonAsync<ViewModels.Users.LoginRequestViewModel, ViewModels.Users.LoginResponseViewModel>
                (requestUri: $"Accounts/login", value: login);

            if (response.IsSuccess == true)
            {
                await _tokenService.SetAsync(response.ValueOrDefault);
                _navigationManager.NavigateTo(uri: "/users");
            }
        }

        public async System.Threading.Tasks.Task
            RegisterAsync(ViewModels.Users.RegisterViewModel register)
        {
            var response =
                await PostAsJsonAsync
                (requestUri: "Accounts/Register", value: register);

            if (response.IsFailed)
            {
                await _swal.FireAsync(new CurrieTechnologies.Razor.SweetAlert2.SweetAlertOptions
                {
                    Text = $"{response.Errors.Select(c => c.Message).FirstOrDefault()}",
                    Icon = CurrieTechnologies.Razor.SweetAlert2.SweetAlertIcon.Error,
                });
            }
            else if (response.IsSuccess)
            {
                _navigationManager.NavigateTo(uri: "/login");
            }
        }

        public async System.Threading.Tasks.Task
        EditAsync(ViewModels.Account.AccountInformationViewModel viewModel)
        {
            putLoading = true;
            
            var response =
                await PutAsJsonAsync
                (requestUri: "Accounts", value: viewModel, hasAuthorize: true);
            
            putLoading = false;
        }

        public async System.Threading.Tasks.Task
            ChangePasswordAsync(ViewModels.Account.ChangePasswordViewModel viewModel)
        {
            postLoading = true;

            var response =
                await PostAsJsonAsync<ViewModels.Account.ChangePasswordViewModel, ViewModels.FluentResult.FluentResultViewModel>
                (requestUri: "Accounts/ChangePassword", value: viewModel, hasAuthorize: true);

            postLoading = false;
        }
    }
}
