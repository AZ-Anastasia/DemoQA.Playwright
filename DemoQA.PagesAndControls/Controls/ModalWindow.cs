using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Controls;

public class ModalWindow
{
    protected readonly ILocator _locator;

    public ModalWindow(ILocator locator)
    {
        _locator = locator;
    }
    public ILocator SubmitButton => _locator.Locator("#submit");
}