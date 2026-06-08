using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using DemoQA.API.Base;
using DemoQA.API.Clients;
using DemoQA.API.Enums;
using DemoQA.API.Models.BookStore.Account;
using DemoQA.API.Models.BookStore.BookStore;
using DemoQA.PagesAndControls.Enums;
using DemoQA.Tests.TestsSteps;
using NUnit.Framework;

namespace DemoQA.Tests.Tests.API;

[AllureParentSuite("API")]
[AllureSuite("BookStore")]
[AllureSubSuite("Guest")]
public class BookStoreGuestTests : PlaywrightSetup
{
    private BookStoreClient _bookStoreClient { get; set; }
    private BookStoreSteps _bookStoreSteps { get; set; }

    [SetUp]
    public new void Setup()
    {
        GlobalSetUp();
        _bookStoreClient = new BookStoreClient(Page.APIRequest);
        _bookStoreSteps = new BookStoreSteps(_bookStoreClient);
    }

    #region BookStore

    [Test]
    [AllureId(009)]
    public async Task GetBookListTest()
    {
        await _bookStoreSteps.GetBookListAsync();
    }

    [Test]
    [AllureId(010)]
    public async Task GetBookTest()
    {
        var bookList = await _bookStoreSteps.GetBookListAsync();
        var isbnToSearchFor = bookList.Books!.First().Isbn!;

        var response = await _bookStoreClient.GetBookAsync(isbnToSearchFor);
        await Expect(response).ToBeOKAsync();
        var isbnFromResult = await response.ToModelAsync<BookModel>();
        Assert.That(isbnFromResult, Is.EqualTo(isbnToSearchFor));
    }

    #endregion
}

[AllureParentSuite("API")]
[AllureSuite("BookStore")]
[AllureSubSuite("User")]
public class BookStoreTests : PlaywrightSetup
{
    private AccountClient _accountClient { get; set; }
    private BookStoreClient _bookStoreClient { get; set; }
    private AccountSteps _accountSteps { get; set; }
    private BookStoreSteps _bookStoreSteps { get; set; }
    private UserAuthModel _user = null!;
    private UserSignInModel _addedUser = null!;

    [SetUp]
    public new async Task Setup()
    {
        GlobalSetUp();

        _accountClient = new AccountClient(Page.APIRequest);
        _bookStoreClient = new BookStoreClient(Page.APIRequest);
        _accountSteps = new AccountSteps(_accountClient);
        _bookStoreSteps = new BookStoreSteps(_bookStoreClient);

        var uniqueId = Guid.NewGuid().ToString("N").Substring(0, 8);
        _user = new UserAuthModel
        {
            UserName = Data["UserAPI"]["UserName"].ToString()! + uniqueId,
            Password = Data["UserAPI"]["Password"].ToString()!
        };

        _addedUser = await PostUserWithStepAsync();
        await AllureApi.Step("Наличие активного токена", async () =>
        {
            await _accountSteps.IsTokenGeneratedAsync(_user);
        });
    }

    [TearDown]
    public async Task Teardown()
    {
        // В случае сбоев
        if (_addedUser?.UserId != null)
            await _accountSteps.DeleteUserAsync(_addedUser.UserId);
    }

    private async Task<UserSignInModel> PostUserWithStepAsync()
    {
        return await AllureApi.Step("Добавление пользователя", async () =>
        {
            return await _accountSteps.PostUserAsync(_user);
        });
    }

    private AddListOfBooksModel GetDefaultBookList()
    {
        return new AddListOfBooksModel
        {
            UserId = _addedUser.UserId,
            collectionOfIsbns = new List<CollectionOfIsbnsModel>
            {
                new CollectionOfIsbnsModel
                {
                    Isbn = BookIsbn.GitPocketGuide.GetDescription()
                },
                new CollectionOfIsbnsModel
                {
                    Isbn = BookIsbn.LearningJavaScriptDesignPatterns.GetDescription()
                },
                new CollectionOfIsbnsModel
                {
                    Isbn = BookIsbn.DesigningEvolvableWebAPIswithASPNET.GetDescription()
                }
            }
        };
    }

    #region Account

    [Test]
    [AllureId(007)]
    public async Task PostIsAuthorizedTest()
    {
        var isAuthorized = await _accountSteps.IsUserAuthorizedAsync(_user);
        Assert.That(isAuthorized, Is.True);
    }

    [Test]
    [AllureId(008)]
    public async Task GetUserTest()
    {
        var isAuthorized = await _accountSteps.IsUserAuthorizedAsync(_user);
        Assert.That(isAuthorized, Is.True);

        await _accountSteps.GetUserAsync(_addedUser.UserId!);
    }

