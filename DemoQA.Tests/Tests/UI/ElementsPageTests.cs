using Allure.NUnit.Attributes;
using DemoQA.PagesAndControls.Enums;
using DemoQA.PagesAndControls.Pages;
using DemoQA.PagesAndControls.Pages.Elements;
using DemoQA.Tests.Helpers;
using NUnit.Framework;

namespace DemoQA.Tests.Tests.UI;

[AllureParentSuite("UI")]
[AllureSuite("ElementsPage")]
public class ElementsPageTests : PlaywrightSetup
{
    private MainPage _mainPage => new(Page);
    private ElementsPageBase _elementsPage => new(Page);
    private RadioButtonPage _radioButtonPage => new(Page);
    private TextBoxPage _textBoxPage => new(Page);
    private WebTablesPage _webTablesPage => new(Page);

    [SetUp]
    public override async Task Setup()
    {
        await base.Setup();
        await _mainPage.GoToCategoryCardPageAsync(AccordionListEnum.Elements.ToString());
        await _elementsPage.UnfoldElementsAccordionAsync(_elementsPage.ElementsAccordion, _elementsPage.ElementsAccordionTitle);
    }

    [Test]
    [AllureId(002)]
    public async Task AnswerThatYouLikeSiteTest()
    {
        await _elementsPage.OpenTabFromElementsAccordionAsync(_elementsPage.AccordionRadioButton, _elementsPage.ElementsAccordion, _elementsPage.ElementsAccordionTitle);
        await Expect(_elementsPage.Title).ToHaveTextAsync(ElementAccordionListEnum.RadioButton.GetDescription());

        await AllureHelper.ScreenshotAttachmentAsync(
            "Выбор радио-кнопки и проверка отображения текста о выбранной кнопке",
            _elementsPage,
            async () =>
        {
            await _radioButtonPage.RadioButtonImpressive.ClickAsync();
            await Assert.ThatAsync(_radioButtonPage.RadioButtonImpressive.IsCheckedAsync, Is.True);
            await Assert.ThatAsync(_radioButtonPage.TextSuccess.TextContentAsync, Does.Contain(RadioButtonPageEnums.Impressive.ToString()));
        });
    }

    [Test]
    [AllureId(003)]
    public async Task AddNewUserTest()
    {
        await _elementsPage.OpenTabFromElementsAccordionAsync(_elementsPage.AccordionTextBox, _elementsPage.ElementsAccordion, _elementsPage.ElementsAccordionTitle);
        await Expect(_elementsPage.Title).ToHaveTextAsync(ElementAccordionListEnum.TextBox.GetDescription());

        var name = Data["User"]["FullName"].ToString()!;
        var email = Data["User"]["Email"].ToString()!;
        var currentAddress = Data["User"]["CurrentAddress"].ToString()!;
        var permanentAddress = Data["User"]["PermanentAddress"].ToString()!;

        await AllureHelper.ScreenshotAttachmentAsync(
            "Заполнение полей формы и добавление пользователя",
            _elementsPage,
            async () =>
        {
            await _textBoxPage.FullNameInput.FillAsync(name);
            await _textBoxPage.EmailInput.FillAsync(email);
            await _textBoxPage.CurrentAddressInput.FillAsync(currentAddress);
            await _textBoxPage.PermanentAddressInput.FillAsync(permanentAddress);
            await _textBoxPage.SubmitButton.ClickAsync();
        });

        await Expect(_textBoxPage.OutputName).ToContainTextAsync(name);
        await Expect(_textBoxPage.OutputEmail).ToContainTextAsync(email);
        await Expect(_textBoxPage.OutputCurrentAddress).ToContainTextAsync(currentAddress);
        await Expect(_textBoxPage.OutputPermanentAddress).ToContainTextAsync(permanentAddress);
    }

    [Test]
    [AllureId(006)]
    public async Task WebTableFunctianalityTest()
    {
        var firstNameKey = Data["WebTables"].Keys.FirstOrDefault()!.ToString();
        var firstNameToSearch = Data["WebTables"][firstNameKey].ToString()!;

        await _elementsPage.OpenTabFromElementsAccordionAsync(_elementsPage.AccordionWebTables, _elementsPage.ElementsAccordion, _elementsPage.ElementsAccordionTitle);
        await Expect(_elementsPage.Title).ToHaveTextAsync(ElementAccordionListEnum.WebTables.GetDescription());

        await AllureHelper.ScreenshotAttachmentAsync(
            "Проверка количества столбцов и строк в таблице",
            _elementsPage,
            async () =>
        {
            await Expect(_webTablesPage.Headers).ToHaveCountAsync(7);
            await Expect(_webTablesPage.Rows).ToHaveCountAsync(3);
            Assert.That(await _webTablesPage.GetColumnIndexAsync(firstNameKey), Is.Not.EqualTo(-1));
        });

        await AllureHelper.ScreenshotAttachmentAsync(
            "Проверка количества столбцов и строк в таблице",
            _elementsPage,
            async () =>
        {
            await _webTablesPage.SearchInput.FillAsync(firstNameToSearch);
            await Expect(_webTablesPage.Rows).ToHaveCountAsync(1);
        });

        await AllureHelper.ScreenshotAttachmentAsync(
            $"Обновление фамилии пользователя {firstNameToSearch}",
            _elementsPage,
            async () =>
        {
            var lastNameToChange = Data["WebTables"]["LastNameToChange"].ToString()!;
            await _webTablesPage.EditButton.ClickAsync();
            await _webTablesPage.ModalWindow.ChangeLastNameAsync(lastNameToChange);
            await _webTablesPage.ModalWindow.SubmitButton.ClickAsync();
        });

        await AllureHelper.ScreenshotAttachmentAsync(
            $"Удаление из таблицы записи с пользователем {firstNameToSearch}",
            _elementsPage,
            async () =>
        {
            await _webTablesPage.DeleteButton.ClickAsync();
            await _webTablesPage.SearchInput.ClearAsync();
            await Expect(_webTablesPage.Rows).ToHaveCountAsync(2);
        });
    }
}