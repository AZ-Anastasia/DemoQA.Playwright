using DemoQA.PagesAndControls.Enums;
using DemoQA.PagesAndControls.Pages;
using DemoQA.PagesAndControls.Pages.Elements;
using NUnit.Framework;

namespace DemoQA.Tests.Tests.UI;

public class ElementsPageTests : PlaywrightSetup
{
    private MainPage _mainPage => new(Page);
    private ElementsPageBase _elementsPage => new(Page);
    private RadioButtonPage _radioButtonPage => new(Page);
    private TextBoxPage _textBoxPage => new(Page);

    [SetUp]
    public new async Task Setup()
    {
        await base.Setup();
        await _mainPage.GoToCategoryCardPageAsync(AccordionListEnum.Elements.ToString());
        await _elementsPage.UnfoldElementsAccordionAsync(_elementsPage.ElementsAccordion, _elementsPage.ElementsAccordionTitle);
    }

    [Test]
    [Property("TestID", "002")]
    public async Task AnswerThatYouLikeSiteTest()
    {
        await _elementsPage.OpenTabFromElementsAccordionAsync(_elementsPage.AccordionRadioButton, _elementsPage.ElementsAccordion, _elementsPage.ElementsAccordionTitle);
        await Expect(_elementsPage.Title).ToHaveTextAsync(ElementAccordionListEnum.RadioButton.GetDescription());

        await _radioButtonPage.RadioButtonImpressive.ClickAsync();
        await Assert.ThatAsync(_radioButtonPage.RadioButtonImpressive.IsCheckedAsync, Is.True);
        await Assert.ThatAsync(_radioButtonPage.TextSuccess.TextContentAsync, Does.Contain(RadioButtonPageEnums.Impressive.ToString()));
    }

    [Test]
    [Property("TestID", "003")]
    public async Task AddNewUserTest()
    {
        await _elementsPage.OpenTabFromElementsAccordionAsync(_elementsPage.AccordionTextBox, _elementsPage.ElementsAccordion, _elementsPage.ElementsAccordionTitle);
        await Expect(_elementsPage.Title).ToHaveTextAsync(ElementAccordionListEnum.TextBox.GetDescription());

        var name = Data["User"]["FullName"].ToString()!;
        var email = Data["User"]["Email"].ToString()!;
        var currentAddress = Data["User"]["CurrentAddress"].ToString()!;
        var permanentAddress = Data["User"]["PermanentAddress"].ToString()!;
        await _textBoxPage.FullNameInput.FillAsync(name);
        await _textBoxPage.EmailInput.FillAsync(email);
        await _textBoxPage.CurrentAddressInput.FillAsync(currentAddress);
        await _textBoxPage.PermanentAddressInput.FillAsync(permanentAddress);
        await _textBoxPage.SubmitButton.ClickAsync();

        await Expect(_textBoxPage.OutputName).ToContainTextAsync(name);
        await Expect(_textBoxPage.OutputEmail).ToContainTextAsync(email);
        await Expect(_textBoxPage.OutputCurrentAddress).ToContainTextAsync(currentAddress);
        await Expect(_textBoxPage.OutputPermanentAddress).ToContainTextAsync(permanentAddress);
    }
}