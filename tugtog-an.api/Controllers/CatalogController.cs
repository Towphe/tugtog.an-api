using System.Net;
using Microsoft.AspNetCore.Mvc;
using tugtog_an.service.spotify;
using tugtog_an.api.Filters;
using Microsoft.Net.Http.Headers;

namespace tugtog_an.api{
    [ApiController]
    [Route("/v1/[controller]")]
    public class CatalogController : ControllerBase{
        private readonly ISpotifyHandler _spotifyHandler;
        public CatalogController(ISpotifyHandler spotifyHandler){
            _spotifyHandler = spotifyHandler;
        }

        [HttpGet("anonymous-spotify-token")]
        public async Task<IActionResult> RetrieveSpotifyToken(){
            if (Request.Cookies["sat"] != null){
                // spotify acccess token is not null
                return StatusCode(400, new {
                    message = "Existing, unexpired token detected."
                });
            }

            // generate access token
            var accessToken = await _spotifyHandler.RetrieveAnonymousAccessToken();

            // set in Cookie
            var spotifyAccessTokenCookie = new CookieHeaderValue("sat", accessToken.AccessToken);    // sat = spotify-access-token
            Response.Cookies.Append("sat", accessToken.AccessToken, new CookieOptions(){
                HttpOnly = true,
                Expires = DateTimeOffset.FromUnixTimeMilliseconds(accessToken.AccessTokenExpirationTimeStampMs),
                Secure = true
            });

            return Ok(accessToken);
        }
        
        [HttpGet("genres")]
        [SpotifyAccessTokenAttribute]
        public async Task<IActionResult> RetrieveGenres(){
            var accessToken = Request.Cookies["sat"];

            var genres = await _spotifyHandler.RetrieveGenres(accessToken);

            return Ok(genres.Genres);
        }

        
    }
}