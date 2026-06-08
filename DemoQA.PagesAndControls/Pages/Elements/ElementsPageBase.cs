using DemoQA.PagesAndControls.Enums;
using Microsoft.Playwright;

namespace DemoQA.PagesAndControls.Pages.Elements;

public class ElementsPageBase(IPage page) : PageBase(page)
{
    public ILocator ElementsAccordionTitle => AccordionTitle(AccordionListName.Elements.GetDescription());
    public ILocator ElementsAccordion => Accordion(ElementsAccordionTitle);
    public ILocator AccordionRadioButton => AccordionItem(ElementsAccordion, ElementAccordionListName.RadioButton.GetDescription());
    public ILocator AccordionTextBox => AccordionItem(ElementsAccordion, ElementAccordionListName.TextBox.GetDescription());
    public ILocator AccordionWebTables => AccordionItem(ElementsAccordion, ElementAccordionListName.WebTables.GetDescription());
}