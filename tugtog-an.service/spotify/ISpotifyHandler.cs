using System.Net.Http;
using tugtog_an.domain.spotify;

namespace tugtog_an.service.spotify;

public interface ISpotifyHandler{
    /// <summary>
    /// Retrieve anonymous access token.
    /// </summary>
    public Task<AnonymousAccessToken> RetrieveAnonymousAccessToken();
    /// <summary>
    /// Retrieve all available genres.
    /// </summary>
    public Task<MusicGenres> RetrieveGenres(string anonymousAccessToken);
}
