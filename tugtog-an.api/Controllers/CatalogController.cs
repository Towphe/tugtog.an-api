using System.Net;
using Microsoft.AspNetCore.Mvc;
using tugtog_an.service.spotify;
using tugtog_an.api.Filters;

namespace tugtog_an.api{
    [ApiController]
    [Route("/v1/[controller]")]
    public class CatalogController : ControllerBase{
        private readonly ISpotifyHandler _spotifyHandler;
        public CatalogController(ISpotifyHandler spotifyHandler){
            _spotifyHandler = spotifyHandler;
        }
        
        [HttpGet("genres")]
        [SpotifyAccessTokenAttribute]
        public async Task<IActionResult> RetrieveGenres(){
            // this must be middleware
            var headerValues = Request.Headers.GetCommaSeparatedValues("Spotify-Access-Token");

            var accessToken = headerValues.FirstOrDefault();

            var genres = await _spotifyHandler.RetrieveGenres(accessToken);

            return Ok(genres.Genres);
        }
    }
}