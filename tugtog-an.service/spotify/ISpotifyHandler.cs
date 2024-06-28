using System.Net.Http;
using tugtog_an.domain.spotify;

namespace tugtog_an.service.spotify;

public interface ISpotifyHandler{
    /// <summary>
    /// Retrieve anonymous access token.
    /// </summary>
    public Task<AnonymousAccessToken> RetrieveAnonymousAccessToken();
    /// <summary>
    /// Generate Spotify Login Link
    /// 
    public Task<string> RetrieveAccessToken();
    /// <summary>
    /// Retrieve all available genres.
    /// </summary>
    public Task<MusicGenres> RetrieveGenres(string anonymousAccessToken);
    /// <summary>
    /// Retrieve artists
    /// </summary>
    public Task<ArtistsSearch> RetrieveArtists(string anonymousAccessToken, int limit=20, int offset=0);
    
}
