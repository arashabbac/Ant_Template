using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Services
{
    public class UserService : Infrastructure.BaseHttpClient
    {
        private readonly TokenService _tokenService;
        private readonly NavigationManager _navigationManager;
        private readonly Microsoft.JSInterop.IJSRuntime _jSRuntime;

        public UserService
            (System.Net.Http.HttpClient client,
            Services.TokenService tokenService,
            Microsoft.JSInterop.IJSRuntime jSRuntime,
            Microsoft.AspNetCore.Components.NavigationManager navigationManager)
                : base(client, tokenService, jSRuntime,navigationManager)
        {
            _tokenService = tokenService;
            _navigationManager = navigationManager;
        }

        public async System.Threading.Tasks.Task
            <ViewModels.ViewPagingDataResult<ViewModels.Users.GetAllViewModel>>
            GetAllAsync(ViewModels.Users.GetAllRequestViewModel request)
        {
            var response =
                await PostAsJsonAsync<ViewModels.Users.GetAllRequestViewModel, ViewModels.ViewPagingDataResult<ViewModels.Users.GetAllViewModel>>
                ("Users/GetAll", request, hasAuthorize: true);

            return response.ValueOrDefault;
        }

        public async System.Threading.Tasks.Task<System.Collections.Generic.IList<ViewModels.Users.GetAllViewModel>>
            GetAllAsync()
        {
            var response =
                await GetAsync<System.Collections.Generic.IList<ViewModels.Users.GetAllViewModel>>
                (requestUri: "Users/", hasAuthorize: true);

            return response.ValueOrDefault;
        }

        public async System.Threading.Tasks.Task
            CreateAsync(ViewModels.Users.CreateViewModel model)
        {
            var response =
                await PostAsJsonAsync
                (requestUri: "Users/Create", value: model, hasAuthorize: true);

            if (response.IsSuccess)
            {
                _navigationManager.NavigateTo("/users");
            }
        }

        public async System.Threading.Tasks.Task<ViewModels.Users.UserViewModel>
            GetByIdAsync(System.Guid userid)
        {
            var response =
                await GetAsync<ViewModels.Users.UserViewModel>
                (requestUri: $"Users/GetById/{userid}", hasAuthorize: true);

            return response.ValueOrDefault;
        }

        public async System.Threading.Tasks.Task
            EditAsync(ViewModels.Users.CreateViewModel model)
        {
            var response =
                await PutAsJsonAsync
                (requestUri: "Users", value: model, hasAuthorize: true);

            if (response.IsSuccess)
            {
                _navigationManager.NavigateTo(uri: "/users");
            }
        }

        public async System.Threading.Tasks.Task
            DeleteByIdAsync(System.Guid userId)
        {
            var response =
                await DeleteAsync(requestUri: $"Users/{ userId }", hasAuthorize: true);
        }



    }
}
