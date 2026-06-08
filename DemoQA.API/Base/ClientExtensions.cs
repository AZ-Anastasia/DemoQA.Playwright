using System.Text.Json;
using Microsoft.Playwright;

namespace DemoQA.API.Base;

public static class ClientExtensions
{
    /// <summary>
    /// Метод расширения для преобразования ответа сервера в модель (объект)
    /// для удобного обращения к полям
    /// </summary>
    /// <typeparam name="T">Передаваемая модель, в которую будут преобразованы данные из ответа</typeparam>
    /// <param name="response">Ответ сервера</param>
    /// <returns></returns>
    /// <exception cref="Exception">Исключение об
    /// - ошибке преобразования объекта
    /// - пустом объекте
    /// - невалидном формате
    /// </exception>
    public static async Task<T> ToModelAsync<T>(this IAPIResponse response)
    {
        var content = await response.TextAsync();

        if (!response.Ok)
        {
            throw new Exception($"Не удалось извлечь {typeof(T).Name}, т.к. запрос завершился с ошибкой {response.Status} ({response.Url}). Подробнее: \n{content}\n");
        }

        if (typeof(T) == typeof(bool))
            if (bool.TryParse(content, out var boolResult))
                return (T)(object)boolResult;

        if (typeof(T) == typeof(string))
            return (T)(object)content;

        try
        {
            var data = await response.JsonAsync<T>();

            if (data == null)
                throw new Exception($"Объект вернулся пустым (null) или не соответствует модели {typeof(T).Name}");

            return data;
        }
        catch (JsonException)
        {
            throw new Exception($"Ожидался JSON для модели {typeof(T).Name}, но сервер прислал невалидный формат со статусом {response.Status}\n({response.Url}).\nПодробнее: {content}");
        }
    }

    /// <summary>
    /// Метод расширения для проверки возврата одного из успешных кодов
    /// </summary>
    /// <param name="response">Ответ сервера</param>
    /// <param name="contextMessage">Сообщение об ошибке для отображения</param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException">
    /// Если код ответа не был успешным, то возвращается собранное сообщение со статусом и деталями
    /// </exception>
    public static async Task EnsureSuccessAsync(this IAPIResponse response, string contextMessage = "")
    {
        if (!response.Ok)
        {
            var errorText = await response.TextAsync();
            var fullMessage = string.IsNullOrEmpty(contextMessage)
                ? $"Запрос завершился ошибкой. Статус: {response.Status}\nОтвет: {errorText}"
                : $"{contextMessage}. Статус: {response.Status}\nОтвет: {errorText}";
            throw new HttpRequestException(fullMessage);
        }
    }
}