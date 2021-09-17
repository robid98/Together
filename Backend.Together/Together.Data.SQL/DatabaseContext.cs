using Microsoft.EntityFrameworkCore;
using Together.Data.Models;

namespace Together.Data.SQL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserAuthenticationModel>()
                .HasIndex(u => u.Email)
                .IsUnique();

            /* One to One relation */
            builder.Entity<UserAuthenticationModel>()
                .HasOne(a => a.UserProfileModel)
                .WithOne(a => a.UserAuthenticationModel)
                .HasForeignKey<UserProfileModel>(c => c.UserId);

            /* One to Many , UserProfile - Posts */
            builder.Entity<PostModel>()
                .HasOne<UserProfileModel>(s => s.UserProfileModel)
                .WithMany(g => g.UserPosts)
                .HasForeignKey(s => s.UserProfileId);
        }

        public DbSet<PostModel> Posts { get; set; }

        public DbSet<CommentModel> Comments { get; set; }

        public DbSet<ReplyModel> Replies { get; set; }

        public DbSet<UserAuthenticationModel> UsersAuthInfos { get; set; }

        public DbSet<UserProfileModel> UsersProfiles { get; set; }
    }
}
