namespace DaltroStore.Identity.API.Models
{
    public class UserLoginResponseDto
    {
        public string AcessToken { get; init; } = string.Empty;

        public double ExpiresIn { get; init; }

        public UserTokenDto UserToken { get; init; }
    }
}