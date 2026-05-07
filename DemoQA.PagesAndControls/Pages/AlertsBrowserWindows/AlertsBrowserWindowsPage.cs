using Allure.NUnit.Attributes;
using DemoQA.Tests.Helpers;
using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Pages.AlertsBrowserWindows;

public class AlertsBrowserWindowsPage(IPage page) : AlertsPageBase(page)
{
    private readonly IPage _page = page;
    public ILocator ConfirmButton => _page.Locator("#confirmButton");
    public ILocator ConfirmResult => _page.Locator("#confirmResult");
    private ILocator _newTabButton => _page.GetByRole(AriaRole.Button, new() { Name = "New Tab" });

    [AllureStep("Выбор действия из диалогового окна: {result}")]
    public async Task<string> ChooseConfirmOptionAsync(bool shouldAccept)
    {
        await AllureHelper.ScreenshotAttachmentAsync(
            "Диалоговое окно",
            this,
            () =>
        {
            _page.Dialog += async (_, dialog) =>
            {
                if (shouldAccept)
                    await dialog.AcceptAsync();
                else
                    await dialog.DismissAsync();
            };
        });

        await ConfirmButton.ClickAsync();

        return shouldAccept ? "Ok" : "Cancel";
    }

    [AllureStep("Открытие новой вкладки в браузере через кнопку из вкладки аккордеона Alerts")]
    public async Task<IPage> OpenNewTabAsync()
    {
        var newPage = await _page.RunAndWaitForPopupAsync(async () =>
        {
            await _newTabButton.ClickAsync();
        });

        await newPage.WaitForLoadStateAsync();

        return newPage;
    }
}