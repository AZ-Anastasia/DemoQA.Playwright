using Allure.NUnit.Attributes;
using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Pages;

public class MainPage(IPage page)
{
    private readonly IPage _page = page;
    public ILocator CategoryCards => _page.Locator(".category-cards").GetByRole(AriaRole.Link);

    [AllureStep("Переход на страницу с категорией элементов через карточку")]
    public async Task GoToCategoryCardPageAsync(string cardName) => await _page.GetByRole(AriaRole.Link, new() { Name = cardName }).ClickAsync();
}