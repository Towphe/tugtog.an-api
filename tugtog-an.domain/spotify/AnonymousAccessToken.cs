
namespace tugtog_an.domain.spotify{
    public class AnonymousAccessToken{
        public required string ClientId {get; set;}
        public required string AccessToken {get; set;}
        public required double AccessTokenExpirationTimeStampMs {get; set;}
        public required bool IsAnonymous {get; set;}
    }
}