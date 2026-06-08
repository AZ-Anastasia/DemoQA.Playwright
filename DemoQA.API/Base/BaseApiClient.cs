using System.Text.Json;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace DemoQA.API.Base;

public abstract class BaseApiClient : PlaywrightTest
{
    protected readonly IAPIRequestContext Request;
    protected readonly string BaseUrl;
    private static string? _token;

    /// <summary>
    /// Тестовые данные из файла
    /// </summary>
    protected Dictionary<string, Dictionary<string, object>> Data { get; set; } = null!;

    protected BaseApiClient(IAPIRequestContext request, string baseUrl)
    {
        var pathToDirectory = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "TestData.json");

        if (!File.Exists(pathToDirectory))
            throw new Exception($"Не найден файл с тестовыми данными по пути: {pathToDirectory}");

        var json = File.ReadAllText(pathToDirectory);
        Data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(json)!;

        Request = request;
        BaseUrl = Data["DefaultSettings"]["Url"].ToString() + baseUrl;
    }

    /// <summary>
    /// Метод для выполнения HTTP-методов
    /// </summary>
    /// <param name="endpoint">Относительный путь API без базового URL</param>
    /// <param name="sendRequest">Отправляемый запрос к серверу</param>
    /// <returns>Объект <see cref="IAPIResponse"/>, содержащий статус-код, заголовки и тело ответа сервера</returns>
    /// <exception cref="Exception">Если при выполнении возникла ошибка, выводит сообщение с URL и статус-кодом</exception>
    public async Task<IAPIResponse> HttpMethodExecuteAsync(string endpoint, Func<string, APIRequestContextOptions, Task<IAPIResponse>> sendRequest)
    {
        var url = $"{BaseUrl}/{endpoint}";
        var options = new APIRequestContextOptions();

        if (!string.IsNullOrEmpty(_token))
        {
            options.Headers = new Dictionary<string, string>
            {
                {"Authorization", $"Bearer {_token}" }
            };
        }

        try
        {
            var response = await sendRequest(url, options);

            if (response.Ok)
            {
                if (string.IsNullOrEmpty(_token))
                {
                    var rawText = await response.TextAsync();
                    if (!string.IsNullOrWhiteSpace(rawText))
                    {
                        try
                        {
                            // Разбор как JSON-структуры
                            using var doc = JsonDocument.Parse(rawText);

                            // Проверка, что это JSON-объект
                            if (doc.RootElement.ValueKind == JsonValueKind.Object)
                            {
                                if (doc.RootElement.TryGetProperty("token", out var tokenProp)
                                    || doc.RootElement.TryGetProperty("Token", out tokenProp))
                                {
                                    var extractedToken = tokenProp.GetString();
                                    if (!string.IsNullOrEmpty(extractedToken))
                                    {
                                        _token = extractedToken;
                                    }
                                }
                            }
                        }
                        catch (JsonException) { }
                    }
                }
            }

            return response;
        }
        catch (HttpRequestException e)
        {
            var statusCodeInfo = e.StatusCode.HasValue
                ? $"Статус код: {(int)e.StatusCode.Value} ({e.StatusCode.Value})"
                : "Статус код: Unknown";
            throw new Exception($"Url: {url}\n{statusCodeInfo}\n" + e.Message);
        }
    }

    #region HTTP методы

    protected async Task<IAPIResponse> GetAsync(string endpoint)
    {
        return await HttpMethodExecuteAsync(endpoint, (url, options) => Request.GetAsync(url, new APIRequestContextOptions
        {
            Headers = options.Headers
        }));
    }

    protected async Task<IAPIResponse> PostAsync<T>(string endpoint, T payload)
    {
        return await HttpMethodExecuteAsync(endpoint, (url, options) =>
        {
            options.DataObject = payload;
            return Request.PostAsync(url, options);
        });
    }

    protected async Task<IAPIResponse> PutAsync<T>(string endpoint, T payload)
    {
        return await HttpMethodExecuteAsync(endpoint, (url, options) => Request.PutAsync(url, new() { DataObject = payload }));
    }

    protected async Task<IAPIResponse> DeleteAsync(string endpoint)
    {
        return await DeleteAsync<object>(endpoint, null);
    }

    protected async Task<IAPIResponse> DeleteAsync<T>(string endpoint, T? payload)
    {
        return await HttpMethodExecuteAsync(endpoint, (url, options) => Request.DeleteAsync(url,
        new APIRequestContextOptions
        {
            Headers = options.Headers,
            DataObject = payload
        }));
    }

    #endregion
}