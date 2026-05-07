using DemoQA.Tests.Helpers;
using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Pages;

public class PageBase(IPage page)
{
    private readonly IPage _page = page;
    public ILocator Title => _page.Locator("h1");
    public ILocator AccordionTitle(string accordionName) =>
        _page
            .Locator(".element-group")
            .Filter(new LocatorFilterOptions { HasText = accordionName });
    public ILocator Accordion(ILocator locator) => locator.Locator(".element-list");
    public ILocator AccordionItem(ILocator locator, string accordionItemName) =>
        locator
            .Locator("li")
            .Filter(new LocatorFilterOptions { HasText = accordionItemName });

    public async Task<bool> IsAccordionUnfoldedAsync(ILocator locator) => (await locator.GetAttributeAsync("class"))!.Contains("show");
    public async Task<bool> IsAccordionItemSelectedAsync(ILocator locator) => (await locator.GetAttributeAsync("class"))!.Contains("active");
    public async Task<byte[]> ScreenshotAsync() => await _page.ScreenshotAsync();

    public async Task UnfoldElementsAccordionAsync(ILocator locatorList, ILocator locatorTitle)
    {
        await AllureHelper.ScreenshotAttachmentAsync(
            "Развернуть вкладку с элементами из аккордеона, если она свернута",
            this,
            async () =>
        {
            if (!await IsAccordionUnfoldedAsync(locatorList))
                await locatorTitle.ClickAsync();
        });
    }

    public async Task FoldElementsAccordionAsync(ILocator locatorList, ILocator locatorTitle)
    {
        await AllureHelper.ScreenshotAttachmentAsync(
            "Свернуть вкладку с элементами из аккордеона, если она развернута",
            this,
            async () =>
        {
            if (await IsAccordionUnfoldedAsync(locatorList))
                await locatorTitle.ClickAsync();
        });
    }

    public async Task OpenTabFromElementsAccordionAsync(ILocator locator, ILocator locatorList, ILocator locatorTitle)
    {
        await AllureHelper.ScreenshotAttachmentAsync(
            $"Открыть страницу с элементом из вкладки аккордеона {(await locatorTitle.Locator(".group-header").TextContentAsync())!.Trim()}",
            this,
            async () =>
        {
            await UnfoldElementsAccordionAsync(locatorList, locatorTitle);

            if (!await IsAccordionItemSelectedAsync(locator))
                await locator.ClickAsync();
        });
    }
}