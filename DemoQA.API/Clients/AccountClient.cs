using Allure.NUnit.Attributes;
using DemoQA.API.Base;
using DemoQA.API.Models.BookStore.Account;
using Microsoft.Playwright;

namespace DemoQA.API.Clients;

public class AccountClient(IAPIRequestContext request, string baseUrl = "/Account") : BaseApiClient(request, baseUrl)
{
    [AllureStep("{this}: Авторизация пользователя")]
    public async Task<IAPIResponse> PostAuthorizedAsync(UserAuthModel user)
    {
        return await PostAsync("v1/Authorized", user);
    }

    [AllureStep("{this}: Генерация токена")]
    public async Task<IAPIResponse> PostGenerateTokenAsync(UserAuthModel user)
    {
        return await PostAsync("v1/GenerateToken", user);
    }

    [AllureStep("{this}: Регистрация пользователя")]
    public async Task<IAPIResponse> PostUserAsync(UserAuthModel user)
    {
        return await PostAsync("v1/User", user);
    }

    [AllureStep("{this}: Удаление пользователя")]
    public async Task<IAPIResponse> DeleteUserAsync(string uuid)
    {
        return await DeleteAsync($"v1/User/{uuid}");
    }

    [AllureStep("{this}: Получение пользователя")]
    public async Task<IAPIResponse> GetUserAsync(string uuid)
    {
        return await GetAsync($"v1/User/{uuid}");
    }
}