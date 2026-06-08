using System.Text.Json.Serialization;
using DemoQA.API.Models.BookStore.BookStore;

namespace DemoQA.API.Models.BookStore.Account;

public class UserAuthModel
{
    [JsonPropertyName("userName")]
    public required string UserName { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }
}

public class UserSignInModel
{
    [JsonPropertyName("userID")]
    public string? UserId { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("books")]
    public List<BookModel>? Books { get; set; }
}