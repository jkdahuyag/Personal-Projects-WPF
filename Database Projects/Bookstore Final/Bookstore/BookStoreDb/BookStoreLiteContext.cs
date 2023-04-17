using BookStoreDb.Configurations.Lite;
using BookStoreDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStoreDb;

public class BookStoreLiteContext : BookStoreContext
{
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Wrote> Wrotes { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder op)
    {
        if (!op.IsConfigured)
        {
            op.UseSqlite(@"DataSource=BookStoreDbFinal");
        }
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.ApplyConfiguration(new AuthorConfigurationLite());
        mb.ApplyConfiguration(new WroteConfigurationLite());
        mb.ApplyConfiguration(new PublisherConfigurationLite());
        mb.ApplyConfiguration(new BookConfigurationLite());
    }
}