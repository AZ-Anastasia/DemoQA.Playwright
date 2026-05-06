using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Controls;

public class ModalWindow(ILocator locator)
{
    protected readonly ILocator _locator = locator;

    public ILocator SubmitButton => _locator.Locator("#submit");
}