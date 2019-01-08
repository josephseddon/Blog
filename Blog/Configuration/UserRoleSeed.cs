using Blog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Configuration
{
    public class UserRoleSeed
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        //local instance of rolemanager
        public UserRoleSeed(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //Populate applications roles. 
        public void SeedRoles()
        {
            //array contains admin role names
            string[] roles = new string[] {
                "Admin",
                "Member",
                "Blocked"
            };

            for (int i = 0; i < roles.Length; i++)
            {
                //checks to see if the role exists and it doesn't creates it.
                if ((_roleManager.FindByNameAsync(roles[i]).Result) == null)
                {
                    IdentityResult result = _roleManager.CreateAsync(new ApplicationRole { Name = roles[i] }).Result;
                }
            }

            //adds claims to admin role
            SeedAdminRoleClaim();

        }

        public void SeedAdminRoleClaim()
        {
            string[] claims = new string[] {
                "ModifyRoles",
                "AdminInterface",
                "CanComment",
                "CanManageUsers",
                "CanViewAnalytics"
            };

            for (int i = 0; i < claims.Length; i++)
            {

                var adminRole = _roleManager.FindByNameAsync("Admin").Result;

                IdentityResult result = _roleManager.AddClaimAsync(adminRole, new Claim(claims[i], "")).Result;
            }

        }

        public void SeedMemberRoleClaim()
        {
            string[] claims = new string[] {
                "CanComment"
            };

            for (int i = 0; i < claims.Length; i++)
            {

                var adminRole = _roleManager.FindByNameAsync("Member").Result;

                IdentityResult result = _roleManager.AddClaimAsync(adminRole, new Claim(claims[i], "")).Result;
            }

        }

    }
}