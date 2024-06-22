namespace DaltroStore.Identity.API.Models
{
    public class UserLoginResponseDto
    {
        public string Id { get; init; }

        public string Email { get; init; }

        public string Name { get; init; }

        public IEnumerable<UserClaimDto> Claims { get; init; }

        public UserTokenInfoDto TokenInfo { get; init; }

        public UserLoginResponseDto(string id, string email, string name,
                                    IEnumerable<UserClaimDto> claims,
                                    UserTokenInfoDto tokenInfo)
        {
            Id = id;
            Email = email;
            Name = name;
            Claims = claims;
            TokenInfo = tokenInfo;
        }
    }
}