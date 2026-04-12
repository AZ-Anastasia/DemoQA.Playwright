using DemoQA.PagesAndControls.Controls;
using DemoQA.PagesAndControls.Enums;
using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Pages.Elements;

public class RadioButtonPage : PageBase
{
    private readonly IPage _page;
    public RadioButton RadioButtonImpressive => new(_page.GetByLabel(RadioButtonPageEnums.Impressive.ToString()));
    public RadioButton TextSuccess => new(_page.Locator(".text-success"));

    public RadioButtonPage(IPage page) : base(page)
    {
        _page = page;
    }
}