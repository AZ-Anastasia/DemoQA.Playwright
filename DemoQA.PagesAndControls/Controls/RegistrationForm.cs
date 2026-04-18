using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Controls;

public class RegistrationForm : ModalWindow
{
    public RegistrationForm(ILocator locator) : base(locator)
    {
    }

    public ILocator LastName => _locator.Locator("#lastName");

    public async Task ChangeLastNameAsync(string lastName)
    {
        await LastName.ClickAsync();
        await LastName.FillAsync(lastName);
    }
}