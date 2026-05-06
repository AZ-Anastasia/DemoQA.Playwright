using DemoQA.PagesAndControls.Enums;
using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Pages.Elements;

public class ElementsPageBase(IPage page) : PageBase(page)
{
    public ILocator ElementsAccordionTitle => AccordionTitle(AccordionListEnum.Elements.GetDescription());
    public ILocator ElementsAccordion => Accordion(ElementsAccordionTitle);
    public ILocator AccordionRadioButton => AccordionItem(ElementsAccordion, ElementAccordionListEnum.RadioButton.GetDescription());
    public ILocator AccordionTextBox => AccordionItem(ElementsAccordion, ElementAccordionListEnum.TextBox.GetDescription());
    public ILocator AccordionWebTables => AccordionItem(ElementsAccordion, ElementAccordionListEnum.WebTables.GetDescription());
}