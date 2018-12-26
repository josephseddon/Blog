using Blog.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Configuration
{
    public class UserAccountSeed
    {
        private UserManager<ApplicationUser> _userManager;

        public UserAccountSeed(UserManager<ApplicationUser> userManager) => _userManager = userManager;

        public void SeedOwner()
        {
            if ((_userManager.FindByNameAsync("Member1@email.com").Result) == null)
            {
                ApplicationUser user1 = new ApplicationUser();
                user1.UserName = "Member1@email.com";
                user1.Email = "Member1@email.com";


                IdentityResult result = _userManager.CreateAsync(user1, "Password123!").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user1, "Admin").Wait();
                }

            }
        }

        public void SeedMembers()
        {
            string[] customers = new string[] {
                "Customer1@email.com",
                "Customer2@email.com",
                "Customer3@email.com",
                "Customer4@email.com",
                "Customer5@email.com" };

            for (int i = 0; i < customers.Length; i++)
            {
                if (( _userManager.FindByNameAsync(customers[i]).Result) == null)
                {
                    ApplicationUser user = new ApplicationUser();
                    user.UserName = customers[i];
                    user.Email = customers[i];

                    IdentityResult result = _userManager.CreateAsync(user, "Password123!").Result;
                }
            }
        }
    }
}
