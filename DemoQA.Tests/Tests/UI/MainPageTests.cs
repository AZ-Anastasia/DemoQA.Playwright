using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using DemoQA.PagesAndControls.Pages;
using NUnit.Framework;

namespace DemoQA.Tests.Tests.UI;

[AllureParentSuite("UI")]
[AllureSuite("MainPage")]
public class MainPageTests : PlaywrightSetup
{
    private MainPage _mainPage => new(Page);

    [Test]
    [AllureId(001)]
    public async Task CheckCardsAmountTest()
    {
        var expectedCardsAmount = 6;
        await AllureApi.Step("Проверка количества карточек на странице", async () =>
        {
            await Expect(_mainPage.CategoryCards).ToHaveCountAsync(expectedCardsAmount);
        });
    }
}