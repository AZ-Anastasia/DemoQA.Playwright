using Allure.NUnit.Attributes;
using DemoQA.PagesAndControls.Enums;
using DemoQA.PagesAndControls.Pages;
using DemoQA.PagesAndControls.Pages.AlertsBrowserWindows;
using NUnit.Framework;

namespace DemoQA.Tests.Tests.UI;

[AllureParentSuite("UI")]
[AllureSuite("AlertsPage")]
public class AlertsPageTests : PlaywrightSetup
{
    private MainPage _mainPage => new(Page);
    private AlertsPageBase _alertsPageBase => new(Page);
    private AlertsBrowserWindowsPage _alertsPage => new(Page);

    [SetUp]
    public new async Task Setup()
    {
        await base.Setup();
        await _mainPage.GoToCategoryCardPageAsync(AccordionListEnum.AlertsFrameWindows.GetDescription());
        await _alertsPageBase.UnfoldElementsAccordionAsync(_alertsPageBase.AlertsAccordion, _alertsPageBase.AlertsAccordionTitle);
    }

    [Test]
    [Property("TestID", "004")]
    public async Task ConfirmAlertTest()
    {
        await _alertsPageBase.OpenTabFromElementsAccordionAsync(_alertsPageBase.AccordionAlerts, _alertsPageBase.AlertsAccordion, _alertsPageBase.AlertsAccordionTitle);
        await Expect(_alertsPageBase.Title).ToHaveTextAsync(AlertsAccordionListEnum.Alerts.GetDescription());

        var result = await _alertsPage.ChooseConfirmOptionAsync(true);
        await _alertsPage.ConfirmButton.ClickAsync();
        await Expect(_alertsPage.ConfirmResult).ToContainTextAsync(result);
    }

    [Test]
    [Property("TestID", "005")]
    public async Task OpenNewTabTest()
    {
        await _alertsPageBase.OpenTabFromElementsAccordionAsync(_alertsPageBase.AccordionBrowserWindows, _alertsPageBase.AlertsAccordion, _alertsPageBase.AlertsAccordionTitle);
        await Expect(_alertsPageBase.Title).ToHaveTextAsync(AlertsAccordionListEnum.BrowserWindows.GetDescription());

        var newTabPage = await _alertsPage.OpenNewTabAsync();
        Assert.That(newTabPage.Url.Split('/').Last(), Does.Contain("sample"));
    }
}