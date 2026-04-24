using System.Text.Json;
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
    protected Dictionary<string, Dictionary<string, object>> Data;
    private static IPlaywright? _playwright;

    [OneTimeSetUp]
    public async Task GlobalSetUp()
    {
        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
    }

    [SetUp]
    public async Task Setup()
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

        await Page.GotoAsync(Data["DefaultSettings"]["Url"].ToString()!);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _playwright?.Dispose();
    }
}