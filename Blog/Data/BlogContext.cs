using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Blog.Models.Comments;

namespace Blog.Data
{
    public class BlogContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<MainComment> MainComments { get; set; }
        public DbSet<SubComment> SubComments { get; set; }

    }
}
