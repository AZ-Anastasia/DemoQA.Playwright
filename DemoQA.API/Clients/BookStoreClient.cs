using Allure.NUnit.Attributes;
using DemoQA.API.Base;
using DemoQA.API.Models.BookStore.BookStore;
using Microsoft.Playwright;

namespace DemoQA.API.Clients;

public class BookStoreClient(IAPIRequestContext request, string baseUrl = "/BookStore")
    : BaseApiClient(request, baseUrl)
{
    [AllureStep("BookStoreClient: Получение списка книг")]
    public async Task<IAPIResponse> GetBookListAsync()
    {
        return await GetAsync("v1/Books");
    }

    [AllureStep("BookStoreClient: Добавление книги/списка книг пользователю")]
    public async Task<IAPIResponse> PostBooksListIsbnAsync(AddListOfBooksModel listOfBooks)
    {
        return await PostAsync("v1/Books", listOfBooks);
    }

    [AllureStep("BookStoreClient: Очистка книг у пользователя")]
    public async Task<IAPIResponse> DeleteUserBooksAsync(string userId)
    {
        return await DeleteAsync($"v1/Books?UserId={userId}");
    }

    [AllureStep("BookStoreClient: Получение книги по ISBN")]
    public async Task<IAPIResponse> GetBookAsync(string isbn)
    {
        return await GetAsync($"v1/Book?ISBN={isbn}");
    }

    [AllureStep("BookStoreClient: Удаление книги у пользователя")]
    public async Task<IAPIResponse> DeleteBookIsbnAsync(UserBookModel listOfBooks)
    {
        return await DeleteAsync("v1/Book", listOfBooks);
    }

    [AllureStep("BookStoreClient: Замена книги по ISBN у пользователя")]
    public async Task<IAPIResponse> PutBookListIsbnAsync(string isbn, UserBookModel listOfBooks)
    {
        return await PutAsync($"v1/Book?ISBN={isbn}", listOfBooks);
    }
}