    #endregion

    #region BookStore

    [Test]
    [AllureId(011)]
    public async Task PostBookListIsbnTest()
    {
        var bookList = await _bookStoreSteps.GetBookListAsync();

        await AllureApi.Step("Проверка авторизации", async () =>
        {
            var isAuthorized = await _accountSteps.IsUserAuthorizedAsync(_user);
            Assert.That(isAuthorized, Is.True);
        });

        var bookIsbnsForAdd = GetDefaultBookList();

        var addedBooks = await _bookStoreSteps.PostBooksListIsbnAsync(bookIsbnsForAdd);
        Assert.That(addedBooks.Books!.First().Isbn, Is.EqualTo(bookIsbnsForAdd.collectionOfIsbns!.First().Isbn));
    }

    [Test]
    [AllureId(012)]
    public async Task DeleteUserBooksTest()
    {
        await AllureApi.Step("Проверка авторизации", async () =>
        {
            var isAuthorized = await _accountSteps.IsUserAuthorizedAsync(_user);
            Assert.That(isAuthorized, Is.True);
        });

        var bookIsbnsForAdd = GetDefaultBookList();
        var addedBooks = await _bookStoreSteps.PostBooksListIsbnAsync(bookIsbnsForAdd);
        Assert.That(addedBooks.Books!.Count(), Is.EqualTo(bookIsbnsForAdd.collectionOfIsbns!.Count()));
        Assert.That(addedBooks.Books!.First().Isbn, Is.EqualTo(bookIsbnsForAdd.collectionOfIsbns!.First().Isbn));

        var response = await _bookStoreClient.DeleteUserBooksAsync(_addedUser.UserId!);
        await Expect(response).ToBeOKAsync();
    }

    [Test]
    [AllureId(013)]
    public async Task DeleteBookIsbnTest()
    {
        await AllureApi.Step("Проверка авторизации", async () =>
        {
            var isAuthorized = await _accountSteps.IsUserAuthorizedAsync(_user);
            Assert.That(isAuthorized, Is.True);
        });

        var bookIsbnsForAdd = GetDefaultBookList();
        var addedBooks = await _bookStoreSteps.PostBooksListIsbnAsync(bookIsbnsForAdd);
        Assert.That(addedBooks.Books!.Count(), Is.EqualTo(bookIsbnsForAdd.collectionOfIsbns!.Count()));
        Assert.That(addedBooks.Books!.First().Isbn, Is.EqualTo(bookIsbnsForAdd.collectionOfIsbns!.First().Isbn));

        var isbnFromUser = await AllureApi.Step("Получение isbn из данных о пользователе", async () =>
        {
            var userData = await _accountSteps.GetUserAsync(_addedUser.UserId!);
            return userData.Books!.First().Isbn;
        });

        var bookToDelete = new UserBookModel
        {
            Isbn = isbnFromUser,
            UserId = _addedUser.UserId
        };

        var response = await _bookStoreClient.DeleteBookIsbnAsync(bookToDelete);
        await Expect(response).ToBeOKAsync();
    }

    [Test]
    [AllureId(014)]
    public async Task PutBookListIsbnTest()
    {
        await AllureApi.Step("Проверка авторизации", async () =>
        {
            var isAuthorized = await _accountSteps.IsUserAuthorizedAsync(_user);
            Assert.That(isAuthorized, Is.True);
        });

        var bookIsbnsForAdd = GetDefaultBookList();
        var addedBooks = await _bookStoreSteps.PostBooksListIsbnAsync(bookIsbnsForAdd);
        Assert.That(addedBooks.Books!.Count(), Is.EqualTo(bookIsbnsForAdd.collectionOfIsbns!.Count()));
        Assert.That(addedBooks.Books!.First().Isbn, Is.EqualTo(bookIsbnsForAdd.collectionOfIsbns!.First().Isbn));

        var isbnFromUser = await AllureApi.Step("Получение isbn из данных о пользователе", async () =>
        {
            var userData = await _accountSteps.GetUserAsync(_addedUser.UserId!);
            return userData.Books!.First().Isbn;
        });

        var bookToUpdate = new UserBookModel
        {
            Isbn = BookIsbnToReplace.SpeakingJavaScript.GetDescription(),
            UserId = _addedUser.UserId
        };

        var response = await _bookStoreClient.PutBookListIsbnAsync(isbnFromUser!, bookToUpdate);
        await Expect(response).ToBeOKAsync();
    }

    #endregion
}