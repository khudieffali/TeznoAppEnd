using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ECommerseUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ECommerseUser> ECommerseUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ECommerseUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { 
                    Id = "aabb22", 
                    Name = "Admin", 
                    ConcurrencyStamp = "54sdsdcs", 
                    NormalizedName = "ADMIN" 
                }
                );

            ECommerseUser newUser = new ECommerseUser()
            {
                Id = "2005",
                FisrtName = "Admin",
                LastName = "Admin",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Address = "Razin 067",
                EmailConfirmed = true,
                NormalizedEmail = "ADMIN@GMAIL.COM",
                PhoneNumber = "+994553053232",
                ConcurrencyStamp = "aewfcjshusssjnxsjnskm",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                AccessFailedCount = 0,
                SecurityStamp = "kkjdxjsdx515sdxs",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                LockoutEnabled = false
            };
      

            PasswordHasher<ECommerseUser> passwordHasher = new PasswordHasher<ECommerseUser>();
            passwordHasher.HashPassword(newUser, "Admin_123");
            builder.Entity<ECommerseUser>().HasData(newUser);
            builder.Entity<IdentityUserRole<string>>
                ().HasData(
                new IdentityUserRole<string>
                {
                    UserId = newUser.Id,
                    RoleId = "aabb22"
                }
                );
    }
    }
}