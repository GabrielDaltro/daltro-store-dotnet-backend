namespace DaltroStore.Identity.API.Models
{
    public class UserClaimDto
    {
        public string Value { get; init; }

        public string Type { get; init; }

        public UserClaimDto(string value, string type)
        {
            Value = value;
            Type = type;
        }
    }
}