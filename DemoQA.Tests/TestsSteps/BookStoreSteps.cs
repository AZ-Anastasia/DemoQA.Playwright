using Allure.NUnit.Attributes;
using DemoQA.API.Base;
using DemoQA.API.Clients;
using DemoQA.API.Models.BookStore.BookStore;

namespace DemoQA.Tests.TestsSteps;

public class BookStoreSteps
{
    private BookStoreClient _bookStoreClient { get; set; }

    public BookStoreSteps(BookStoreClient bookStoreClient)
    {
        _bookStoreClient = bookStoreClient;
    }

    [AllureStep("Получение списка книг")]
    public async Task<BookListModel> GetBookListAsync()
    {
        var response = await _bookStoreClient.GetBookListAsync();
        await response.EnsureSuccessAsync("Не удалось получить список книг");

        return await response.ToModelAsync<BookListModel>();
    }

    [AllureStep("Добавление списка книг")]
    public async Task<AllBooksModel> PostBooksListIsbnAsync(AddListOfBooksModel listOfBooks)
    {
        var response = await _bookStoreClient.PostBooksListIsbnAsync(listOfBooks);
        await response.EnsureSuccessAsync("Не удалось добавить список книг");

        return await response.ToModelAsync<AllBooksModel>();
    }
}