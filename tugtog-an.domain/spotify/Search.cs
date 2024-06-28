
namespace tugtog_an.domain.spotify;

public class Search<T>{
    public string? Href {get; set;}
    public required int Limit {get; set;}
    public required string Next {get; set;}
    public required int Offset {get; set;}
    public string? Previous {get; set;}
    public required int Total {get; set;}
    public required List<T> Items {get; set;}
}