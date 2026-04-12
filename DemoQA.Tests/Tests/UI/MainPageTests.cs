using DemoQA.PagesAndControls.Pages;
using NUnit.Framework;

namespace DemoQA.Tests.Tests.UI;

public class MainPageTests : PlaywrightSetup
{
    private MainPage _mainPage => new(Page);

    [Test]
    [Property("TestID", "001")]
    public async Task CheckCardsAmountTest()
    {
        await Expect(_mainPage.CategoryCards).ToHaveCountAsync(6);
    }
}