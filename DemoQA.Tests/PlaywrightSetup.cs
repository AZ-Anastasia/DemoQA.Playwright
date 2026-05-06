using System.Text.Json;
using Allure.Net.Commons;
using Allure.NUnit;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace DemoQA.Tests;

[AllureNUnit]
[Parallelizable(ParallelScope.None)]
[TestFixture]
public class PlaywrightSetup : PageTest
{
    protected Dictionary<string, Dictionary<string, object>> Data { get; set; } = null!;

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
    }

    [SetUp]
    public virtual async Task Setup()
    {
        var url = Data["DefaultSettings"]["Url"].ToString();

        await AllureApi.Step($"Переход на сайт {url}", async () =>
        {
            await Page.GotoAsync(url!);
        });
    }
}