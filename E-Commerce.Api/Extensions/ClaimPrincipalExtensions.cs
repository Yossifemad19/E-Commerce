using System.Security.Claims;

namespace E_Commerce.Api.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        } 

    }
}
