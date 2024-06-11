
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Configuration;
using RestSharp;

using Newtonsoft.Json;
using tugtog_an.domain.spotify;

namespace tugtog_an.service.spotify;
    
public class SpotifyHandler : ISpotifyHandler{
    private readonly RestClient _restClient;
    private readonly IConfiguration _configuration;
    public SpotifyHandler(IConfiguration configuration){
        _configuration = configuration;

        _restClient = new RestClient();
    }
    public async Task<AnonymousAccessToken> RetrieveAnonymousAccessToken(){
        var request = new RestRequest("https://open.spotify.com/get_access_token");

        var response = await _restClient.GetAsync(request);

        return JsonConvert.DeserializeObject<AnonymousAccessToken>(response.Content);
    }

    public async Task<MusicGenres> RetrieveGenres(string anonymousAccessToken){
        var request = new RestRequest("https://api.spotify.com/v1/recommendations/available-genre-seeds");
        request.AddHeader("Authorization", $"Bearer {anonymousAccessToken}");
        var response = await _restClient.GetAsync(request);

        return JsonConvert.DeserializeObject<MusicGenres>(response.Content);
    }
}