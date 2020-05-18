using FeedHunter.API.Model;
using Microsoft.EntityFrameworkCore;

namespace FeedHunter.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<FeedSource> FeedSources { get; set; }
    }
}
