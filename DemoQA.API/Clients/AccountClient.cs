using Allure.NUnit.Attributes;
using DemoQA.API.Base;
using DemoQA.API.Models.BookStore.Account;
using Microsoft.Playwright;

namespace DemoQA.API.Clients;

[AllureFeature("API Account Client: ")]
public class AccountClient(IAPIRequestContext request, string baseUrl = "/Account") : BaseApiClient(request, baseUrl)
{
    [AllureStep("Авторизация пользователя")]
    public async Task<IAPIResponse> PostAuthorizedAsync(UserAuthModel user)
    {
        return await PostAsync("v1/Authorized", user);
    }

    [AllureStep("Генерация токена")]
    public async Task<IAPIResponse> PostGenerateTokenAsync(UserAuthModel user)
    {
        return await PostAsync("v1/GenerateToken", user);
    }

    [AllureStep("Регистрация пользователя")]
    public async Task<IAPIResponse> PostUserAsync(UserAuthModel user)
    {
        return await PostAsync("v1/User", user);
    }

    [AllureStep("Удаление пользователя")]
    public async Task<IAPIResponse> DeleteUserAsync(string uuid)
    {
        return await DeleteAsync($"v1/User/{uuid}");
    }

    [AllureStep("Получение пользователя")]
    public async Task<IAPIResponse> GetUserAsync(string uuid)
    {
        return await GetAsync($"v1/User/{uuid}");
    }
}