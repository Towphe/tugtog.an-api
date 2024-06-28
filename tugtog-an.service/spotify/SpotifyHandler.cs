
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Configuration;
using RestSharp;

using Newtonsoft.Json;
using tugtog_an.domain.spotify;

namespace tugtog_an.service.spotify;
    
public class SpotifyHandler : ISpotifyHandler{
    private readonly RestClient _restClient;
    private readonly IConfiguration _configuration;
    private readonly Random random = new Random();
    public SpotifyHandler(IConfiguration configuration){
        _configuration = configuration;

        _restClient = new RestClient();
    }

    private string GenerateRandomString(int length){
       const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
                            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public async Task<AnonymousAccessToken> RetrieveAnonymousAccessToken(){
        var request = new RestRequest("https://open.spotify.com/get_access_token");

        var response = await _restClient.GetAsync(request);

        return JsonConvert.DeserializeObject<AnonymousAccessToken>(response.Content);
    }

    public async Task<string> RetrieveAccessToken(){
        var scope = "user-read-private user-read-email";

        var urlString = $"https://accounts.spotify.com/authorize?response_type=token&client_id={_configuration["SPOTIFY_CLIENT_ID"]}&scope={scope}&redirect_uri={_configuration["REDIRECT_URI"]}&state={GenerateRandomString(16)}";

        
        
        // var request = new RestRequest("https://accounts.spotify.com/authorize");

        // // add query parameters
        // request.AddQueryParameter("response_type", "token");
        // request.AddQueryParameter("client_id", _configuration["SPOTIFY_CLIENT_ID"]);
        // request.AddQueryParameter("scope", "user-read-private user-read-email");
        // request.AddQueryParameter("redirect_uri", _configuration["REDIRECT_URI"]);
        // request.AddQueryParameter("state", GenerateRandomString(16));

        // foreach (var f in request.Parameters){
        //     Console.WriteLine($"{f.Name}: {f.Value}");
        // }
        
        // var response = await _restClient.GetAsync(request);

        // Console.WriteLine(response.Content);

        return urlString;
    }

    public async Task<MusicGenres> RetrieveGenres(string anonymousAccessToken){
        var request = new RestRequest("https://api.spotify.com/v1/recommendations/available-genre-seeds");
        request.AddHeader("Authorization", $"Bearer {anonymousAccessToken}");
        var response = await _restClient.GetAsync(request);

        return JsonConvert.DeserializeObject<MusicGenres>(response.Content);
    }

    public async Task<ArtistsSearch> RetrieveArtists(string anonymousAccessToken, int limit=20, int offset=0){
        var request = new RestRequest($"https://api.spotify.com/v1/search?q=year%3A{DateTime.UtcNow.Year}&type=artist&market=PH&limit={limit}&offset={offset}");
        request.AddHeader("Authorization", $"Bearer {anonymousAccessToken}");
        var response = await _restClient.GetAsync(request);
        
        return JsonConvert.DeserializeObject<ArtistsSearch>(response.Content);
    }
}