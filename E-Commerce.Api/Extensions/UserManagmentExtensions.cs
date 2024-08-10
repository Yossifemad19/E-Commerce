using E_Commerce.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce.Api.Extensions
{
    public static class UserManagmentExtensions
    {
        public static async Task<AppUser> GetUserWithAddresByEmail(this UserManager<AppUser> userManager,ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await userManager.Users.Include(x=>x.Address).FirstOrDefaultAsync(X=>X.Email == email);
        }

        public static async Task<AppUser> GetUserByClaims(this UserManager<AppUser> userManager,ClaimsPrincipal user)
        {
            return await userManager.Users.FirstOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
        }
    }
}
