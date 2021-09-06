using Microsoft.EntityFrameworkCore;
using Together.Data.Models;

namespace Together.Data.SQL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<PostModel> Posts { get; set; }

        public DbSet<CommentModel> Comments { get; set; }

        public DbSet<ReplyModel> Replies { get; set; }
    }
}
