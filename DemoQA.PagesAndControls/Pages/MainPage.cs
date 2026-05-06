using Allure.NUnit.Attributes;
using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Pages;

public class MainPage
{
    private readonly IPage _page;
    public ILocator CategoryCards => _page.Locator(".category-cards").GetByRole(AriaRole.Link);

    public MainPage(IPage page)
    {
        _page = page;
    }

    [AllureStep("Переход на страницу с категорией элементов через карточку")]
    public async Task GoToCategoryCardPageAsync(string cardName) => await _page.GetByRole(AriaRole.Link, new() { Name = cardName }).ClickAsync();
}