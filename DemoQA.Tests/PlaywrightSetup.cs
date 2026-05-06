using System.Text.Json;
using Allure.Net.Commons;
using Allure.NUnit;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace DemoQA.Tests;

[AllureNUnit]
[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightSetup : PageTest
{
    protected Dictionary<string, Dictionary<string, object>> Data { get; set; } = null!;
    private static IPlaywright? _playwright;

    [OneTimeSetUp]
    public async Task GlobalSetUp()
    {
        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
    }

    [SetUp]
    public async Task Setup()
    {
        AllureApi.Step("Получение тестовых данных из файла", () =>
        {
            var pathToDirectory = Path.Combine(
                Directory.GetParent(
                    Directory.GetParent(
                        AppDomain.CurrentDomain.BaseDirectory)!
                        .Parent!.FullName)!
                    .Parent!.FullName,
                "TestData.json");
            var json = File.ReadAllText(pathToDirectory);
            Data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(json)!;
        });

        var url = Data["DefaultSettings"]["Url"].ToString();

        await AllureApi.Step($"Переход на сайт {url}", async () =>
        {
            await Page.GotoAsync(url!);
        });
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _playwright?.Dispose();
    }
}