using Blog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Configuration
{
    public class UserRoleSeed
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleSeed(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public void SeedRoles()
        {
            string[] roles = new string[] {
                "Admin",
                "Member"};

            for (int i = 0; i < roles.Length; i++)
            {

                if ((_roleManager.FindByNameAsync(roles[i]).Result) == null)
                {
                    IdentityResult result = _roleManager.CreateAsync(new IdentityRole { Name = roles[i] }).Result;
                }
            }

        }

        public async void SeedMember()
        {

            if ((await _roleManager.FindByNameAsync("Member")) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
            }

        }

    }
}