public class ExternalUrls
{
   public required string Spotify { get; set; }
}

public class Followers
{
    public string? Href { get; set; }
    public required int Total { get; set; }
}

public class Image
{
    public required string Url { get; set; }
    public required int Height { get; set; }
    public required int Width { get; set; }
}

public class ArtistPreview
{
    public required ExternalUrls ExternalUrls { get; set; }
    public required Followers Followers { get; set; }
    public required List<string> Genres { get; set; }
    public required string Href { get; set; }
    public required string Id { get; set; }
    public required List<Image> Images { get; set; }
   	public required string Name { get; set; }
   	public required int Popularity { get; set; }
   	public required string Type { get; set; }
   	public required string Uri { get; set; }
}