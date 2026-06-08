using System.Text.Json.Serialization;

namespace DemoQA.API.Models.BookStore.BookStore;

public class AddListOfBooksModel
{
    [JsonPropertyName("userId")]
    public string? UserId { get; set; }
    
    [JsonPropertyName("collectionOfIsbns")]
    public List<CollectionOfIsbnsModel>? collectionOfIsbns { get; set; }
}

public class CollectionOfIsbnsModel
{
    [JsonPropertyName("isbn")]
    public string? Isbn { get; set; }
}

public class AllBooksModel
{
    [JsonPropertyName("books")]
    public List<BookModel>? Books { get; set; }
}