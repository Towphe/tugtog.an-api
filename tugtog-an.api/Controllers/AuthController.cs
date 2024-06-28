using System.Net;
using Microsoft.AspNetCore.Mvc;
using tugtog_an.service.spotify;
using tugtog_an.api.Filters;
using Microsoft.Net.Http.Headers;

namespace tugtog_an.api{
    [ApiController]
    [Route("/v1/[controller]")]
    public class AuthController : ControllerBase{
        private readonly ISpotifyHandler _spotifyHandler;
        private readonly IConfiguration _configuration;
        public AuthController(ISpotifyHandler spotifyHandler, IConfiguration configuration){
            _spotifyHandler = spotifyHandler;
            _configuration = configuration;
        }

        [HttpGet("anonymous")]
        public async Task<IActionResult> RetrieveAnonymousSpotifyToken(){
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

        [HttpGet("spotify-login")]
        public async Task<IActionResult> RetrieveSpotifyToken(){
            var url = await _spotifyHandler.RetrieveAccessToken();
            return Ok(url);
        }

        // https://localhost:44350/#\
        //access_token=BQCXkIU3Q3qRWzGFb6MRWhsG0QKMm_xn6sC_XYpazUXZfTzjeCz8HGJhn5N-T9eczoUUzoEh3E4zSTAU-3kmucUsb8KURGSgSILMMH0MH0pzTq-5H6kZqS9KI0hYDEhz4PQ8kGR0hTybNUJrbdeqym-u139-sYIhB36oulpvw_wPRkY2tPIiRkPNvZxzEM5rWUX6LNDEpt15ttJtBTw
        //token_type=Bearer
        //expires_in=3600
        //state=MET2WK1SSZJ09WWB

        [HttpGet("logout")]
        public async Task<IActionResult> Logout(){
            // idea: maybe add checker if `sat` cookie exists

            // remove Cookie
            Response.Cookies.Delete("sat");

            // return Ok
            return Ok();
        }
    }
}