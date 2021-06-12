using Microsoft.JSInterop;
using System.Linq;
using System.Net.Http.Json;

namespace Infrastructure
{
    public class BaseHttpClient
    {
        private readonly System.Net.Http.HttpClient _client;
        private readonly Services.TokenService _tokenService;
        private readonly Microsoft.JSInterop.IJSRuntime _jsRuntime;
        private readonly Microsoft.AspNetCore.Components.NavigationManager _navigationManager;

        public BaseHttpClient
            (System.Net.Http.HttpClient client,
            Services.TokenService tokenService,
            Microsoft.JSInterop.IJSRuntime jSRuntime,
            Microsoft.AspNetCore.Components.NavigationManager navigationManager)
        {
            _client = client;
            _tokenService = tokenService;
            _navigationManager = navigationManager;
            _jsRuntime = jSRuntime;
        }

        public bool Loading { get; set; } = false;

        public async System.Threading.Tasks.Task<ViewModels.FluentResult.FluentResultViewModel>
            PostAsJsonAsync<TValue>(string requestUri, TValue value, bool hasAuthorize = false)
        {
            Loading = true;

            if (hasAuthorize)
            {
                await setAuthenticationHeaderAsync();
            }

            var response = await _client.PostAsJsonAsync(requestUri, value);

            var result =
                await response.Content.ReadFromJsonAsync
                <ViewModels.FluentResult.FluentResultViewModel>();

            if (result.IsFailed)
            {
                foreach (var error in result.Errors)
                {
                    await _jsRuntime.InvokeVoidAsync("showDangerToast", error.Message);
                }
            }
            else
            {
                foreach (var success in result.Successes)
                {
                    await _jsRuntime.InvokeVoidAsync("showSuccessToast", success.Message);
                }

            }

            Loading = false;
            return result;
        }

        public async System.Threading.Tasks.Task<ViewModels.FluentResult.FluentResultViewModel>
            PostAsync(string requestUri, System.Net.Http.HttpContent httpContent = null, bool hasAuthorize = false)
        {
            Loading = true;

            if (hasAuthorize)
            {
                await setAuthenticationHeaderAsync();
            }

            var response = await _client.PostAsync(requestUri, httpContent);

            var result =
                await response.Content.ReadFromJsonAsync
                <ViewModels.FluentResult.FluentResultViewModel>();

            if (result.IsFailed)
            {
                foreach (var error in result.Errors)
                {
                    await _jsRuntime.InvokeVoidAsync("showDangerToast", error.Message);
                }
            }
            else
            {
                foreach (var success in result.Successes)
                {
                    await _jsRuntime.InvokeVoidAsync("showSuccessToast", success.Message);
                }
            }

            Loading = false;
            return result;
        }

        public async System.Threading.Tasks.Task<ViewModels.FluentResult.FluentResultViewModel<TResponse>>
        PostAsJsonAsync<TValue, TResponse>(string requestUri, TValue value, bool hasAuthorize = false)
        {
            Loading = true;

            if (hasAuthorize)
            {
                await setAuthenticationHeaderAsync();
            }

            var response = await _client.PostAsJsonAsync(requestUri, value);

            var result =
                await response.Content.ReadFromJsonAsync
                <ViewModels.FluentResult.FluentResultViewModel<TResponse>>();

            if (result.IsFailed)
            {
                foreach (var error in result.Errors)
                {
                    await _jsRuntime.InvokeVoidAsync("showDangerToast", error.Message);
                }

            }
            else
            {
                foreach (var success in result.Successes)
                {
                    await _jsRuntime.InvokeVoidAsync("showSuccessToast", success.Message);
                }
            }

            Loading = false;
            return result;
        }

        public async System.Threading.Tasks.Task<ViewModels.FluentResult.FluentResultViewModel>
            PutAsJsonAsync<TValue>(string requestUri, TValue value, bool hasAuthorize = false)
        {
            Loading = true;

            if (hasAuthorize)
            {
                await setAuthenticationHeaderAsync();
            }

            var response = await _client.PutAsJsonAsync(requestUri, value);

            var result =
                await response.Content.ReadFromJsonAsync
                <ViewModels.FluentResult.FluentResultViewModel>();

            if (result.IsFailed)
            {
                foreach (var error in result.Errors)
                {
                    await _jsRuntime.InvokeVoidAsync("showDangerToast", error.Message);
                }
            }
            else
            {
                foreach (var success in result.Successes)
                {
                    await _jsRuntime.InvokeVoidAsync("showSuccessToast", success.Message);
                }
            }

            Loading = false;
            return result;
        }

        public async System.Threading.Tasks.Task<ViewModels.FluentResult.FluentResultViewModel<TResponse>>
            PutAsJsonAsync<TValue, TResponse>
            (string requestUri, TValue value, bool hasAuthorize = false)
        {
            Loading = true;

            if (hasAuthorize)
            {
                await setAuthenticationHeaderAsync();
            }

            var response = await _client.PutAsJsonAsync(requestUri, value);

            var result =
                await response.Content.ReadFromJsonAsync
                <ViewModels.FluentResult.FluentResultViewModel<TResponse>>();

            if (result.IsFailed)
            {
                foreach (var error in result.Errors)
                {
                    await _jsRuntime.InvokeVoidAsync("showDangerToast", error.Message);
                }
            }
            else
            {
                foreach (var success in result.Successes)
                {
                    await _jsRuntime.InvokeVoidAsync("showSuccessToast", success.Message);
                }
            }

            Loading = false;
            return result;
        }

        public async System.Threading.Tasks.Task
            <ViewModels.FluentResult.FluentResultViewModel> DeleteAsync
            (string requestUri, bool hasAuthorize = false)
        {
            if (hasAuthorize)
            {
                await setAuthenticationHeaderAsync();
            }

            var response = await _client.DeleteAsync(requestUri);

            var result =
                await response.Content.ReadFromJsonAsync
                <ViewModels.FluentResult.FluentResultViewModel>();

            if (result.IsFailed)
            {
                foreach (var error in result.Errors)
                {
                    await _jsRuntime.InvokeVoidAsync("showDangerToast", error.Message);
                }
            }
            else
            {
                foreach (var success in result.Successes)
                {
                    await _jsRuntime.InvokeVoidAsync("showSuccessToast", success.Message);
                }
            }

            return result;
        }

        public async System.Threading.Tasks.Task
            <ViewModels.FluentResult.FluentResultViewModel<T>> GetAsync<T>
            (string requestUri, bool hasAuthorize = false)
        {
            if (hasAuthorize)
            {
                await setAuthenticationHeaderAsync();
            }

            var response = await _client.GetAsync(requestUri);

            var result =
                await response.Content.ReadFromJsonAsync
                <ViewModels.FluentResult.FluentResultViewModel<T>>();

            if (result.IsFailed)
            {
                foreach (var error in result.Errors)
                {
                    await _jsRuntime.InvokeVoidAsync("showDangerToast", error.Message);
                }
            }

            return result;
        }

        private async System.Threading.Tasks.Task setAuthenticationHeaderAsync()
        {
            var user = await _tokenService.GetUserAsync();

            if (user != null)
            {
                _client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue
                        ("Bearer", user.Token);
            }
        }

    }
}