using Microsoft.EntityFrameworkCore;
using blog_backend.Models;

namespace blog_backend.Data
{
    public class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options)
    {
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}