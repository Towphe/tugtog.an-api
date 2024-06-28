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
        
        [HttpGet("genres")]
        [SpotifyAccessTokenAttribute]
        public async Task<IActionResult> RetrieveGenres(){
            var accessToken = Request.Cookies["sat"];

            var genres = await _spotifyHandler.RetrieveGenres(accessToken);

            return Ok(genres.Genres);
        }

        [HttpGet("artists")]
        [SpotifyAccessTokenAttribute]
        public async Task<IActionResult> RetrieveArtists(){
            var accessToken = Request.Cookies["sat"];

            var artists = await _spotifyHandler.RetrieveArtists(accessToken);

            return Ok(artists);
        }

        
    }
}