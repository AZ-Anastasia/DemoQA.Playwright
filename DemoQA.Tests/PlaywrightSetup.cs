using System.Text.Json;
using Allure.NUnit;
using DemoQA.Tests.Helpers;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace DemoQA.Tests;

[AllureNUnit]
[Parallelizable(ParallelScope.None)]
[TestFixture]
public class PlaywrightSetup : PageTest
{
    protected Dictionary<string, Dictionary<string, object>> Data { get; set; } = null!;
    protected IAPIRequestContext ApiRequest { get; set; } = null!;
    private string _url = null!;

    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        var pathToDirectory = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "TestData.json");

        if (!File.Exists(pathToDirectory))
            throw new Exception($"Не найден файл с тестовыми данными по пути: {pathToDirectory}");

        var json = File.ReadAllText(pathToDirectory);
        Data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(json)!;
        _url = Data["DefaultSettings"]["Url"].ToString()!;
    }

    [SetUp]
    public virtual async Task Setup()
    {
        ApiRequest = await Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = _url
        });
    }

    public virtual async Task SetupUI()
    {
        await AllureHelper.ScreenshotAttachmentAsync($"Переход на сайт {_url}",
        Page,
        async () =>
        {
            await Page.GotoAsync(_url!);
        });
    }

    [OneTimeTearDown]
    public virtual async Task TearDown()
    {
        if (ApiRequest != null) await ApiRequest.DisposeAsync();
    }
}