using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Controls;

public class RadioButton
{
    protected readonly ILocator _locator;

    public RadioButton(ILocator locator)
    {
        _locator = locator;
    }

    public virtual async Task ClickAsync() => await _locator.ClickAsync();
    public virtual async Task<string?> TextContentAsync() => await _locator.TextContentAsync();
    public virtual async Task<bool> IsCheckedAsync() => await _locator.IsCheckedAsync();
}