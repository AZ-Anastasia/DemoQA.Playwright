using System.Text.Json.Serialization;

namespace DemoQA.API.Models.BookStore.BookStore;

public class BookModel
{
    [JsonPropertyName("isbn")]
    public string? Isbn { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("subTitle")]
    public string? SubTitle { get; set; }

    [JsonPropertyName("author")]
    public string? Author { get; set; }

    [JsonPropertyName("publish_date")]
    public DateTime? PublishDate { get; set; }

    [JsonPropertyName("publisher")]
    public string? Publisher { get; set; }

    [JsonPropertyName("pages")]
    public int Pages { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("website")]
    public string? Website { get; set; }
}

public class BookListModel
{
    [JsonPropertyName("books")]
    public List<BookModel>? Books { get; set; }
}