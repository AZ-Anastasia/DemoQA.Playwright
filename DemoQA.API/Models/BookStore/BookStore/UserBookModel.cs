using System.Text.Json.Serialization;

namespace DemoQA.API.Models.BookStore.BookStore;

public class UserBookModel
{
    [JsonPropertyName("isbn")]
    public string? Isbn { get; set; }

    [JsonPropertyName("userId")]
    public string? UserId { get; set; }
}