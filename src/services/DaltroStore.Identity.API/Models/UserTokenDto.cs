namespace DaltroStore.Identity.API.Models
{
    public class UserTokenDto
    {
       public string Id { get; init; }

        public string Email { get; init; }

        public IEnumerable<UserClaimDto> Claims { get; init; }
    }
}