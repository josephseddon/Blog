using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogContext : IdentityDbContext 
    {
        public BlogContext(DbContextOptions<BlogContext> options) 
            : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }

    }
}
