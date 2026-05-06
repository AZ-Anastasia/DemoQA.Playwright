using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Pages.Elements;

public class TextBoxPage(IPage page) : PageBase(page)
{
    private readonly IPage _page = page;
    public ILocator FullNameInput => _page.Locator("#userName");
    public ILocator EmailInput => _page.Locator("#userEmail");
    public ILocator CurrentAddressInput => _page.Locator("#currentAddress");
    public ILocator PermanentAddressInput => _page.Locator("#permanentAddress");
    public ILocator SubmitButton => _page.Locator("#submit");

    private ILocator _outputContainer => _page.Locator("#output");
    public ILocator OutputName => _outputContainer.Locator("#name");
    public ILocator OutputEmail => _outputContainer.Locator("#email");
    public ILocator OutputCurrentAddress => _outputContainer.Locator("#currentAddress");
    public ILocator OutputPermanentAddress => _outputContainer.Locator("#permanentAddress");
}