using Allure.NUnit.Attributes;
using DemoQA.API.Base;
using DemoQA.API.Models.BookStore.BookStore;
using Microsoft.Playwright;

namespace DemoQA.API.Clients;

public class BookStoreClient(IAPIRequestContext request, string baseUrl = "/BookStore")
    : BaseApiClient(request, baseUrl)
{
    [AllureStep("{this}: Получение списка книг")]
    public async Task<IAPIResponse> GetBookListAsync()
    {
        return await GetAsync("v1/Books");
    }

    [AllureStep("{this}: Добавление книги/списка книг пользователю")]
    public async Task<IAPIResponse> PostBooksListIsbnAsync(AddListOfBooksModel listOfBooks)
    {
        return await PostAsync("v1/Books", listOfBooks);
    }

    [AllureStep("{this}: Очистка книг у пользователя")]
    public async Task<IAPIResponse> DeleteUserBooksAsync(string userId)
    {
        return await DeleteAsync($"v1/Books?UserId={userId}");
    }

    [AllureStep("{this}: Получение книги по ISBN")]
    public async Task<IAPIResponse> GetBookAsync(string isbn)
    {
        return await GetAsync($"v1/Book?ISBN={isbn}");
    }

    [AllureStep("{this}: Удаление книги у пользователя")]
    public async Task<IAPIResponse> DeleteBookIsbnAsync(UserBookModel listOfBooks)
    {
        return await DeleteAsync("v1/Book", listOfBooks);
    }

    [AllureStep("{this}: Замена книги по ISBN у пользователя")]
    public async Task<IAPIResponse> PutBookListIsbnAsync(string isbn, UserBookModel listOfBooks)
    {
        return await PutAsync($"v1/Book?ISBN={isbn}", listOfBooks);
    }
}