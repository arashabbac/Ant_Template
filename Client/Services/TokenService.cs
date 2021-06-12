using Microsoft.JSInterop;

namespace Services
{
    public class TokenService
    {
        private readonly IJSRuntime _jSRuntime;
        private readonly Microsoft.AspNetCore.Components.NavigationManager _navigationManager;

        public TokenService
            (IJSRuntime jSRuntime,
            Microsoft.AspNetCore.Components.NavigationManager navigationManager)
        {
            _jSRuntime = jSRuntime;
            _navigationManager = navigationManager;
        }

        public async System.Threading.Tasks.Task
            SetAsync(ViewModels.Users.LoginResponseViewModel login)
        {
            var json =
                Newtonsoft.Json.JsonConvert.SerializeObject(login);

            await _jSRuntime.InvokeVoidAsync
                ("localStorage.setItem", "user", json)
                .ConfigureAwait(false);

            _navigationManager.NavigateTo("/");
        }

        public async System.Threading.Tasks.Task<ViewModels.Users.LoginResponseViewModel>
            GetUserAsync()
        {
            var json =
                await _jSRuntime.InvokeAsync<string>
                ("localStorage.getItem", "user")
                .ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(json))
            {
                _navigationManager.NavigateTo(uri: "/login", forceLoad: false);
                return null;
            }

            var response =
                Newtonsoft.Json.JsonConvert.DeserializeObject<ViewModels.Users.LoginResponseViewModel>
                (json);

            System.Console.WriteLine(response.Type);

            return response;
        }

        public async System.Threading.Tasks.Task RemoveAsync()
        {
            await _jSRuntime
                .InvokeVoidAsync("localStorage.removeItem", "user")
                .ConfigureAwait(false);

            _navigationManager.NavigateTo("/login");
        }
    }
}
