using DemoQA.PagesAndControls.Controls;
using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Pages.Elements;

public class WebTablesPage : PageBase
{
    private readonly IPage _page;
    private ILocator _table => _page.Locator(".table");
    public ILocator Headers => _table.Locator("thead > tr > th");
    public ILocator Rows => _table.Locator("tbody > tr");
    public ILocator Cells => _table.Locator("td");
    public ILocator SearchInput => _page.Locator("#searchBox");
    public ILocator EditButton => _page.GetByTitle("Edit");
    public ILocator DeleteButton => _page.GetByTitle("Delete");
    public RegistrationForm ModalWindow => new(_page.Locator(".modal-content"));

    public WebTablesPage(IPage page) : base(page)
    {
        _page = page;
    }

    public async Task<int> GetColumnIndexAsync(string columnText)
    {
        var headers = await Headers.AllInnerTextsAsync();
        int columnIndex = Array.IndexOf(headers.ToArray(), columnText);
        if (columnIndex == -1) throw new Exception($"There is no column with title \"{columnText}\"");
        return columnIndex;
    }

    public async Task<string> GetColumnTextAsync(int columnIndex)
    {
        var cell = Cells.Nth(columnIndex);
        return (await cell.TextContentAsync())!;
    }
}