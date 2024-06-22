namespace DaltroStore.Identity.API.Models
{
    public class UserTokenInfoDto
    {
        public string AccessToken { get; init; }

        public double ExpiresIn { get; init; }

        public UserTokenInfoDto(string accessToken, double expiresIn)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
        }
    }
}