using Allure.Net.Commons;
using DemoQA.PagesAndControls.Pages;
using Microsoft.Playwright;

namespace DemoQA.Tests.Helpers;

public static class AllureHelper
{
    public static async Task ExecuteWithScreenshot(string stepDescription, Func<Task<byte[]>> screenshotProvider, Func<Task> action)
    {
        await AllureApi.Step(stepDescription, async () =>
        {
            try
            {
                await action();
            }
            finally // Гарантируем создание скриншота
            {
                var bytes = await screenshotProvider();
                AllureApi.AddAttachment($"Скриншот: {stepDescription}", "image/png", bytes);
            }
        });
    }

    // IPage + Func<Task> (async)
    public static Task ScreenshotAttachmentAsync(string stepDescription, IPage page, Func<Task> action)
    => ExecuteWithScreenshot(stepDescription, () => page.ScreenshotAsync(), action);

    // PageBase + Func<Task> (async)
    public static Task ScreenshotAttachmentAsync(string stepDescription, PageBase page, Func<Task> action)
    => ExecuteWithScreenshot(stepDescription, () => page.ScreenshotAsync(), action);

    // PageBase + Action (sync)
    public static Task ScreenshotAttachmentAsync(string stepDescription, PageBase page, Action action)
    => ScreenshotAttachmentAsync(stepDescription, page, () => { action(); return Task.CompletedTask; });
}