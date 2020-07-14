namespace VehicleRepairs.Database.Domain.Contexts
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using VehicleRepairs.Database.Domain.Entities;

    public partial class ApplicationDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Station> Stations { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<OrderDetail>()
                .HasKey(r => new { r.OrderId, r.ServiceId });
        }
    }
}
