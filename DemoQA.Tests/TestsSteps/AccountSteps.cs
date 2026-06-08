using Allure.NUnit.Attributes;
using DemoQA.API.Base;
using DemoQA.API.Clients;
using DemoQA.API.Models.BookStore.Account;

namespace DemoQA.Tests.TestsSteps;

public class AccountSteps
{
    private AccountClient _accountClient { get; set; }

    public AccountSteps(AccountClient accountClient)
    {
        _accountClient = accountClient;
    }

    [AllureStep("Авторизован ли пользователь")]
    public async Task<bool> IsUserAuthorizedAsync(UserAuthModel user)
    {
        var response = await _accountClient.PostAuthorizedAsync(user);
        await response.EnsureSuccessAsync("Не удалось проверить авторизацию пользователя");

        return await response.ToModelAsync<bool>();
    }

    [AllureStep("Генерация токена")]
    public async Task<TokenViewModel> IsTokenGeneratedAsync(UserAuthModel user)
    {
        var response = await _accountClient.PostGenerateTokenAsync(user);
        await response.EnsureSuccessAsync("Не удалось сгенерировать токен");

        return await response.ToModelAsync<TokenViewModel>();
    }

    [AllureStep("Добавление пользователя")]
    public async Task<UserSignInModel> PostUserAsync(UserAuthModel user)
    {
        var response = await _accountClient.PostUserAsync(user);
        await response.EnsureSuccessAsync("Не удалось добавить пользователя");

        return await response.ToModelAsync<UserSignInModel>();
    }

    [AllureStep("Удаление пользователя")]
    public async Task DeleteUserAsync(string userId)
    {
        var response = await _accountClient.DeleteUserAsync(userId);
        await response.EnsureSuccessAsync("Не удалось удалить пользователя");
    }

    [AllureStep("Получение пользователя")]
    public async Task<UserSignInModel> GetUserAsync(string userId)
    {
        var response = await _accountClient.GetUserAsync(userId);
        await response.EnsureSuccessAsync("Не удалось получить пользователя");

        return await response.ToModelAsync<UserSignInModel>();
    }
}