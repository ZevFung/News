using Microsoft.EntityFrameworkCore;

namespace News.Service
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        {
        }

        public DbSet<News.Model.Entity.News> News { get; set; }
        public DbSet<News.Model.Entity.Banner> Banner { get; set; }
        public DbSet<News.Model.Entity.Comment> Comment { get; set; }
        public DbSet<News.Model.Entity.NewsClassify> NewsClassify { get; set; }
    }
}
