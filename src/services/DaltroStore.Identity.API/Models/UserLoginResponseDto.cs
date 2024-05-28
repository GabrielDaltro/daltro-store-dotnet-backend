namespace DaltroStore.Identity.API.Models
{
    public class UserLoginResponseDto
    {
        public string AccessToken { get; init; } = string.Empty;

        public double ExpiresIn { get; init; }

        public UserTokenDto UserToken { get; init; }
    }
}