using E_Commerce.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedIdentityDateAsync(UserManager<AppUser> userManger)
        {
            if (!userManger.Users.Any())
            {
                var user = new AppUser() {
                    UserName = "Youssef",
                    DisplayName="Youssef_emad",
                    Email="Youssef@test.com",
                    Address=new Address()
                    {
                        Name="mohamed",
                        Street="39 a",
                        State="helwan"
                    }
                
                };
                await userManger.CreateAsync(user,"Youssef@000");
            }
        }
    }
}
