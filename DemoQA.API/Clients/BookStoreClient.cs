using Allure.NUnit.Attributes;
using DemoQA.API.Base;
using DemoQA.API.Models.BookStore.BookStore;
using Microsoft.Playwright;

namespace DemoQA.API.Clients;

[AllureFeature("API BookStore Client: ")]
public class BookStoreClient(IAPIRequestContext request, string baseUrl = "/BookStore")
    : BaseApiClient(request, baseUrl)
{
    [AllureStep("Получение списка книг")]
    public async Task<IAPIResponse> GetBookListAsync()
    {
        return await GetAsync("v1/Books");
    }

    [AllureStep("Добавление книги/списка книг пользователю")]
    public async Task<IAPIResponse> PostBooksListIsbnAsync(AddListOfBooksModel listOfBooks)
    {
        return await PostAsync("v1/Books", listOfBooks);
    }

    [AllureStep("Очистка книг у пользователя")]
    public async Task<IAPIResponse> DeleteUserBooksAsync(string userId)
    {
        return await DeleteAsync($"v1/Books?UserId={userId}");
    }

    [AllureStep("Получение книги по ISBN")]
    public async Task<IAPIResponse> GetBookAsync(string isbn)
    {
        return await GetAsync($"v1/Book?ISBN={isbn}");
    }

    [AllureStep("Удаление книги у пользователя")]
    public async Task<IAPIResponse> DeleteBookIsbnAsync(UserBookModel listOfBooks)
    {
        return await DeleteAsync("v1/Book", listOfBooks);
    }

    [AllureStep("Замена книги по ISBN у пользователя")]
    public async Task<IAPIResponse> PutBookListIsbnAsync(string isbn, UserBookModel listOfBooks)
    {
        return await PutAsync($"v1/Book?ISBN={isbn}", listOfBooks);
    }
}