using System.Text.Json.Serialization;

namespace DemoQA.API.Models.BookStore.Account;

public class TokenViewModel
{
    [JsonPropertyName("token")]
    public string? Token { get; set; }
    
    [JsonPropertyName("expires")]
    public DateTime Expires { get; set; }
    
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    
    [JsonPropertyName("result")]
    public string? Result { get; set; }
}