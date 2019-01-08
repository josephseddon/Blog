using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Data;
using Blog.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blog.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Blog.Data.FileManager;
using Blog.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Blog
{
    public class Startup
    {


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {
            //adds mvc services
            services.AddMvc();

            //sets password policy
            services.AddIdentity<ApplicationUser,ApplicationRole> (options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            })

            //adds db context for application
                .AddEntityFrameworkStores<BlogContext>();

            //defines login path
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                options.Cookie.HttpOnly = true;
                options.AccessDeniedPath = "/Auth/AccessDenied";
            });


            //creates claims policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ModifyRoles", policy => policy.RequireClaim("ModifyRoles"));
                options.AddPolicy("AdminInterface", policy => policy.RequireClaim("AdminInterface"));
                options.AddPolicy("Comment", policy => policy.RequireClaim("CanComment"));
                options.AddPolicy("UserManagement", policy => policy.RequireClaim("CanManageUsers"));
                options.AddPolicy("ViewAnalytics", policy => policy.RequireClaim("CanViewAnalytics"));
            });

            //db respository
            services.AddTransient<IRepository, Repository>();

            //File manager repository
            services.AddTransient<IFileManager, FileManager>();

            //
            services.AddDbContext<BlogContext>(options => options.UseSqlServer(@"Data Source = (localdb)\ProjectsV13; Initial Catalog = Blog; Integrated Security=True; Connect Timeout=30;"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();

            //Seeds database users and roles
            var scope = app.ApplicationServices.CreateScope();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            new UserRoleSeed(roleMgr).SeedRoles();
            new UserRoleSeed(roleMgr).SeedAdminRoleClaim();
            new UserRoleSeed(roleMgr).SeedMemberRoleClaim();
            new UserAccountSeed(userMgr).SeedOwner();
            new UserAccountSeed(userMgr).SeedMembers();
        }
    }
}
