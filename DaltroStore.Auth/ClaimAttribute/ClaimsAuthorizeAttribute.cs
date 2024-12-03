using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace DaltroStore.Auth.Claims
{
    public static class UserClaimChecker
    {
        public static bool Check(HttpContext httpContext, string claimName, string claimValue)
        {
            return (httpContext.User.Identity?.IsAuthenticated ?? false) &&
                httpContext.User.Claims.Any(claim => claim.Type == claimName && claim.Value.Contains(claimValue));
        }
    }

    public class ClaimFilter : IAuthorizationFilter
    {
        private readonly Claim claim;

        public ClaimFilter(Claim claim)
        {
            this.claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var identity = context.HttpContext.User.Identity;

            if(identity == null || !identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!UserClaimChecker.Check(context.HttpContext, claim.Type, claim.Value))
            {
                context.Result = new ForbidResult();
            }
        }
    }

    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(ClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}