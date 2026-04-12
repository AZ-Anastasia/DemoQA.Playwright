using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Pages;

public class PageBase
{
    private readonly IPage _page;
    public ILocator Title => _page.Locator("h1");

    public PageBase(IPage page)
    {
        _page = page;
    }

    public ILocator GetAccordionTitle(string accordionName) => _page.Locator(".element-group").Filter(new LocatorFilterOptions { HasText = accordionName });
    public ILocator GetAccordion(ILocator locator) => locator.Locator(".element-list");
    public ILocator GetAccordionItem(ILocator locator, string accordionItemName) => locator.Locator("li").Filter(new LocatorFilterOptions { HasText = accordionItemName });
    public async Task<bool> IsAccordionUnfoldedAsync(ILocator locator) => (await locator.GetAttributeAsync("class"))!.Contains("show");
    public async Task<bool> IsAccordionItemSelectedAsync(ILocator locator) => (await locator.GetAttributeAsync("class"))!.Contains("active");

    public async Task UnfoldElementsAccordionAsync(ILocator locatorList, ILocator locatorTitle)
    {
        if (!await IsAccordionUnfoldedAsync(locatorList))
            await locatorTitle.ClickAsync();
    }

    public async Task FoldElementsAccordionAsync(ILocator locatorList, ILocator locatorTitle)
    {
        if (await IsAccordionUnfoldedAsync(locatorList))
            await locatorTitle.ClickAsync();
    }

    public async Task OpenTabFromElementsAccordionAsync(ILocator locator, ILocator locatorList, ILocator locatorTitle)
    {
        await UnfoldElementsAccordionAsync(locatorList, locatorTitle);

        if (!await IsAccordionItemSelectedAsync(locator))
            await locator.ClickAsync();
    }
}