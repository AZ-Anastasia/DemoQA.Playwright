using DemoQA.PagesAndControls.Enums;
using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Pages.AlertsBrowserWindows;

public class AlertsPageBase(IPage page) : PageBase(page)
{
    public ILocator AlertsAccordionTitle => AccordionTitle(AccordionListEnum.AlertsFrameWindows.GetDescription());
    public ILocator AlertsAccordion => Accordion(AlertsAccordionTitle);
    public ILocator AccordionAlerts => AccordionItem(AlertsAccordion, AlertsAccordionListEnum.Alerts.GetDescription());
    public ILocator AccordionBrowserWindows => AccordionItem(AlertsAccordion, AlertsAccordionListEnum.BrowserWindows.GetDescription());
}