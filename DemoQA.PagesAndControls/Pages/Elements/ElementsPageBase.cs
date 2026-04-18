using DemoQA.PagesAndControls.Enums;
using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Pages.Elements;

public class ElementsPageBase(IPage page) : PageBase(page)
{
    public ILocator ElementsAccordionTitle => GetAccordionTitle(AccordionListEnum.Elements.GetDescription());
    public ILocator ElementsAccordion => GetAccordion(ElementsAccordionTitle);
    public ILocator AccordionRadioButton => GetAccordionItem(ElementsAccordion, ElementAccordionListEnum.RadioButton.GetDescription());
    public ILocator AccordionTextBox => GetAccordionItem(ElementsAccordion, ElementAccordionListEnum.TextBox.GetDescription());
    public ILocator AccordionWebTables => GetAccordionItem(ElementsAccordion, ElementAccordionListEnum.WebTables.GetDescription());
}