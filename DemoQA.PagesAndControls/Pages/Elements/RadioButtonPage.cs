using DemoQA.PagesAndControls.Controls;
using DemoQA.PagesAndControls.Enums;
using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Pages.Elements;

public class RadioButtonPage(IPage page) : PageBase(page)
{
    private readonly IPage _page = page;
    public RadioButton RadioButtonImpressive => new(_page.GetByLabel(RadioButtonPageEnums.Impressive.ToString()));
    public RadioButton TextSuccess => new(_page.Locator(".text-success"));
}