using InGame.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InGame.Infrastructure.Data
{
    public class InGameContext :DbContext//IdentityDbContext<ApplicationUser>
    {
        public InGameContext(DbContextOptions<InGameContext> options) : base(options)
        {
        }

     

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>();
            builder.Entity<Category>();
            builder.Entity<SubCategory>();
        }
    }
}
