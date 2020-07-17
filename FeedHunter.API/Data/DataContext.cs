using FeedHunter.API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FeedHunter.API.Data
{
    public class DataContext :
        IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
            IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<FeedSource> FeedSources { get; set; }
        public DbSet<Article> Articles { get; set; }

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

            builder.Entity<ArticleCategory>(ac =>
            {
                ac.HasKey(ac => new { ac.ArticleId, ac.CategoryId });

                ac.HasOne(a => a.Article)
                    .WithMany(a => a.Categories)
                    .HasForeignKey(ac => ac.ArticleId);

                ac.HasOne(ac => ac.Category)
                    .WithMany(a => a.Articles)
                    .HasForeignKey(ac => ac.CategoryId);
            });

            builder.Entity<Article>(a =>
            {
                a.HasOne(f => f.Channel)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}