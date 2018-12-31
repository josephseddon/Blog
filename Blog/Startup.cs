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

namespace Blog
{
    public class Startup
    {


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddIdentity<ApplicationUser,ApplicationRole> (options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            })
                //.AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BlogContext>();


            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
            });

            services.AddTransient<IRepository, Repository>();

            services.AddTransient<IFileManager, FileManager>();

            services.AddDbContext<BlogContext>(options => options.UseSqlServer(@"Data Source = (localdb)\ProjectsV13; Initial Catalog = Blog; Integrated Security=True; Connect Timeout=30;"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
            var scope = app.ApplicationServices.CreateScope();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            new UserRoleSeed(roleMgr).SeedRoles();
            new UserAccountSeed(userMgr).SeedOwner();
            new UserAccountSeed(userMgr).SeedMembers();
        }
    }
}
