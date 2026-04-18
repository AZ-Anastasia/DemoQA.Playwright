using DemoQA.PagesAndControls.Enums;
using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Pages.AlertsBrowserWindows;

public class AlertsPageBase(IPage page) : PageBase(page)
{
    public ILocator AlertsAccordionTitle => GetAccordionTitle(AccordionListEnum.AlertsFrameWindows.GetDescription());
    public ILocator AlertsAccordion => GetAccordion(AlertsAccordionTitle);
    public ILocator AccordionAlerts => GetAccordionItem(AlertsAccordion, AlertsAccordionListEnum.Alerts.GetDescription());
    public ILocator AccordionBrowserWindows => GetAccordionItem(AlertsAccordion, AlertsAccordionListEnum.BrowserWindows.GetDescription());
}